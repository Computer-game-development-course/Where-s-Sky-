using UnityEngine;
using System.Collections;

public class StartLevel : MonoBehaviour
{
    [SerializeField] int levelNumber;
    [SerializeField] float animationDuration = 0.2f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (GetComponent<Collider2D>().OverlapPoint(mousePos))
            {
                AnimateAndChangeScene();
                if (levelNumber >= 0)
                {
                    GameManager.Instance.LoadLevel(levelNumber);
                }
            }
        }
    }

    IEnumerator AnimateAndChangeScene()
    {
        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = originalScale * 0.9f;

        float timer = 0;
        while (timer <= animationDuration / 2)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, timer / (animationDuration / 2));
            timer += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Reset timer for scaling up
        timer = 0;
        while (timer <= animationDuration / 2)
        {
            transform.localScale = Vector3.Lerp(targetScale, originalScale, timer / (animationDuration / 2));
            timer += Time.deltaTime;
            yield return null; // Wait for the next frame
        }
    }

}