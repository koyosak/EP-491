using UnityEngine;

public class QuitGame : MonoBehaviour
{
    private void Update()
    {
        // Check if the Q key is pressed
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Quit();
        }
    }

    // Method to quit the game
    private void Quit()
    {
        Debug.Log("Quit the game!");
        Application.Quit();

#if UNITY_EDITOR
        // If running in the Unity Editor, stop play mode
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}