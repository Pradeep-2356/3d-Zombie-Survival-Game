using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InstructionUI : MonoBehaviour
{
    [Header("Instruction UI Elements")]
    public GameObject instructionCanvas;
    public GameObject characterInstructionPanel;
    public Text instructionText; // Use TMP_Text if using TextMeshPro

    void Start()
    {
        StartCoroutine(ShowCharacterInstructions());
    }

    IEnumerator ShowCharacterInstructions()
    {
        instructionCanvas.SetActive(true);
        characterInstructionPanel.SetActive(true);

        instructionText.text = "Use W A S D buttons for movement, and SPACE button to Jump";
        yield return new WaitForSeconds(4f);

        instructionText.text = "Holding SHIFT button with W A S D you can run";
        yield return new WaitForSeconds(4f);

        instructionText.text = "Use LMB to attack zombies and RMB to aim and press M to see Objectives";
        yield return new WaitForSeconds(4f);

        characterInstructionPanel.SetActive(false);
    }
}
