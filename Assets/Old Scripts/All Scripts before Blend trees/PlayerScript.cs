using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
    [Header("Player Movement")]
    public float playerSpeed = 1.9f;
    public float playerSprint = 3f;

    [Header("Player Health Things")]
    private float playerHealth = 120f;
    public float presentHealth;
    public GameObject playerDamage;
    public HealthBar healthBar;
    private bool isTakingDamage = false;
    private float damageUICooldown = 0f;

    [Header("Player Script Cameras")]
    public Transform playerCamera;
    public GameObject EndGameMenuUI;

    [Header("Player Animator and Gravity")]
    public CharacterController cC;
    public float gravity = 9.81f;
    public Animator animator;

    [Header("Player Jumping and Velocity")]
    private float turnCalmTime = 0.1f;
    float turnCalmVelocity;
    public float jumpRange = 1f;
    Vector3 velocity;
    public Transform surfaceCheck;
    bool onSurface;
    public float surfaceDistance = 0.4f;
    public LayerMask surfaceMask;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        presentHealth = playerHealth;
        healthBar.GiveFullHealth(playerHealth);
    }

    private void Update()
    {
        onSurface = Physics.CheckSphere(surfaceCheck.position, surfaceDistance, surfaceMask);
        if (onSurface && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y -= gravity * Time.deltaTime;
        cC.Move(velocity * Time.deltaTime);

        PlayerMove();
        Jump();

        if (damageUICooldown > 0)
        {
            damageUICooldown -= Time.deltaTime;
            if (damageUICooldown <= 0)
            {
                playerDamage.SetActive(false);
            }
        }
    }

    void PlayerMove()
    {
        float horizontal_axis = Input.GetAxis("Horizontal");
        float vertical_axis = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal_axis, 0f, vertical_axis);
        float speedInput = direction.magnitude;

        if (speedInput >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            float moveSpeed = Input.GetKey(KeyCode.LeftShift) ? playerSprint : playerSpeed;
            cC.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);

            animator.SetFloat("Speed", moveSpeed, 0.1f, Time.deltaTime);
        }
        else
        {
            animator.SetFloat("Speed", 0f, 0.1f, Time.deltaTime);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && onSurface)
        {
            animator.SetTrigger("Jump");
            velocity.y = Mathf.Sqrt(jumpRange * 2f * gravity);
        }
    }

    public void playerHitDamage(float takeDamage)
    {
        presentHealth -= takeDamage;
        healthBar.SetHealth(presentHealth);

        playerDamage.SetActive(true);
        damageUICooldown = 2.2f;

        if (presentHealth <= 0)
        {
            playerDie();
        }
    }

    private void playerDie()
    {
        animator.SetBool("Die", true);
        EndGameMenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Object.Destroy(gameObject, 1.0f);
    }

    IEnumerator PlayerDamage()
    {
        isTakingDamage = true;
        playerDamage.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        playerDamage.SetActive(false);
        isTakingDamage = false;
    }
}
