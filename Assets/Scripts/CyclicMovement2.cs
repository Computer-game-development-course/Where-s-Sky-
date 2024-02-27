using UnityEngine;

public class CyclicMovement2 : MonoBehaviour
{
    public float speed = 5.0f; // Movement speed to the right
    public float limitX = 6.5f; // The X-axis limit

    private Vector3 startPosition; // To store the starting position

    // Start is called before the first frame update
    void Start()
    {
        // Store the starting position of the GameObject
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Move the GameObject to the right
        transform.Translate(speed * Time.deltaTime, 0, 0);

        // Check if the GameObject has reached or passed the limit
        if (transform.position.x >= startPosition.x + limitX)
        {
            // If so, reset the position to the starting position
            transform.position = startPosition;
        }
    }
}
