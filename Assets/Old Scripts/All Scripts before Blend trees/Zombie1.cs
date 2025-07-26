using UnityEngine;
using UnityEngine.AI;

public class Zombie1 : MonoBehaviour
{
    [Header("Zombie Health and Damage")]
    public float zombieHealth = 100f;
    private float presentHealth;
    public float giveDamage = 5f;
    public HealthBar healthBar;

    [Header("Zombie Things")]
    public LayerMask PlayerLayer;
    public NavMeshAgent zombieAgent;
    public Transform LookPoint;
    public Camera AttackingRaycastArea;
    public Transform PlayerBody;

    [Header("Zombie Guarding Var")]
    public GameObject[] walkPoints;
    private int currentZombiePosition = 0;
    public float zombieSpeed;
    private float walkingpointRadius = 2f;

    [Header("Zombie Attacking Var")]
    public float timeBtwAttack;
    private bool previouslyAttack;

    [Header("Zombie Animations")]
    public Animator anim;

    [Header("Zombie Mood/States")]
    public float visionRadius;
    public float attackingRadius;
    public bool playerInVisionRadius;
    public bool playerInAttackingRadius;

    private void Awake()
    {
        healthBar.GiveFullHealth(zombieHealth);
        zombieAgent = GetComponent<NavMeshAgent>();
        presentHealth = zombieHealth;
    }

    private void Update()
    {
        playerInVisionRadius = Physics.CheckSphere(transform.position, visionRadius, PlayerLayer);
        playerInAttackingRadius = Physics.CheckSphere(transform.position, attackingRadius, PlayerLayer);

        if (!playerInVisionRadius && !playerInAttackingRadius)
            Guard();
        else if (playerInVisionRadius && !playerInAttackingRadius)
            PursuePlayer();
        else if (playerInVisionRadius && playerInAttackingRadius)
            AttackPlayer();
    }

    private void ResetAllBools()
    {
        anim.SetBool("Walking", false);
        anim.SetBool("Running", false);
        anim.SetBool("Attacking", false);
    }

    private void Guard()
    {
        ResetAllBools();
        anim.SetBool("Walking", true);
        zombieAgent.isStopped = false;

        if (Vector3.Distance(walkPoints[currentZombiePosition].transform.position, transform.position) < walkingpointRadius)
        {
            currentZombiePosition = Random.Range(0, walkPoints.Length);
        }

        transform.position = Vector3.MoveTowards(transform.position,
            walkPoints[currentZombiePosition].transform.position,
            Time.deltaTime * zombieSpeed);

        transform.LookAt(walkPoints[currentZombiePosition].transform.position);
    }

    private void PursuePlayer()
    {
        ResetAllBools();
        anim.SetBool("Running", true);

        zombieAgent.isStopped = false;
        zombieAgent.SetDestination(PlayerBody.position);
    }

    private void AttackPlayer()
    {
        ResetAllBools();
        anim.SetBool("Attacking", true);

        zombieAgent.isStopped = true;
        zombieAgent.SetDestination(transform.position);
        transform.LookAt(LookPoint);

        if (!previouslyAttack)
        {
            Debug.Log("Attack Triggered");

            Vector3 directionToPlayer = (PlayerBody.position - AttackingRaycastArea.transform.position).normalized;

            RaycastHit hitInfo;
            if (Physics.Raycast(AttackingRaycastArea.transform.position, directionToPlayer, out hitInfo, attackingRadius))
            {
                Debug.Log("Raycast Hit: " + hitInfo.transform.name);

                PlayerScript player = hitInfo.transform.GetComponent<PlayerScript>();
                if (player != null)
                {
                    player.playerHitDamage(giveDamage);
                    Debug.Log("Player Damaged");
                }
            }
            else
            {
                Debug.Log("Raycast missed");
            }

            previouslyAttack = true;
            Invoke(nameof(ActiveAttacking), timeBtwAttack);
        }
    }

    private void ActiveAttacking()
    {
        previouslyAttack = false;
    }

    public void zombieHitDamage(float takeDamage)
    {
        presentHealth -= takeDamage;
        healthBar.SetHealth(presentHealth);

        if (presentHealth <= 0)
        {
            anim.SetBool("Walking", false);
            anim.SetBool("Running", false);
            anim.SetBool("Attacking", false);
            anim.SetBool("Died", true);
            zombieDie();
        }
    }

    private void zombieDie()
    {
        zombieAgent.isStopped = true;
        zombieAgent.SetDestination(transform.position);
        zombieSpeed = 0f;
        attackingRadius = 0f;
        visionRadius = 0f;
        playerInVisionRadius = false;
        playerInAttackingRadius = false;
        Destroy(gameObject, 5.0f);
    }
}
