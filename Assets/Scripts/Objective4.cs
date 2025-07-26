using UnityEngine;
using UnityEngine.SceneManagement;

public class Objective4 : MonoBehaviour
{
      private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Vehicle")
        {
            //Complete Objective
            ObjectiveManager.MarkObjective(4);
            SceneManager.LoadScene("MainMenu");
        }
    }
}
