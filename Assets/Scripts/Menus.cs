using UnityEngine;
using UnityEngine.SceneManagement;
public class Menus : MonoBehaviour
{
    [Header("All Menu's")]
    public GameObject pauseMenuUI;
    public GameObject EndGameMenuUI;
    public GameObject ObjectiveMenuUI;
    public static bool GameIsStopped = false;

    [Header("Heading Animators")]
    public Animator pauseHeadingAnimator;
    public Animator objectiveHeadingAnimator;
    public Animator endHeadingAnimator;

    [Header("Cinemachine & Canvas References")]
    public GameObject thirdPersonCinemachine;
    public GameObject aimCinemachine;
    public GameObject thirdPersonCanvas;
    public GameObject aimCanvas;

    void SetGameplayElementsActive(bool isActive)
    {
    thirdPersonCinemachine.SetActive(isActive);
    aimCinemachine.SetActive(isActive);
    thirdPersonCanvas.SetActive(isActive);
    aimCanvas.SetActive(isActive);
    }
    void PlayAnimationForPauseMenu(Animator animator)
    {
        if (animator != null)
        {
            // Ensure the PauseHeading is active before playing
            animator.gameObject.SetActive(true);

            // Reactivate Animator in case it was disabled
            animator.enabled = false;
            animator.enabled = true;

            // Play the animation from the beginning
            animator.Play("Pause-EndHeading", -1, 0f);
        }
    }

    void PlayAnimationForObjectiveMenu(Animator animator)
    {
        if (animator != null)
        {
            // Ensure the PauseHeading is active before playing
            animator.gameObject.SetActive(true);

            // Reactivate Animator in case it was disabled
            animator.enabled = false;
            animator.enabled = true;
            animator.Play("ObjectivesHeading", -1, 0f);
        }
    }

    public void ShowEndMenu()
    {
    if (EndGameMenuUI != null)
        EndGameMenuUI.SetActive(true);

    if (endHeadingAnimator != null)
    {
        endHeadingAnimator.gameObject.SetActive(true);
        endHeadingAnimator.enabled = false;
        endHeadingAnimator.enabled = true;
        endHeadingAnimator.Play("Pause-EndHeading", -1, 0f);
    }

    SetGameplayElementsActive(false);  // disable gameplay canvas & cinemachine
    Time.timeScale = 0f;
    GameIsStopped = true;
    Cursor.lockState = CursorLockMode.None;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsStopped)
            {
                resume();
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                pause();
                Cursor.lockState = CursorLockMode.None;
            }
        }
        else if (Input.GetKeyDown("m"))
        {
            if (GameIsStopped)
            {
                removeObjectives();
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                showObjectives();
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
    public void showObjectives()
    {
        ObjectiveMenuUI.SetActive(true);
        PlayAnimationForObjectiveMenu(objectiveHeadingAnimator);
        SetGameplayElementsActive(false);  //Disable all gameplay UI & cameras
        Time.timeScale = 0f;
        GameIsStopped = true;
    }

    public void removeObjectives()
    {
        ObjectiveMenuUI.SetActive(false);
        SetGameplayElementsActive(true);  // Re-enable gameplay UI & cameras
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        GameIsStopped = false;
    }

    public void pause()
    {
        pauseMenuUI.SetActive(true);
        PlayAnimationForPauseMenu(pauseHeadingAnimator);
        SetGameplayElementsActive(false);  //Disable all gameplay UI & cameras
        Time.timeScale = 0f;
        GameIsStopped = true;
    }
    public void resume()
    {
        pauseMenuUI.SetActive(false);
        SetGameplayElementsActive(true);  // Re-enable gameplay UI & cameras
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        GameIsStopped = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void QuitGame()
    {
        Debug.Log("Qutting Game...");
        Application.Quit();
    }
}
