using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject leftArrow;
    [SerializeField] GameObject rightArrow;
    [SerializeField] float cameraMoveSpeed = 5.0f;
    [SerializeField] float leftLimit = 0.2f;
    [SerializeField] float rightLimit = 5.2f;

    private bool isFlashingRight = false;
    private bool isFlashingLeft = false;

    void Update()
    {
        HandleCameraMovement();
        UpdateArrowVisibilityAndEffect();
    }

    private void HandleCameraMovement()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (transform.position.x < rightLimit)
            {
                transform.Translate(Vector3.right * cameraMoveSpeed * Time.deltaTime);
                if (!isFlashingRight) StartCoroutine(FlashArrow(rightArrow, true));
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (transform.position.x > leftLimit)
            {
                transform.Translate(Vector3.left * cameraMoveSpeed * Time.deltaTime);
                if (!isFlashingLeft) StartCoroutine(FlashArrow(leftArrow, false));
            }
        }
    }

    private void UpdateArrowVisibilityAndEffect()
    {
        rightArrow.SetActive(transform.position.x < rightLimit);
        leftArrow.SetActive(transform.position.x > leftLimit);
    }

    System.Collections.IEnumerator FlashArrow(GameObject arrow, bool isRight)
    {
        if (isRight) isFlashingRight = true;
        else isFlashingLeft = true;

        SpriteRenderer renderer = arrow.GetComponent<SpriteRenderer>();
        Color originalColor = renderer.color;
        float flashDuration = 0.5f; // Duration of the flash effect
        float timer = 0;

        while (timer <= flashDuration)
        {
            // Flash effect logic (simple example: toggle visibility)
            float lerpTime = Mathf.PingPong(timer, flashDuration / 2) / (flashDuration / 2);
            renderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(0.2f, 1f, lerpTime));
            timer += Time.deltaTime;
            yield return null;
        }

        renderer.color = originalColor; // Reset to original color after flashing

        if (isRight) isFlashingRight = false;
        else isFlashingLeft = false;
    }
}