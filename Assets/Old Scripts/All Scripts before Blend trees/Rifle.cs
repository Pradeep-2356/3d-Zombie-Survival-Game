using UnityEngine;
using System.Collections;

public class Rifle : MonoBehaviour
{
    [Header("Rifle Things")]
    public Camera cam;
    public float giveDamageOf = 10f;
    public float shootingRange = 100f;
    public float fireCharge = 15f;
    public float nextTimeToShoot = 0f;
    public Animator animator;
    public Transform hand;

    [Header("Rifle Ammunition and Shooting")]
    public int maximumAmmunation = 30;
    public int mag = 10;
    private int presentAmmunation;
    public float reloadingTime = 1.3f;
    private bool setReloading = false;
    public PlayerScript player;
    public GameObject rifleUI;

    [Header("Rifle Effects")]
    public ParticleSystem muzzleSpark;
    public GameObject WoodenEffect;
    public GameObject goreEffect;

    [Header("Sounds and UI")]
    public GameObject AmmoOutUI;
    public AudioClip shootingSound;
    public AudioClip reloadingSound;
    public AudioSource audioSource;

    private void Awake()
    {
        transform.SetParent(hand);
        rifleUI.SetActive(true);
        presentAmmunation = maximumAmmunation;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (setReloading)
            return;
        animator.SetFloat("IsFiring", Input.GetButton("Fire1") ? 1f : 0f);
        bool isWalking = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        bool isAiming = Input.GetButton("Fire2");
        animator.SetBool("IsAiming", isAiming);

        // FIRE LOGIC
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToShoot && presentAmmunation > 0)
        {
            nextTimeToShoot = Time.time + 1f / fireCharge;

            if (isWalking)
            {
                animator.SetTrigger("FireWalk");
            }
            else
            {
                animator.SetTrigger("Fire");
            }

            Shoot();
        }

        // RELOAD IF NEEDED
        if (presentAmmunation <= 0 && mag > 0)
        {
            StartCoroutine(Reload());
            return;
        }

        // OUT OF AMMO DISPLAY
        if (presentAmmunation <= 0 && mag == 0)
        {
            StartCoroutine(ShowAmmoOut());
            return;
        }
    }

    private void Shoot()
    {
        if (mag == 0 && presentAmmunation <= 0)
        {
            StartCoroutine(ShowAmmoOut());
            return;
        }

        presentAmmunation--;
        AmmoCount.occurence.UpdateAmmoText(presentAmmunation);
        AmmoCount.occurence.UpdateMagText(mag);

        muzzleSpark.Play();
        audioSource.PlayOneShot(shootingSound);

        RaycastHit hitInfo;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, shootingRange))
        {
            Debug.Log(hitInfo.transform.name);

            if (hitInfo.transform.TryGetComponent<ObjectToHit>(out var objectToHit))
            {
                objectToHit.ObjectHitDamage(giveDamageOf);
                GameObject WoodGo = Instantiate(WoodenEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(WoodGo, 1f);
            }
            else if (hitInfo.transform.TryGetComponent<Zombie1>(out var zombie1))
            {
                zombie1.zombieHitDamage(giveDamageOf);
                GameObject goreEffectgo = Instantiate(goreEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(goreEffectgo, 1f);
            }
            else if (hitInfo.transform.TryGetComponent<Zombie2>(out var zombie2))
            {
                zombie2.zombieHitDamage(giveDamageOf);
                GameObject goreEffectgo = Instantiate(goreEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(goreEffectgo, 1f);
            }
        }
    }

    IEnumerator Reload()
    {
        if (mag <= 0) yield break;

        player.playerSpeed = 0f;
        player.playerSprint = 0f;
        setReloading = true;

        Debug.Log("Reloading...");
        animator.SetBool("Reloading", true);
        audioSource.PlayOneShot(reloadingSound);

        yield return new WaitForSeconds(reloadingTime);

        mag--;
        presentAmmunation = maximumAmmunation;

        AmmoCount.occurence.UpdateAmmoText(presentAmmunation);
        AmmoCount.occurence.UpdateMagText(mag);

        animator.SetBool("Reloading", false);
        player.playerSpeed = 1.9f;
        player.playerSprint = 3f;
        setReloading = false;
    }

    IEnumerator ShowAmmoOut()
    {
        AmmoOutUI.SetActive(true);
        yield return new WaitForSeconds(5f);
        AmmoOutUI.SetActive(false);
    }
}
