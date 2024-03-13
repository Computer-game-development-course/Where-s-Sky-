using UnityEngine;
using System.Collections;

public class FeaturesManager : MonoBehaviour
{
    private GameTimer gameTimer;
    private bool isHourglassActive = false;
    [SerializeField] string featureName;
    [SerializeField] int amount;
    [SerializeField] float animationDuration = 0.2f;
    public float moveDistance = 1.0f; // How far the arrow moves
    public float moveSpeed = 1.0f; // Speed of the movement

    public SpriteRenderer RedArrorDisplay; // RedArrorDisplay.enabled
    private Vector3 originalScale;

    void Start()
    {
        gameTimer = FindObjectOfType<GameTimer>();
        originalScale = transform.localScale;
        amount = GameManager.Instance.getFeatureAmount(featureName);
    }

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

                if (amount > 0)
                {
                    GameManager.Instance.RemoveFeature(featureName);
                    ActivateFeature();
                }
                else if (!(RedArrorDisplay.enabled))
                {
                    StartCoroutine(ShowRedArrow());
                }

            }
        }
    }

    void ActivateFeature()
    {
        if (featureName == "hourglass")
        {
            ActivateHourglassPowerUp();
        }
        else if (featureName == "snack")
        {
            ActivateSnackPowerUp();
        }
        else if (featureName == "x2")
        {
            ActivateX2PowerUp();
        }
        else if (featureName == "ball")
        {
            ActivateBallPowerUp();
        }
    }

    public void ActivateHourglassPowerUp()
    {
        if (!isHourglassActive)
        {
            isHourglassActive = true;
            gameTimer.PauseTimerForDuration(10f); // Pause timer for 10 seconds
            Invoke(nameof(DeactivateHourglass), 10f); // Deactivate after 10 seconds
        }
    }

    private void DeactivateHourglass()
    {
        isHourglassActive = false;
    }

    public void ActivateSnackPowerUp()
    {
        // If cat in the current room player won
        amount = GameManager.Instance.getFeatureAmount(featureName);


    }

    public void ActivateX2PowerUp()
    {
        // Double the coins earned
        amount = GameManager.Instance.getFeatureAmount(featureName);
    }

    public void ActivateBallPowerUp()
    {
        // If cat in the current room player won
        amount = GameManager.Instance.getFeatureAmount(featureName);
    }
    IEnumerator ShowRedArrow()
    {
        RedArrorDisplay.enabled = true;

        float timer = 0f;
        float direction = 1f; // Start moving right

        // Calculate the original position
        Vector3 originalPosition = RedArrorDisplay.transform.position;
        Vector3 targetPosition = originalPosition;

        while (timer < 2f)
        {
            // Update the timer by the elapsed time since last frame
            timer += Time.deltaTime;

            // Move the arrow back and forth by changing its target position based on the direction
            if (direction > 0) // Moving right
            {
                targetPosition = originalPosition + Vector3.right * moveDistance;
            }
            else // Moving left
            {
                targetPosition = originalPosition + Vector3.left * moveDistance;
            }

            // Move towards the target position
            RedArrorDisplay.transform.position = Vector3.MoveTowards(RedArrorDisplay.transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // If the arrow reaches the target position, change direction
            if (RedArrorDisplay.transform.position == targetPosition)
            {
                direction *= -1; // Change direction
                originalPosition = RedArrorDisplay.transform.position; // Update the original position for the new direction
            }

            yield return null;
        }

        RedArrorDisplay.enabled = false; // Disable the RedArrorDisplay after 5 seconds
    }

    IEnumerator AnimateAndChangeScene()
    {
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
    }
}