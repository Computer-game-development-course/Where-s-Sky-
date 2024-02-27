using UnityEngine;
using TMPro; // Namespace for TextMeshPro
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    [Tooltip("Total time in seconds the player has to find the cat.")]
    [SerializeField] private float timeLeft = 40f; // Total time for the game (in seconds)
    [SerializeField] private float initial_time = 40f;

    [Tooltip("Names of the scenes where this object should not be destroyed.")]
    [SerializeField] string[] persistInScenes;

    private bool isOn = false;
    private bool isReset = false;

    void Start()
    {
        GameManager.Instance.InittTime();
    }
    void Update()
    {
        timeLeft = GameManager.Instance.timeLeft;
        initial_time = GameManager.Instance.initial_time;
        isReset = GameManager.Instance.isReset;

        if (!isReset)
        {
            if (ShouldPersistInCurrentScene())
            {
                if (!isOn)
                {
                    SpriteRenderer mainSpriteRenderer = GetComponent<SpriteRenderer>();
                    if (mainSpriteRenderer != null)
                    {
                        mainSpriteRenderer.enabled = true;
                    }

                    // Iterate through all child objects to set their visibility
                    foreach (Transform child in transform)
                    {
                        // Handle SpriteRenderer components (for most child objects)
                        SpriteRenderer sr = child.GetComponent<SpriteRenderer>();
                        if (sr != null)
                        {
                            sr.enabled = true;
                        }

                        // Handle the special case for the "TEXT" object with TextMeshPro
                        MeshRenderer tmpText = child.GetComponent<MeshRenderer>();
                        if (tmpText != null)
                        {
                            // For TextMeshPro components, we control visibility through the enabled property
                            tmpText.enabled = true;
                        }
                    }
                    isOn = true;
                }

                // Check if there is still time left
                if (timeLeft > 0)
                {
                    // Decrease the remaining time
                    timeLeft -= Time.deltaTime;
                    GameManager.Instance.SetTimeLeft(timeLeft);

                    // Update the timer display on the UI
                    UpdateTimerText();
                }
                else
                {
                    // If time runs out, load the 'lose' scene
                    LoadLoseScene();
                }
            }
            else
            {
                SpriteRenderer mainSpriteRenderer = GetComponent<SpriteRenderer>();
                if (mainSpriteRenderer != null)
                {
                    mainSpriteRenderer.enabled = false;
                }

                // Iterate through all child objects to set their visibility
                foreach (Transform child in transform)
                {
                    // Handle SpriteRenderer components (for most child objects)
                    SpriteRenderer sr = child.GetComponent<SpriteRenderer>();
                    if (sr != null)
                    {
                        sr.enabled = false;
                    }

                    // Handle the special case for the "TEXT" object with TextMeshPro
                    MeshRenderer tmpText = child.GetComponent<MeshRenderer>();
                    if (tmpText != null)
                    {
                        // For TextMeshPro components, control visibility through the enabled property
                        tmpText.enabled = false;
                    }
                }
                isOn = false;
            }
        }
        else
        {
            SpriteRenderer mainSpriteRenderer = GetComponent<SpriteRenderer>();
            if (mainSpriteRenderer != null)
            {
                mainSpriteRenderer.enabled = false;
            }

            // Iterate through all child objects to set their visibility
            foreach (Transform child in transform)
            {
                // Handle SpriteRenderer components (for most child objects)
                SpriteRenderer sr = child.GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    sr.enabled = false;
                }

                // Handle the special case for the "TEXT" object with TextMeshPro
                MeshRenderer tmpText = child.GetComponent<MeshRenderer>();
                if (tmpText != null)
                {
                    // For TextMeshPro components, control visibility through the enabled property
                    tmpText.enabled = false;
                }
            }
            isOn = false;
        }
    }

    private bool ShouldPersistInCurrentScene()
    {
        // Gets the name of the currently active scene.
        string currentSceneName = SceneManager.GetActiveScene().name;
        // Iterates over the list of scenes where this object should persist.
        foreach (string sceneName in persistInScenes)
        {
            if (currentSceneName.Equals(sceneName))
            {
                // If the current scene's name matches one in the list, the object should persist.
                return true;
            }
        }
        // If no match is found, the object should not persist in the current scene.
        return false;
    }

    public float GetInitialTime()
    {
        return initial_time;
    }

    public float GetTimeLeft()
    {
        return timeLeft;
    }

    public void SetInitialTime(float sec)
    {
        initial_time = sec;
    }

    public void SetTimeLeft(float sec)
    {
        timeLeft = sec;
    }

    void UpdateTimerText()
    {

        // This will find the TextMeshPro component among the children of time_visual
        TextMeshPro timerText = GetComponentInChildren<TextMeshPro>();

        if (timerText != null)
        {
            timerText.text = $"00:{timeLeft:00}";
        }
    }



    // Load the 'lose' scene
    void LoadLoseScene()
    {
        Debug.Log("Time's Up! You Lost!");
        SceneManager.LoadScene("lose");
    }
}