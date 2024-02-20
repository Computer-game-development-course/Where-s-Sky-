using UnityEngine;
using TMPro; // Namespace for TextMeshPro
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    [Tooltip("TextMeshPro component for displaying the timer.")]
    [SerializeField] private TextMeshPro timerText;

    [Tooltip("Total time in seconds the player has to find the cat.")]
    [SerializeField] private float timeLeft = 20f; // Total time for the game (in seconds)
    [SerializeField] private float initial_time = 20f;

    public class Level
    {
        public bool[] level_stars = new bool[3];
    }

    public Level[] levels; // Corrected the class name here

    void Start()
    {
        // Initialize the levels array with 30 Level objects
        levels = new Level[30];
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i] = new Level();
        }
    }

    public float GetInitialTime()
    {
        return initial_time;
    }

    public float GetTimeLeft()
    {
        return timeLeft;
    }

    public void SetStarsForLevel(int levelNumber, int starsEarned)
    {
        // Reset all stars to false for the level
        for (int i = 0; i < levels[levelNumber - 1].level_stars.Length; i++)
        {
            levels[levelNumber - 1].level_stars[i] = i < starsEarned;
        }
    }

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
        // Format the timer string to always show two digits
        timerText.text = $"00:{timeLeft:00}";
    }

    // Load the 'lose' scene
    void LoadLoseScene()
    {
        Debug.Log("Time's Up! You Lost!");
        SceneManager.LoadScene("lose");
    }
}