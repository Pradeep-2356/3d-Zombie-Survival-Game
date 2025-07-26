using UnityEngine;
using UnityEngine.SceneManagement;

public class Objective4 : MonoBehaviour
{
      private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Vehicle")
        {
            //Complete Objective
            ObjectivesComplete.occurence.GetObjectivesDone(true, true, true, true);
            SceneManager.LoadScene("MainMenu");
        }
    }
}
