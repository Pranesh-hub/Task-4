using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerCollision : MonoBehaviour
{
    public PlayerMovement movement; // Reference to player movement script
    public Slider healthBar;        // UI slider for health
    public ScoreUpdate scoreUpdate; // Reference to ScoreUpdate script
    public float maxHealth = 100f;  // Maximum health value
    private float currentHealth;    // Current health value
    private float score = 0f;

    public TMP_Text points;
    void Start()
    {
        // Initialize health
        currentHealth = maxHealth;

        // Update the health bar UI
        UpdateHealthBar();
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.CompareTag("Obstacle"))
        {
            Debug.Log("Obstacle hit!");

            // Reduce health on collision
            TakeDamage(20f); // Adjust the damage value as needed
        }
        else if (collisionInfo.collider.CompareTag("Collectible"))
        {
            Debug.Log("Colletible added");
            score += 25f;
            Debug.Log(score);
            Destroy(collisionInfo.gameObject);
            points.text= $"Score: {score.ToString("0")}";
        }
    }

    void TakeDamage(float damage)
    {
        // Reduce current health
        currentHealth -= damage;

        // Clamp health to stay within valid range
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Update the health bar UI
        UpdateHealthBar();

        // Check if health is 0 and restart the game
        if (currentHealth <= 0)
        {
            movement.enabled = false;
            Debug.Log("Player is defeated!");

            // Stop the race
            scoreUpdate.StopRacing();

            // Restart the game
            FindAnyObjectByType<GameManager>().RestartGame();
        }
    }

    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.value = currentHealth / maxHealth;
        }
    }
}
