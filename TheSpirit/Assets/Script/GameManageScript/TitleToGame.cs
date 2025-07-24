using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleToGame : MonoBehaviour
{
    private void Update()
    {
        // Check if the Enter key is pressed
        if (Input.GetKeyDown(KeyCode.Return))
        {
            LoadGameScene();
        }
    }

    private void LoadGameScene()
    {
        string sceneName = "GameScene"; // Set the name of your Game Scene

        // Ensure the scene exists in the Build Settings
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneManager.LoadScene(sceneName); // Load the Game Scene
        }
        else
        {
            Debug.LogError($"Scene '{sceneName}' is not found in Build Settings!");
        }
    }
}

