using UnityEngine;

public class Objective2 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Complete Objective
            ObjectiveManager.MarkObjective(2);
            Destroy(gameObject, 2f);
        }
    }
}
