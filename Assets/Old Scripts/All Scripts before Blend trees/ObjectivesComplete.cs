using UnityEngine;
using UnityEngine.UI;

public class ObjectivesComplete : MonoBehaviour
{
    [Header("Objectives to Complete")]
    public Text Objective1;
    public Text Objective2;
    public Text Objective3;
    public Text Objective4;

    public static ObjectivesComplete occurence;

    private void Awake()
    {
        occurence = this;
    }

    public void GetObjectivesDone(bool obj1, bool obj2, bool obj3, bool obj4)
    {
        if (obj1 == true)
        {
            Objective1.text = "1. Completed";
            Objective1.color = Color.green;
        }
        else
        {
            Objective1.text = "01. Find the Rifle";
            Objective1.color = Color.white;
        }

        if (obj2 == true)
        {
            Objective2.text = "2. Completed";
            Objective2.color = Color.green;
        }
        else
        {
            Objective2.text = "02. Locate the Villegers";
            Objective2.color = Color.white;
        }

        if (obj3 == true)
        {
            Objective3.text = "3. Completed";
            Objective3.color = Color.green;
        }
        else
        {
            Objective3.text = "03. Find Vechile";
            Objective3.color = Color.white;
        }

        if (obj4 == true)
        {
            Objective1.text = "4. Completed";
            Objective1.color = Color.green;
        }
        else
        {
            Objective4.text = "04. Get all Villegers into Vechilce";
            Objective4.color = Color.white;
        }
    }
}
