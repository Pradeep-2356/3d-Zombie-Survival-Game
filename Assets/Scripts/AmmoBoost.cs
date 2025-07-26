using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AmmoBoost : MonoBehaviour
{
    [Header("AmmoBoost")]
    public Rifle rifle;
    private int magToGive = 15;
    private float radius = 2.5f;

    [Header("Sounds")]
    public AudioClip AmmoBoostSound;
    public AudioSource audioSource;

    [Header("AmmoBox Animator")]
    public Animator animator;

    [Header("Instruction UI")]
    public GameObject thingPanel;
    public Text thingText;
    public string itemName = "Ammo";

    private void Update()
    {
        if (Vector3.Distance(transform.position, rifle.transform.position) < radius)
        {
            thingPanel.SetActive(true);
            thingText.text = $"Press F key to pickup the {itemName}";

            if (Input.GetKeyDown("f"))
            {
                animator.SetBool("Open", true);
                rifle.mag = magToGive;

                //Updating the UI
                AmmoCount.occurence.UpdateMagText(magToGive);

                //sound effect
                audioSource.PlayOneShot(AmmoBoostSound);

                Object.Destroy(gameObject, 1.5f);
            }
        }
        else
        {
            thingPanel.SetActive(false);
        }
    }
}
