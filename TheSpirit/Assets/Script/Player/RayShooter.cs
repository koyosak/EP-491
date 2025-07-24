using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RayShooter : MonoBehaviour
{
    private Camera _cam;

    bool _enemyStateMachine;
    private int _defeatedEnemies = 0; // Variable to track defeated enemies

    [SerializeField] private int _requiredDefeats = 5; // Number of enemies to defeat to win

    // Reference to the UI text components
    [SerializeField] private TextMeshProUGUI _defeatedEnemiesText;
    [SerializeField] private TextMeshProUGUI _shotsLeftText;

    public int ShotsLeft { get; private set; } = 5; // Public variable to track remaining shots to defeat the enemy

    private void Start()
    {
        _cam = GetComponent<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        GameObject enemy = GameObject.Find("Enemy_StateMachine(Clone)");
        _enemyStateMachine = enemy == null;

        UpdateUI();
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 100, Color.white);

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 point = new(_cam.pixelWidth / 2, _cam.pixelHeight / 2, 0);
            Ray ray = _cam.ScreenPointToRay(point);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                GameObject hitObj = hit.transform.gameObject;
                ReactiveTarget target = hitObj.GetComponent<ReactiveTarget>();

                if (target != null)
                {
                    if (!_enemyStateMachine)
                        target.ReactToHit();
                    else
                        hitObj.GetComponent<EnemyStateMachine>().ReactToHit();
                }
                else
                {
                    StartCoroutine(SphereIndicator(hit.point));
                }
            }
        }
    }

    public void DecreaseShotsToDefeatEnemy()
    {
        if (ShotsLeft > 0)
        {
            ShotsLeft--; // Decrease the remaining shots
        }

        UpdateUI();
    }

    public void IncrementDefeatedEnemies()
    {
        _defeatedEnemies++;
        UpdateUI();

        // Check if player has defeated the required number of enemies
        if (_defeatedEnemies >= _requiredDefeats)
        {
            LoadGameClearScene();
        }
    }

    private void UpdateUI()
    {
        if (_defeatedEnemiesText != null)
        {
            _defeatedEnemiesText.text = "Level: " + (_defeatedEnemies);
        }

        //if (_shotsLeftText != null)
        //{
        //    _shotsLeftText.text = "Shots Left: " + ShotsLeft;
        //}
    }

    void OnGUI()
    {
        int size = 30; // Size of the text
        float posX = _cam.pixelWidth / 2 - size / 4;
        float posY = _cam.pixelHeight / 2 - size / 2;

        GUI.contentColor = Color.white;
        GUI.Label(new Rect(posX, posY, size, size), "*");
    }

    IEnumerator SphereIndicator(Vector3 pos)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;

        yield return new WaitForSeconds(1);
        Destroy(sphere);
    }

    private void LoadGameClearScene()
    {
        string sceneName = "GameClearScene"; // Name of the scene to load

        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneManager.LoadScene(sceneName); // Load GameClearScene
        }
        else
        {
            Debug.LogError($"Scene '{sceneName}' not found in Build Settings!");
        }
    }
}




