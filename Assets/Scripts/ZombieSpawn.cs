using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ZombieSpawn : MonoBehaviour
{
    [Header("Zombie Spawn Var")]
    public GameObject zombiePrefab;
    public Transform zombieSpawnPosition;
    public GameObject dangerZone1;
    private float repeatCycle = 1f;

    [Header("Sounds")]
    public AudioClip DangerZoneSound;
    public AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            InvokeRepeating("EnemySpawner", 5f, repeatCycle);
            audioSource.PlayOneShot(DangerZoneSound);
            StartCoroutine(dangerZoneTimer());
            Destroy(gameObject, 10f);
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    void EnemySpawner()
    {
        Instantiate(zombiePrefab, zombieSpawnPosition.position, zombieSpawnPosition.rotation);

    }

    IEnumerator dangerZoneTimer()
    {
        dangerZone1.SetActive(true);
        yield return new WaitForSeconds(5f);
        dangerZone1.SetActive(false);
    }

}
