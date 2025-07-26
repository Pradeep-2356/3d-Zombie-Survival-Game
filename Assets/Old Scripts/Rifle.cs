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

    [Header("Rifle Ammunation and Shooting")]
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

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToShoot && presentAmmunation > 0)
        {
            animator.SetBool("Fire", true);
            animator.SetBool("Idle", false);
            nextTimeToShoot = Time.time + 1f / fireCharge;
            Shoot();
        }
        else if (Input.GetButton("Fire1") && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)))
        {
            animator.SetBool("Idle", false);
            animator.SetBool("FireWalk", true);
        }
        else if (Input.GetButton("Fire1") && Input.GetButton("Fire2"))
        {
            animator.SetBool("Idle", false);
            animator.SetBool("IdleAim", true);
            animator.SetBool("FireWalk", true);
            animator.SetBool("Walk", true);
            animator.SetBool("Reloading", false);
        }
        else
        {
            animator.SetBool("Fire", false);
            animator.SetBool("FireWalk", false);
            animator.SetBool("Idle", true);
        }

        // Start reloading when ammo runs out
        if (presentAmmunation <= 0 && mag > 0)
        {
            StartCoroutine(Reload());
            return;
        }

        // Show ammo out if no mags left
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

        // Update UI
        AmmoCount.occurence.UpdateAmmoText(presentAmmunation);
        AmmoCount.occurence.UpdateMagText(mag);

        muzzleSpark.Play();
        audioSource.PlayOneShot(shootingSound);

        RaycastHit hitInfo;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, shootingRange))
        {
            Debug.Log(hitInfo.transform.name);

            ObjectToHit objectToHit = hitInfo.transform.GetComponent<ObjectToHit>();
            Zombie1 zombie1 = hitInfo.transform.GetComponent<Zombie1>();
            Zombie2 zombie2 = hitInfo.transform.GetComponent<Zombie2>();

            if (objectToHit != null)
            {
                objectToHit.ObjectHitDamage(giveDamageOf);
                GameObject WoodGo = Instantiate(WoodenEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(WoodGo, 1f);
            }
            else if (zombie1 != null)
            {
                zombie1.zombieHitDamage(giveDamageOf);
                GameObject goreEffectgo = Instantiate(goreEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(goreEffectgo, 1f);
            }
            else if (zombie2 != null)
            {
                zombie2.zombieHitDamage(giveDamageOf);
                GameObject goreEffectgo = Instantiate(goreEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(goreEffectgo, 1f);
            }
        }
    }

    IEnumerator Reload()
    {
        if (mag <= 0)
        {
            Debug.Log("No more mags left!");
            yield break; // Don't reload
        }

        player.playerSpeed = 0f;
        player.playerSprint = 0f;
        setReloading = true;
        Debug.Log("Reloading...");
        animator.SetBool("Reloading", true);
        audioSource.PlayOneShot(reloadingSound);

        yield return new WaitForSeconds(reloadingTime);

        mag--; // Reduce mag only when reload happens
        presentAmmunation = maximumAmmunation;

        // Update UI
        AmmoCount.occurence.UpdateAmmoText(presentAmmunation);
        AmmoCount.occurence.UpdateMagText(mag);

        animator.SetBool("Reloading", false);
        player.playerSpeed = 1.9f;
        player.playerSprint = 3;
        setReloading = false;
    }

    IEnumerator ShowAmmoOut()
    {
        AmmoOutUI.SetActive(true);
        yield return new WaitForSeconds(5f);
        AmmoOutUI.SetActive(false);
    }
}
