using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Tooltip("The GameObject that represents the left arrow visual or trigger.")]
    [SerializeField] GameObject leftArrow;

    [Tooltip("The GameObject that represents the right arrow visual or trigger.")]
    [SerializeField] GameObject rightArrow;

    [Tooltip("The speed at which the camera moves.")]
    [SerializeField] float cameraMoveSpeed = 5.0f;

    [Tooltip("The left boundary of the camera's movement.")]
    [SerializeField] float leftLimit = 0.2f;

    [Tooltip("The right boundary of the camera's movement.")]
    [SerializeField] float rightLimit = 5.2f;

    void Update()
    {
        // Calls the method responsible for handling camera movement.
        HandleCameraMovement();
    }

    private void HandleCameraMovement()
    {
        // Checks if the right arrow key is being pressed.
        if (Input.GetKey(KeyCode.RightArrow))
        {
            // Checks if the camera's position is within the allowed range to the right.
            if (transform.position.x < rightLimit)
            {
                // Moves the camera to the right at the specified speed, adjusted for frame rate.
                transform.Translate(Vector3.right * cameraMoveSpeed * Time.deltaTime);
            }
        }
        // Checks if the left arrow key is being pressed.
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            // Checks if the camera's position is within the allowed range to the left.
            if (transform.position.x > leftLimit)
            {
                // Moves the camera to the left at the specified speed, adjusted for frame rate.
                transform.Translate(Vector3.left * cameraMoveSpeed * Time.deltaTime);
            }
        }
    }
}