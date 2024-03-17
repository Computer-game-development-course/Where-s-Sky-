using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowArrow : MonoBehaviour
{
    public float moveDistance = 1.0f; // How far the arrow moves
    public float moveSpeed = 1.0f; // Speed of the movement
    public float animationDuration = 0.3f;
    [SerializeField] GameObject RedArrorDisplay;

    void Start()
    {
        Invoke(nameof(ShowRedArrow), animationDuration);

    }

    IEnumerator ShowRedArrow()
    {
        float timer = 0f;
        float direction = 1f;

        Vector3 originalPosition = RedArrorDisplay.transform.position;
        Vector3 targetPosition = originalPosition;

        while (timer < animationDuration)
        {
            timer += Time.deltaTime;

            if (direction > 0)
            {
                targetPosition = originalPosition + Vector3.right * moveDistance;
            }
            else
            {
                targetPosition = originalPosition + Vector3.left * moveDistance;
            }

            RedArrorDisplay.transform.position = Vector3.MoveTowards(RedArrorDisplay.transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (RedArrorDisplay.transform.position == targetPosition)
            {
                direction *= -1;
                originalPosition = RedArrorDisplay.transform.position;
            }

            yield return null;
        }

    }


}
