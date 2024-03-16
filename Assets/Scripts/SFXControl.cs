using UnityEngine;
using System.Collections;

public class SFXControl : MonoBehaviour
{
    [Tooltip("Total duration of the scaling animation.")]
    [SerializeField] float animationDuration = 0.2f;

    public Color hoverColor = new Color(0.729f, 0.729f, 0.729f); // RGB values for BABABA color
    private Color originalColor; // Original color of the button
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component

    private AudioManager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();

        // Get the SpriteRenderer component attached to the GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Store the original color of the button
        originalColor = spriteRenderer.color;
    }

    void Update()
    {
        if (!audioManager.isSFXPlay)
        {
            spriteRenderer.color = hoverColor;
        }

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

        audioManager.SFXOnOff();

        if (!audioManager.isSFXPlay)
        {
            spriteRenderer.color = hoverColor;
        }
        else
        {
            spriteRenderer.color = originalColor;
        }
    }
}