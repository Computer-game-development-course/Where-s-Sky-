using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [Tooltip("The center point of the oscillation, relative to the starting position.")]
    [SerializeField]
    private Vector3 center;

    [Tooltip("The maximum distance the fish moves from the center point.")]
    [SerializeField]
    private float maxDistance = 2.0f;

    [Tooltip("How fast the fish oscillates (frequency).")]
    [SerializeField]
    private float velocity = 1.0f;

    // The direction vector of the oscillation
    private Vector3 direction = new Vector3(1, 0, 0);

    // A Transform component to hold the reference to the fish's transform
    private Transform fishTransform;

    // To store the previous value of the sine wave
    private float previousSinWave;

    // Start is called before the first frame update
    void Start()
    {

        // Output to the console
        Debug.Log("Start");

        // Get the Transform component of the current fish
        fishTransform = GetComponent<Transform>();

        // Set the center point as the current position of the fish
        center = fishTransform.position;

        // Calculate the initial sine wave value based on the current time and velocity
        previousSinWave = Mathf.Sin(Time.time * velocity);
    }

    // Update is called once per frame
    void Update()
    {
        // Output to the console
        Debug.Log("Update");

        // Calculate the current sine wave value, which return a value between -1 and 1
        float CurrentsinWave = Mathf.Sin(Time.time * velocity);

        // Flip the fish direction based on the direction of the movement
        if (CurrentsinWave < previousSinWave && fishTransform.localScale.x > 0 ||
            CurrentsinWave > previousSinWave && fishTransform.localScale.x < 0)
        {
            fishTransform.localScale = new Vector3(-fishTransform.localScale.x, fishTransform.localScale.y, fishTransform.localScale.z);
        }

        // Update the previousSinWave value for the next frame
        previousSinWave = CurrentsinWave;

        // Determine the distance to move for this frame
        float moveStep = CurrentsinWave * maxDistance;

        // Calculate the new position
        fishTransform.position = center + (moveStep * direction.normalized);
    }
}