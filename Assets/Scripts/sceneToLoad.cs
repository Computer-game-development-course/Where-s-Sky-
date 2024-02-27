using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AssetClick : MonoBehaviour
{
    [Tooltip("Name of the scene to load after the animation.")]
    [SerializeField] string sceneToLoad;

    [Tooltip("Total duration of the scaling animation.")]
    [SerializeField] float animationDuration = 0.5f;

    private GameTimer gameTimer; // Reference to the GameTimer script

    void Update()
    {
        // Check for mouse button (left click) press
        if (Input.GetMouseButtonDown(0))
        {
            // Convert mouse position to world coordinates
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Check if the mouse click is over this object's collider
            if (GetComponent<Collider2D>().OverlapPoint(mousePos))
            {
                // Start the scale animation and scene change coroutine
                StartCoroutine(AnimateAndChangeScene());
            }
        }
    }

    IEnumerator AnimateAndChangeScene()
    {
        // Store the original scale of the GameObject
        Vector3 originalScale = transform.localScale;

        // Target scale is set to 90% of the original scale
        Vector3 targetScale = originalScale * 0.9f;

        // Scale down the GameObject to the target scale
        float timer = 0;
        while (timer <= animationDuration / 2)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, timer / (animationDuration / 2));
            timer += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Reset timer for scaling up
        timer = 0;

        // Scale the GameObject back to its original scale
        while (timer <= animationDuration / 2)
        {
            transform.localScale = Vector3.Lerp(targetScale, originalScale, timer / (animationDuration / 2));
            timer += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        gameTimer = FindObjectOfType<GameTimer>();
        if (gameTimer != null)
        {
            if (sceneToLoad.Equals("room1"))
            {
                if (gameObject.name.Equals("play_button") || gameObject.name.Equals("continue_buton"))
                {
                    GameManager.Instance.currentStage = GameManager.Instance.lastToOpen;
                    GameManager.Instance.InittTime();
                }
                else
                {
                    // Assuming 'gameObject' is the current GameObject this script is attached to,
                    // or you have a reference to the GameObject you want to check.
                    for (int i = 0; i < 30; i++)
                    {
                        // Construct the expected name string
                        string expectedName = $"Level {i + 1}";

                        // Check if the name of the object matches the expected name
                        if (gameObject.name.Equals(expectedName))
                        {
                            // Set the current stage in GameManager to the matched level
                            GameManager.Instance.currentStage = i;
                            GameManager.Instance.InittTime();
                            break; // Exit the loop once the match is found
                        }
                    }
                }
            }
        }

        // Load the specified scene after the animation is complete
        SceneManager.LoadScene(sceneToLoad);
    }
}