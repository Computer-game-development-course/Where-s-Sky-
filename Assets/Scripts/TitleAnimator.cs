using UnityEngine;
public class TitleAnimator : MonoBehaviour
{
    [Tooltip("Maximum rotation angle amplitude for the animation.")]
    [SerializeField] float angleAmplitude = 15.0f; // Maximum rotation angle

    [Tooltip("Speed of rotation. Higher values make the animation faster.")]
    [SerializeField] float frequency = 1f; // Speed of rotation

    private float originalRotation; // Original rotation angle

    void Start()
    {
        // Store the initial Z-axis rotation of the GameObject to use as a baseline for the animation.
        originalRotation = transform.eulerAngles.z; // Store the initial rotation
    }

    void Update()
    {
        // Calculate the new rotation angle using a sine wave for smooth oscillation around the Z-axis.
        // The sine wave generates values between -1 and 1, which are then scaled by the angleAmplitude.
        float angle = angleAmplitude * Mathf.Sin(frequency * Time.time);

        // Apply the calculated angle to the GameObject's original rotation. This creates a seesaw motion effect.
        // The GameObject rotates around its Z-axis, oscillating between -angleAmplitude and +angleAmplitude degrees from its original rotation.
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, originalRotation + angle);
    }
}