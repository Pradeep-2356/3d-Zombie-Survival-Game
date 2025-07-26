using UnityEngine;

public class RotateHealthBar_UI : MonoBehaviour
{
    public Transform MainCamera;
    private void LateUpdate(){
        transform.LookAt(transform.position + MainCamera.forward);
    }
}
