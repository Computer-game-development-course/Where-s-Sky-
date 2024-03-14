using System.Collections;
using UnityEngine;

public class CatPeek : MonoBehaviour
{
    public float minHeight; // The height at which the cat is fully hidden
    public float maxHeight; // The height at which the cat is fully visible
    public float peekDuration = 2f; // How long it takes to peek out
    public float hideDuration = 1f; // How fast the cat hides
    private float targetYPosition;
    private float secToWait = 2f;

    void Start()
    {
        targetYPosition = transform.position.y;
        StartCoroutine(PeekRoutine());
    }

    void Update()
    {
        // Smoothly move the cat up and down towards the target y position
        float newY = Mathf.MoveTowards(transform.position.y, targetYPosition, Time.deltaTime * (targetYPosition == minHeight ? hideDuration : peekDuration));
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    IEnumerator PeekRoutine()
    {
        while (true)
        {
            // Wait for a random amount of time before peeking
            yield return new WaitForSeconds(Random.Range(secToWait - 1, secToWait + 1));

            // Move up to peek
            targetYPosition = maxHeight;
            yield return new WaitForSeconds(secToWait); // Stay peeked out for a bit

            // Move down to hide
            targetYPosition = minHeight;
            yield return new WaitForSeconds(secToWait); // Stay hidden for a bit
        }
    }
}
