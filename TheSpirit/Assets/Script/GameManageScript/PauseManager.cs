using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu; // Reference to the pause menu GameObject
    [SerializeField] private MouseLookBehavior mouseLook; // Reference to the MouseLookBehavior script

    private bool _isPaused = false;

    private void Start()
    {
        // Ensure the pause menu is hidden when the game starts
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
    }

    private void Update()
    {
        // Check for the 'P' key press to toggle pause
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        _isPaused = !_isPaused;

        // Pause the game and show the pause menu
        if (_isPaused)
        {
            Time.timeScale = 0f;
            if (pauseMenu != null)
            {
                pauseMenu.SetActive(true); // Show the pause menu
            }

            // Disable mouse look when the game is paused
            if (mouseLook != null)
            {
                mouseLook.SetPauseState(true);
            }
        }
        // Resume the game and hide the pause menu
        else
        {
            Time.timeScale = 1f;
            if (pauseMenu != null)
            {
                pauseMenu.SetActive(false); // Hide the pause menu
            }

            // Enable mouse look when the game is resumed
            if (mouseLook != null)
            {
                mouseLook.SetPauseState(false);
            }
        }
    }
}

