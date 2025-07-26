using UnityEngine;
using System.Collections;
public class RiflePickup : MonoBehaviour
{
    [Header("Rifle's")]
    public GameObject PlayerRifle;
    public GameObject PickupRifle;
    public PlayerPunch playerPunch;
    public GameObject rifleUI;

    [Header("Rifle Assign Things")]
    public PlayerScript player;
    private float radius = 2.5f;
    public Animator animator;
    public float nextTimeToPunch = 0f;
    public float punchCharge = 15f;
    private bool canPunch = true;

    private void Awake()
    {
        PlayerRifle.SetActive(false);
        rifleUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && canPunch && !PlayerRifle.activeSelf)
        {
            animator.SetTrigger("Punch");
            playerPunch.Punch();
            StartCoroutine(PunchCooldown());
        }

        if (Vector3.Distance(transform.position, player.transform.position) < radius)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                PlayerRifle.SetActive(true);
                PickupRifle.SetActive(false);
                rifleUI.SetActive(true);
                ObjectivesComplete.occurence.GetObjectivesDone(true, false, false, false);
            }
        }
    }
    IEnumerator PunchCooldown()
    {
    canPunch = false;
    yield return new WaitForSeconds(1f / punchCharge); // your punch delay
    canPunch = true;
    }
}
