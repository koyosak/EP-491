using UnityEngine;
using UnityEngine.UI; 
using TMPro;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText; // Reference to the Text UI element
    [SerializeField] private float maxDuration = 300f; // Maximum duration in seconds (optional)

    private float _timeElapsed; // Tracks elapsed time
    private bool _timerRunning;

    private void Start()
    {
        _timeElapsed = 0f; // Start from 00:00
        _timerRunning = true; // Start the timer
        UpdateTimerDisplay(_timeElapsed);
    }

    private void Update()
    {
        if (_timerRunning)
        {
            _timeElapsed += Time.deltaTime;

            if (_timeElapsed >= maxDuration) // Optional limit
            {
                _timeElapsed = maxDuration;
                _timerRunning = false; // Stop if reaching max duration
                TimerEnded();
            }

            UpdateTimerDisplay(_timeElapsed);
        }
    }

    private void UpdateTimerDisplay(float timeToDisplay)
    {
        // Format the time as MM:SS
        int minutes = Mathf.FloorToInt(timeToDisplay / 60);
        int seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void TimerEnded()
    {
        Debug.Log("Timer stopped at max duration!");
        // Add behavior for when the timer stops (optional)
    }
}
