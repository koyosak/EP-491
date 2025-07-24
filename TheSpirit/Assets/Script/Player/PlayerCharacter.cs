using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private int _health = 5;
    public int maxHealth = 5;

    [SerializeField] private TextMeshProUGUI healthText; // Reference to the TextMeshProUGUI element

    public int Health
    {
        get => _health;
        set
        {
            _health = Mathf.Clamp(value, 0, maxHealth); // Ensure health stays within valid range
            UpdateHealthUI(); // Update the health display
            if (_health <= 0)
            {
                OnPlayerDeath();
            }
        }
    }

    private void Start()
    {
        UpdateHealthUI(); // Initialize the UI with the current health
    }

    public void Hurt(int damage)
    {
        Health -= damage;
        Debug.Log($"Health: {Health}");
    }

    private void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = $"Health: {Health}";
        }
    }

    private void OnPlayerDeath()
    {
        Debug.Log("Player is dead. Loading Game Over Scene...");
        SceneManager.LoadScene("GameOverScene");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Hurt(1); // Reduce health by 1
        }
    }
}

