using UnityEngine;
using System.Collections;

public class buyFeature : MonoBehaviour
{
    [Tooltip("")]
    [SerializeField] string featureName;

    [Tooltip("")]
    [SerializeField] int cost;

    [Tooltip("Total duration of the scaling animation.")]
    [SerializeField] float animationDuration = 0.2f;
    public float moveDistance = 1.0f; // How far the arrow moves
    public float moveSpeed = 1.0f; // Speed of the movement

    public SpriteRenderer RedArrorDisplay; // RedArrorDisplay.enabled
    private Vector3 originalScale;

    private bool noMoney = false;
    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (GetComponent<Collider2D>().OverlapPoint(mousePos))
            {
                StartCoroutine(AnimateAndChangeScene());

                int money = GameManager.Instance.GetTotalCoins();
                if (money >= cost)
                {
                    GameManager.Instance.RemoveCoins(cost);
                    GameManager.Instance.AddFeature(featureName);
                }
                else
                {
                    if (!noMoney && !(RedArrorDisplay.enabled))
                    {
                        StartCoroutine(ShowRedArrow());
                    }

                }
            }
        }
    }

    IEnumerator ShowRedArrow()
    {
        noMoney = true;
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
        noMoney = false;
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