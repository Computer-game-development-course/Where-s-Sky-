using UnityEngine;
using TMPro; // Namespace for TextMeshPro
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    [Tooltip("TextMeshPro component for displaying the timer.")]
    [SerializeField] TextMeshPro timerText;

    [Tooltip("Total time in seconds the player has to find the cat.")]
    [SerializeField] float timeLeft = 20f; // Total time for the game (in seconds)

    void Update()
    {
        // Check if there is still time left
        if (timeLeft > 0)
        {
            // Decrease the remaining time
            timeLeft -= Time.deltaTime;
            // Update the timer display on the UI
            UpdateTimerText();
        }
        else
        {
            // If time runs out, load the 'lose' scene
            LoadLoseScene();
        }
    }

    // Updates the timer text in the UI
    void UpdateTimerText()
    {
        // Set the timerText to show the rounded remaining time
        timerText.text = "Time left: " + Mathf.RoundToInt(timeLeft).ToString();
    }

    // Load the 'lose' scene
    void LoadLoseScene()
    {
        // Load the scene named 'lose'
        SceneManager.LoadScene("lose");
        Debug.Log("Time's Up! You Lost!");
    }
}
