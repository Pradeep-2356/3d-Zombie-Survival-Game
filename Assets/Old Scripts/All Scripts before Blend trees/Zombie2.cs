using UnityEngine;
using UnityEngine.AI;

public class Zombie2 : MonoBehaviour
{
    [Header("Zombie Health and Damage")]
    private float zombieHealth = 100f;
    private float presentHealth;
    public float giveDamage = 5f;
    public HealthBar healthBar;

    [Header("Zombie Things")]
    public LayerMask PlayerLayer;
    public NavMeshAgent zombieAgent;
    public Transform LookPoint;
    public Camera AttackingRaycastArea;
    public Transform PlayerBody;

    [Header("Zombie Standing Var")]
    public float zombieSpeed;

    [Header("Zombie Attacking Var")]
    public float timeBtwAttack = 2f;
    private bool previouslyAttack;

    [Header("Zombie Animations")]
    public Animator anim;

    [Header("Zombie Mood/States")]
    public float visionRadius = 15f;
    public float attackingRadius = 2.5f;
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
            Idle();
        else if (playerInVisionRadius && !playerInAttackingRadius)
            PursuePlayer();
        else if (playerInVisionRadius && playerInAttackingRadius)
            AttackPlayer();
    }

    private void ResetAllBools()
    {
        anim.SetBool("Idle", false);
        anim.SetBool("Running", false);
        anim.SetBool("Attacking", false);
    }

    private void Idle()
    {
        ResetAllBools();
        anim.SetBool("Idle", true);
        zombieAgent.isStopped = true;
        zombieAgent.SetDestination(transform.position);
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
            anim.SetBool("Died", true);
            zombieDie();
        }
    }

    private void zombieDie()
    {
        ResetAllBools();
        zombieAgent.isStopped = true;
        zombieSpeed = 0f;
        attackingRadius = 0f;
        visionRadius = 0f;
        playerInVisionRadius = false;
        playerInAttackingRadius = false;
        Destroy(gameObject, 5f);
    }
}
