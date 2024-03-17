using UnityEngine;
using TMPro; // Namespace for TextMeshPro
using UnityEngine.SceneManagement;

public class UpdateCoins : MonoBehaviour
{
    private int coins = 0;

    [Tooltip("Name of the scene to load after the animation.")]
    [SerializeField] string sceneToLoad;

    void Update()
    {
        coins = GameManager.Instance.GetTotalCoins();
        UpdateTimerText();

        // Check for mouse button (left click) press
        if (Input.GetMouseButtonDown(0))
        {
            // Convert mouse position to world coordinates
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Check if the mouse click is over this object's collider
            if (GetComponent<Collider2D>().OverlapPoint(mousePos))
            {
                if (!string.IsNullOrEmpty(sceneToLoad))
                {
                    // Start the scale animation and scene change coroutine
                    SceneManager.LoadScene(sceneToLoad);
                }
            }
        }
    }


    void UpdateTimerText()
    {
        // This will find the TextMeshPro component among the children of time_visual
        TextMeshPro text = GetComponentInChildren<TextMeshPro>();

        if (text != null)
        {
            text.text = $"{coins}";
        }
    }
}
