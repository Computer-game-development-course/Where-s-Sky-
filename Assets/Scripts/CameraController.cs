using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject leftArrow;
    [SerializeField] GameObject rightArrow;
    [SerializeField] GameObject MapRoom = null;
    [SerializeField] GameObject SettingRoom = null;

    [SerializeField] GameObject PlayerLostRoom = null;
    [SerializeField] float cameraMoveSpeed = 5.0f;
    [SerializeField] float leftLimit = 0.2f;
    [SerializeField] float rightLimit = 5.2f;

    private bool isFlashingRight = false;
    private bool isFlashingLeft = false;

    void Update()
    {
        bool isRoomActive = (MapRoom != null && MapRoom.activeSelf) || (SettingRoom != null && SettingRoom.activeSelf) || (PlayerLostRoom != null && PlayerLostRoom.activeSelf);
        if (isRoomActive)
        {
            leftArrow.SetActive(false);
            rightArrow.SetActive(false);
            transform.position = new Vector3(0, 0, -10);
        }
        else
        {
            HandleCameraMovement();
            UpdateArrowVisibilityAndEffect();
        }
    }

    private void HandleCameraMovement()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (transform.position.x < rightLimit)
            {
                //Translate used to move the GameObject in the direction and distance of the provided Vector3
                transform.Translate(Vector3.right * cameraMoveSpeed * Time.deltaTime);
                if (!isFlashingRight) StartCoroutine(FlashArrow(rightArrow, true));
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (transform.position.x > leftLimit)
            {
                //Translate used to move the GameObject in the direction and distance of the provided Vector3
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

    // IEnumerator - is the return type for Unity coroutines, allowing to pause execution and resume it over several frames
    System.Collections.IEnumerator FlashArrow(GameObject arrow, bool isRight)
    {
        if (isRight) isFlashingRight = true;
        else isFlashingLeft = true;

        SpriteRenderer renderer = arrow.GetComponent<SpriteRenderer>();
        Color originalColor = renderer.color;
        float flashDuration = 0.5f;
        float timer = 0;

        while (timer <= flashDuration)
        {
            // Mathf.PingPong oscillate a value between 0 and flashDuration / 2 as timer increases.
            float lerpTime = Mathf.PingPong(timer, flashDuration / 2) / (flashDuration / 2);
            // Mathf.Lerp interpolates between two alpha values (0.2 and 1.0) based on lerpTime
            renderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(0.2f, 1f, lerpTime));
            timer += Time.deltaTime;
            yield return null;
        }

        // Reset to original color after flashing
        renderer.color = originalColor;

        if (isRight) isFlashingRight = false;
        else isFlashingLeft = false;
    }
}