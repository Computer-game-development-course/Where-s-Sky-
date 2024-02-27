using UnityEngine;

public class CatVisibilityManager : MonoBehaviour
{
    public int catStageNumber; // Assign this in the Inspector to match the cat to the stage

    // void Start()
    // {
    //     UpdateCatVisibility();
    // }
    void OnEnable()
    {
        UpdateCatVisibility();
    }

    // void OnEnable()
    // {
    //     // Optional: Subscribe to a delegate/event in GameManager that's called when stages are updated
    //     //GameManager.OnStageChanged += UpdateCatVisibility;
    // }

    // void OnDisable()
    // {
    //     // Optional: Unsubscribe from the GameManager event when this GameObject is disabled
    //     // GameManager.OnStageChanged -= UpdateCatVisibility;
    // }

    void UpdateCatVisibility()
    {
        // Check if this cat's stage matches the current stage in the GameManager
        bool shouldShow = GameManager.Instance.currentStage == catStageNumber;
        // Enable or disable the SpriteRenderer based on the stage match
        GetComponent<SpriteRenderer>().enabled = shouldShow;
    }
}
