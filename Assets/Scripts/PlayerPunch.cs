using UnityEngine;

public class PlayerPunch : MonoBehaviour
{
    [Header("Punch Settings")]
    public Camera cam;
    public float giveDamageOf = 10f;
    public float punchingRange = 5f;
    public Animator animator;
    public Transform playerBody;

    public void Punch()
    {
        Debug.Log("Punch method called");

        animator.SetTrigger("Punch");

        // Ray origin slightly forward from camera
        Vector3 rayOrigin = cam.transform.position + cam.transform.forward * 0.5f;
        Debug.DrawRay(rayOrigin, cam.transform.forward * punchingRange, Color.red, 2f);

        if (Physics.Raycast(rayOrigin, cam.transform.forward, out RaycastHit hitInfo, punchingRange))
        {
            Debug.Log("Raycast hit: " + hitInfo.transform.name);

            // 🔁 Rotate player body toward hit point (only on Y axis)
            Vector3 direction = new Vector3(hitInfo.point.x, playerBody.position.y, hitInfo.point.z) - playerBody.position;
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                playerBody.rotation = Quaternion.Slerp(playerBody.rotation, targetRotation, Time.deltaTime * 20f);
            }

            // Damage logic
            if (hitInfo.transform.TryGetComponent(out Zombie1 zombie1))
            {
                zombie1.zombieHitDamage(giveDamageOf);
                Debug.Log("Zombie1 Damaged");
            }
            else if (hitInfo.transform.TryGetComponent(out Zombie2 zombie2))
            {
                zombie2.zombieHitDamage(giveDamageOf);
                Debug.Log("Zombie2 Damaged");
            }
            else
            {
                Debug.Log("Hit something else: " + hitInfo.transform.name);
            }
        }
        else
        {
            Debug.Log("Raycast missed");
        }
    }
}
