using UnityEngine;
using UnityEngine.UI;

public class CrosshairColorChange : MonoBehaviour
{
    [Header("Crosshair Settings")]
    public Image crosshairImage;
    public Color defaultColor = Color.white;
    public Color enemyColor = Color.red;
    public float detectionRange = 100f;

    [Header("Camera")]
    public Camera playerCamera;  // Assign Main Camera here

    void Update()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, detectionRange))
        {
            if (hit.collider.CompareTag("Zombie"))
            {
                crosshairImage.color = enemyColor;
            }
            else
            {
                crosshairImage.color = defaultColor;
            }
        }
        else
        {
            crosshairImage.color = defaultColor;
        }
    }
}
