using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreUpdate : MonoBehaviour
{
    public TMP_Text timerText;   // Text object to display the timer
    public float totalTime = 30f; // Total time for the countdown
    public bool isRacing = true; // Boolean to track the racing state
    private float timeRemaining; // Tracks the remaining time

    void Start()
    {
        // Initialize the timer
        timeRemaining = totalTime;

        // Ensure the timerText is assigned
        if (timerText == null)
        {
            Debug.LogError("TimerText is not assigned in the Inspector.");
        }

        // Ensure the totalTime is a positive number
        if (totalTime <= 0)
        {
            Debug.LogError("Total time should be greater than zero.");
            totalTime = 30f; // Reset to default value
        }

        //Debug.Log($"Timer initialized with {timeRemaining} seconds.");
    }

    void Update()
    {
        isRacing = true;
        if (isRacing)
        {
            if (timeRemaining > 0)
            {
                // Decrease the time and update the UI
                timeRemaining -= Time.deltaTime;
                timeRemaining = Mathf.Max(0, timeRemaining); // Prevent negative time

                // Print time remaining in the console
                //Debug.Log("Time Left: ${timeRemaining:F2} seconds");

                // Update the timer UI
                if (timerText != null)
                {
                    timerText.text = $"Time Left: {timeRemaining.ToString("0.00")} s";

                    // Debug log to ensure the text is updated
                    //Debug.Log($"Timer Text Updated: {timerText.text}");
                }
            }
            else
            {
                // Time has run out
                Debug.Log("Time's up!");
                StopRacing();
            }
        }
    }

    public void StopRacing()
    {
        if (isRacing)
        {
            isRacing = false;

            // Log the race end
            Debug.Log("Race ended. Restarting the game...");

            // Restart the game
            GameManager gameManager = FindAnyObjectByType<GameManager>();
            if (gameManager != null)
            {
                gameManager.RestartGame();
            }
            else
            {
                Debug.LogError("GameManager not found in the scene!");
            }
        }
    }
}
