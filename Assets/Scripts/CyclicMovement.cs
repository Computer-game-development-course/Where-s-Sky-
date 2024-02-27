using UnityEngine;
using System.Collections;

public class CyclicMovement : MonoBehaviour
{
    [Tooltip("Speed of the object's movement.")]
    [SerializeField] float speed = 2.0f; // Speed of the movement

    [Tooltip("Distance for the object to move left and right from its starting position.")]
    [SerializeField] float distance = 1.0f; // Distance to move left and right

    private Vector3 startPosition; // Starting position of the object
    private bool movingRight = true; // Flag to determine if the object is moving right or left

    void Start()
    {
        // Store the initial position of the object
        startPosition = transform.position;

        // Start the MoveObject coroutine to begin cyclic movement
        StartCoroutine(MoveObject());
    }

    IEnumerator MoveObject()
    {
        while (true) // Infinite loop to continuously move the object
        {
            // Calculate the target position based on the current direction
            Vector3 targetPosition = startPosition + (movingRight ? Vector3.right : Vector3.left) * distance;

            // Move towards the target position
            while (Vector3.Distance(transform.position, targetPosition) > 0.01f) // Check if the object is close enough to the target
            {
                // Move the object towards the target position at the specified speed
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                yield return null; // Wait for the next frame before continuing the loop
            }

            // Once the target is reached, switch the movement direction
            movingRight = !movingRight;
        }
    }
}

