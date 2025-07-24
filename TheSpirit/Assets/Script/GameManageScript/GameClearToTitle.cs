using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClearToTitle : MonoBehaviour
{
    private void Update()
    {
        // Check if the Space key is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadTitleScene();
        }
    }

    private void LoadTitleScene()
    {
        string sceneName = "TitleScene"; // Set the name of your Title Scene

        // Ensure the scene exists in the Build Settings
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneManager.LoadScene(sceneName); // Load the Title Scene
        }
        else
        {
            Debug.LogError($"Scene '{sceneName}' is not found in Build Settings!");
        }
    }
}

