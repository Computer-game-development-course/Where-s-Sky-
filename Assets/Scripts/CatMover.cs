using UnityEngine;

public class CatMover : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public float leftLimit = -5.0f;
    public float rightLimit = 5.0f;

    private bool movingRight = true;

    void Update()
    {
        // Move the cat right if movingRight is true, left otherwise
        if (movingRight)
        {
            // If we've reached the right limit, switch direction
            if (transform.position.x >= rightLimit)
            {
                movingRight = false;
            }
            else
            {
                // Move to the right
                transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            }
        }
        else
        {
            // If we've reached the left limit, switch direction
            if (transform.position.x <= leftLimit)
            {
                movingRight = true;
            }
            else
            {
                // Move to the left
                transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            }
        }
    }
}
