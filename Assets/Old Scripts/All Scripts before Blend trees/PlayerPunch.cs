using UnityEngine;

public class PlayerPunch : MonoBehaviour
{
    [Header("Punch Settings")]
    public Camera cam;
    public float giveDamageOf = 10f;
    public float punchingRange = 5f;
    public Animator animator;

    public void Punch()
    {
        animator.SetTrigger("Punch");

        RaycastHit hitInfo;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, punchingRange))
        {
            Debug.Log(hitInfo.transform.name);

            if (hitInfo.transform.TryGetComponent<ObjectToHit>(out var objectToHit))
            {
                objectToHit.ObjectHitDamage(giveDamageOf);
            }
            else if (hitInfo.transform.TryGetComponent<Zombie1>(out var zombie1))
            {
                zombie1.zombieHitDamage(giveDamageOf);
            }
            else if (hitInfo.transform.TryGetComponent<Zombie2>(out var zombie2))
            {
                zombie2.zombieHitDamage(giveDamageOf);
            }
        }
    }
}
