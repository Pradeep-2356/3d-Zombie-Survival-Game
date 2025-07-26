using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject selectPlayer;
    public GameObject mainMenu;

    public void OnSelectPlayer()
    {
        selectPlayer.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void OnPlayButton()
    {
        SceneManager.LoadScene("ZombieLand");
    }

    public void OnQuitButton()
    {
        Debug.Log("Qutting Game...");
        Application.Quit();
    }
}
