using UnityEngine;
using UnityEngine.SceneManagement;
public class SelectPlayer : MonoBehaviour
{
     public GameObject selectPlayer;
     public GameObject mainMenu;

    public void OnBackButton()
    {
        selectPlayer.SetActive(false);
        mainMenu.SetActive(true);
     }
    public void OnPlayer1()
    {
        SceneManager.LoadScene("ZombieLand");
    }
    public void OnPlayer2()
    {
        SceneManager.LoadScene("ZombieLand1");
    }
    public void OnPlayer3(){
        SceneManager.LoadScene("ZombieLand2");
    }
}
