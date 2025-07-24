using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverToTitle : MonoBehaviour
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
        // Load the TitleScene
        string titleSceneName = "TitleScene"; // Replace with the actual name of your title scene
        if (Application.CanStreamedLevelBeLoaded(titleSceneName))
        {
            SceneManager.LoadScene(titleSceneName);
        }
        else
        {
            Debug.LogError($"Scene '{titleSceneName}' not found in Build Settings!");
        }
    }
}