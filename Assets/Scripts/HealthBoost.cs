using UnityEngine;
using UnityEngine.UI;

public class HealthBoost : MonoBehaviour
{
    public PlayerScript player;
    private float healthToGive = 120f;
    private float radius = 2.5f;

    public AudioClip HealthBoostSound;
    public AudioSource audioSource;
    public Animator animator;

    public GameObject thingPanel;
    public Text thingText;
    public string itemName = "Medikit";

    private bool playerInRange = false;

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        bool wasInRange = playerInRange;
        playerInRange = distance < radius;

        if (playerInRange)
        {
            if (!thingPanel.activeSelf)
            {
                thingPanel.SetActive(true);
                thingText.text = $"Press F key to pickup the {itemName}";
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                animator.SetBool("Open", true);
                player.presentHealth = healthToGive;
                player.healthBar.SetHealth(healthToGive);
                audioSource.PlayOneShot(HealthBoostSound);
                Destroy(gameObject, 1.5f);
            }
        }
        else if (wasInRange && !AnyOtherHealthBoxInRange())
        {
            // Only hide if no other healthbox is keeping it open
            thingPanel.SetActive(false);
        }
    }

    private bool AnyOtherHealthBoxInRange()
    {
        HealthBoost[] allHealthBoxes = FindObjectsOfType<HealthBoost>();
        foreach (HealthBoost hb in allHealthBoxes)
        {
            if (hb == this) continue;
            float dist = Vector3.Distance(hb.transform.position, player.transform.position);
            if (dist < hb.radius)
                return true;
        }
        return false;
    }
}
