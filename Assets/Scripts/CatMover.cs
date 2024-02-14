using UnityEngine;

public class CatMover : MonoBehaviour
{
    [Tooltip("The speed at which the cat moves.")]
    [SerializeField] float moveSpeed = 2.0f;

    [Tooltip("The left boundary of the cat's movement.")]
    [SerializeField] float leftLimit = -5.0f;

    [Tooltip("The right boundary of the cat's movement.")]
    [SerializeField] float rightLimit = 5.0f;

    // Keeps track of whether the cat is currently moving to the right. If false, the cat is moving to the left.
    // This is used to change the direction of the cat's movement when it reaches a boundary.
    private bool movingRight = true;

    void Update()
    {
        // Check the direction the cat is currently moving in.
        if (movingRight)
        {
            // Check if the cat has reached or surpassed the right movement limit.
            if (transform.position.x >= rightLimit)
            {
                // If so, change direction to move left.
                movingRight = false;
            }
            else
            {
                // If not at the limit, continue moving to the right.
                // Movement is scaled by moveSpeed and the time since the last frame to ensure smooth motion.
                transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            }
        }
        else // If movingRight is false, the cat is moving to the left.
        {
            // Check if the cat has reached or surpassed the left movement limit.
            if (transform.position.x <= leftLimit)
            {
                // If so, change direction to move right.
                movingRight = true;
            }
            else
            {
                // If not at the limit, continue moving to the left.
                // Movement is scaled by moveSpeed and the time since the last frame to ensure smooth motion.
                transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            }
        }
    }
}