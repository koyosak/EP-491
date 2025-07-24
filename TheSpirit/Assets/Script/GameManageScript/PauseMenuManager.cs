using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu; // Reference to the pause menu GameObject
    private bool isPaused = false;

    private void Start()
    {
        // Ensure the pause menu is hidden when the game starts
        pauseMenu.SetActive(false);
    }

    private void Update()
    {
        // Toggle pause menu with the 'P' key
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            // Show pause menu and pause the game
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;  // Pause the game
        }
        else
        {
            // Hide pause menu and resume the game
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;  // Resume the game
        }
    }

    public void ResumeGame()
    {
        // Resume the game when the resume button is clicked
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        // Quit the game when the quit button is clicked
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}

