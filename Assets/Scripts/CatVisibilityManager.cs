using UnityEngine;

public class CatVisibilityManager : MonoBehaviour
{
    [SerializeField] int catStageNumber;
    private SpriteRenderer spriteRenderer;
    private PolygonCollider2D polygonCollider;

    void Awake()
    {
        // Cache the components on awake
        spriteRenderer = GetComponent<SpriteRenderer>();
        polygonCollider = GetComponent<PolygonCollider2D>();
    }

    void OnEnable()
    {
        UpdateCatVisibility();
    }

    void UpdateCatVisibility()
    {
        // Check if this cat's stage matches the current stage in the GameManager
        bool shouldShow = GameManager.Instance.currentStage == catStageNumber;

        // Enable or disable the SpriteRenderer and PolygonCollider2D based on the stage match
        spriteRenderer.enabled = shouldShow;
        polygonCollider.enabled = shouldShow;
    }
}
