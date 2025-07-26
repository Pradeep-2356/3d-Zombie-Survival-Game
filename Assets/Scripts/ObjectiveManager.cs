using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    public static bool obj1Done = false;
    public static bool obj2Done = false;
    public static bool obj3Done = false;
    public static bool obj4Done = false;

    public static void MarkObjective(int index)
    {
        switch (index)
        {
            case 1: obj1Done = true; break;
            case 2: obj2Done = true; break;
            case 3: obj3Done = true; break;
            case 4: obj4Done = true; break;
        }

        // Refresh UI if it exists and is visible
        if (ObjectivesComplete.occurence != null && ObjectivesComplete.occurence.isActiveAndEnabled)
        {
            ObjectivesComplete.occurence.RefreshUI();
        }
    }
}
