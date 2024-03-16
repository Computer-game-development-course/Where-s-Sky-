using UnityEngine;

public class CatVisibilityManager : MonoBehaviour
{
    [SerializeField] int catStageNumber;
    private SpriteRenderer spriteRenderer;
    private PolygonCollider2D polygonCollider;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        polygonCollider = GetComponent<PolygonCollider2D>();
    }

    void OnEnable()
    {
        UpdateCatVisibility();
    }

    void UpdateCatVisibility()
    {
        bool shouldShow = GameManager.Instance.currentLevel.id == catStageNumber;

        // Enable or disable the SpriteRenderer and PolygonCollider2D based on the stage match
        spriteRenderer.enabled = shouldShow;
        polygonCollider.enabled = shouldShow;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (GetComponent<Collider2D>().OverlapPoint(mousePos))
            {
                LevelManager.Instance.playerWon();
            }
        }
    }
}
