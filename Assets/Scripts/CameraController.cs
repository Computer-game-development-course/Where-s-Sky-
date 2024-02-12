using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject leftArrow;
    public GameObject rightArrow;
    public float cameraMoveSpeed = 5.0f;
    public float leftLimit = 0.2f;
    public float rightLimit = 5.2f;

    void Update()
    {
        HandleCameraMovement();
    }

    private void HandleCameraMovement()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (transform.position.x < rightLimit)
            {
                transform.Translate(Vector3.right * cameraMoveSpeed * Time.deltaTime);
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (transform.position.x > leftLimit)
            {
                transform.Translate(Vector3.left * cameraMoveSpeed * Time.deltaTime);
            }
        }
    }
}
