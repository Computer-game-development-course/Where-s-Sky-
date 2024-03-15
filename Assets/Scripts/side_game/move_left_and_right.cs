using UnityEngine;

public class MoveLeftAndRight : MonoBehaviour
{
    [Tooltip("The maximum distance the object should move from the initial position.")]
    [SerializeField] float distance = 2.0f;

    [Tooltip("The speed at which the object should move.")]
    [SerializeField] float speed = 2.0f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position; // Store the initial position of the object.
    }

    void Update()
    {
        Vector3 offset = new Vector3(Mathf.Sin(Time.time * speed) * distance, 0, 0); // Calculate the offset using a sine wave.
        transform.position = startPosition + offset; // Apply the offset to the initial position to move left and right.
    }
}
