using UnityEngine;

public class TitleAnimator : MonoBehaviour
{
    public float amplitude = 0.5f;
    public float frequency = 1f;

    private Vector3 startPos;
    private float tempVal;

    void Start()
    {
        startPos = transform.position;
        tempVal = startPos.y;
    }

    void Update()
    {
        // Bouncing animation with Sine wave for smoothness
        tempVal = startPos.y + amplitude * Mathf.Sin(frequency * Time.time);
        transform.position = new Vector3(startPos.x, tempVal, startPos.z);
    }
}