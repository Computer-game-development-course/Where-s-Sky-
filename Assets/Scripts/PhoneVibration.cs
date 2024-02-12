using UnityEngine;

public class PhoneVibration : MonoBehaviour
{
    [Tooltip("Controls the strength of the vibration effect.")]
    [SerializeField] float vibrationIntensity = 0.1f;

    [Tooltip("Duration in seconds before the vibration stops.")]
    [SerializeField] float vibrationSpeed = 50.0f;

    private Vector3 originalPosition; // Stores the original position of the phone
    private bool isVibrating = false; // Flag to determine if the phone is currently vibrating

    void Start()
    {
        originalPosition = transform.position; // Store the original position at the start
        StartVibration(); // Start the vibration effect when the game starts
    }

    void Update()
    {
        if (isVibrating)
        {
            // Simulate vibration by applying small, random movements to the phone's position
            transform.position = originalPosition + Random.insideUnitSphere * vibrationIntensity;
        }
    }

    // Method to start the vibration effect
    public void StartVibration()
    {
        isVibrating = true;
        // Schedule the StopVibration method to be called after the specified vibrationSpeed duration
        Invoke("StopVibration", vibrationSpeed);
    }

    // Method to stop the vibration effect
    public void StopVibration()
    {
        isVibrating = false;
        // Reset the phone's position to its original position
        transform.position = originalPosition;
    }
}
