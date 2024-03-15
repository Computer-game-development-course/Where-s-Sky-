using System.Collections;
using UnityEngine;

public class MoveDownAndUp : MonoBehaviour
{
    [Tooltip("The distance the object will move down.")]
    [SerializeField] float distance = 5.0f;

    [Tooltip("The speed of the movement.")]
    [SerializeField] float speed = 3.0f;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool movingDown = true;


    void Start()
    {
        startPosition = transform.position; // Store the initial position.
        targetPosition = new Vector3(startPosition.x, startPosition.y - distance, startPosition.z); // Calculate the target position.
        StartCoroutine(MoveUpDown()); // Start the coroutine.
    }

    IEnumerator MoveUpDown()
    {
        while (true) // Infinite loop to keep the object moving.
        {
            // Determine the direction of movement based on the current position and target position.
            Vector3 target = movingDown ? targetPosition : startPosition;

            // Move towards the target position.
            while (transform.position != target)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
                yield return null; // Wait until the next frame.
            }

            // Toggle the direction of movement.
            movingDown = !movingDown;

            // Wait for 1 second at the top and bottom positions before moving again.
            yield return new WaitForSeconds(1f);
        }
    }
}
