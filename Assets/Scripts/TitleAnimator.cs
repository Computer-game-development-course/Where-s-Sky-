// using UnityEngine;

// public class TitleAnimator : MonoBehaviour
// {
//     public float amplitude = 0.5f;
//     public float frequency = 1f;

//     private Vector3 startPos;
//     private float tempVal;

//     void Start()
//     {
//         startPos = transform.position;
//         tempVal = startPos.y;
//     }

//     void Update()
//     {
//         // Bouncing animation with Sine wave for smoothness
//         tempVal = startPos.y + amplitude * Mathf.Sin(frequency * Time.time);
//         transform.position = new Vector3(startPos.x, tempVal, startPos.z);
//     }
// }
using UnityEngine;

public class TitleAnimator : MonoBehaviour
{
    public float angleAmplitude = 15.0f; // Maximum rotation angle
    public float frequency = 1f; // Speed of rotation

    private float originalRotation; // Original rotation angle

    void Start()
    {
        originalRotation = transform.eulerAngles.z; // Store the initial rotation
    }

    void Update()
    {
        // Calculate the rotation angle with a Sine wave for a smooth seesaw motion
        float angle = angleAmplitude * Mathf.Sin(frequency * Time.time);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, originalRotation + angle);
    }
}
