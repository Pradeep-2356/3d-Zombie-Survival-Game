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

    private void OnEnable()
    {
        RefreshUI();
    }

    public void RefreshUI()
    {
        // Pull from central data holder
        Objective1.text = ObjectiveManager.obj1Done ? "1. Completed" : "01. Find the Rifle";
        Objective1.color = ObjectiveManager.obj1Done ? Color.green : Color.white;

        Objective2.text = ObjectiveManager.obj2Done ? "2. Completed" : "02. Locate the Villagers";
        Objective2.color = ObjectiveManager.obj2Done ? Color.green : Color.white;

        Objective3.text = ObjectiveManager.obj3Done ? "3. Completed" : "03. Find Vehicle";
        Objective3.color = ObjectiveManager.obj3Done ? Color.green : Color.white;

        Objective4.text = ObjectiveManager.obj4Done ? "4. Completed" : "04. Get all Villagers into Vehicle";
        Objective4.color = ObjectiveManager.obj4Done ? Color.green : Color.white;
    }
}
