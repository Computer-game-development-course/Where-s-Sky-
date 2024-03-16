using UnityEngine;
using TMPro; // Namespace for TextMeshPro
using UnityEngine.SceneManagement;

public class SideGameTimer : MonoBehaviour
{
    [Tooltip("Total time in seconds the player has to find the cat.")]
    [SerializeField] private float initial_time = 180f;

    private float timeLeft;

    void Start()
    {
        timeLeft = initial_time;
    }


    void Update()
    {
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
            LoadScene();
        }
    }

    void UpdateTimerText()
    {
        // This will find the TextMeshPro component among the children of time_visual
        TextMeshPro timerText = GetComponentInChildren<TextMeshPro>();

        if (timerText != null)
        {
            int minute = (int)(timeLeft / 60);
            int seconds = (int)(timeLeft % 60);
            timerText.text = $"{minute:00}:{seconds:00}";
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene("shop");
    }
}