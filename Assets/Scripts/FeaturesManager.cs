using UnityEngine;
using System.Collections;
using TMPro;

public class FeaturesManager : MonoBehaviour
{
    // private GameTimer gameTimer;
    private bool isFeatureActive = false;
    [SerializeField] string featureName;
    [SerializeField] int amount;
    [SerializeField] TextMeshPro amountText;

    private Vector3 originalScale;
    private float animationDuration = 0.2f;

    void Start()
    {
        // gameTimer = FindObjectOfType<GameTimer>();

        //amount = GameManager.Instance.getFeatureAmount(featureName);
        amountText.text = amount.ToString();

        originalScale = transform.localScale;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (GetComponent<Collider2D>().OverlapPoint(mousePos))
            {
                StartCoroutine(AnimateAndChangeScene());
                if (amount > 0)
                {
                    //GameManager.Instance.RemoveFeature(featureName);
                    ActivateFeature();
                }
            }
        }
    }

    void ActivateFeature()
    {
        if (featureName == "hourglass")
        {
            ActivateHourglassPowerUp();
        }
        else if (featureName == "snack")
        {
            ActivateSnackPowerUp();
        }
        else if (featureName == "x2")
        {
            ActivateX2PowerUp();
        }
        else if (featureName == "ball")
        {
            ActivateBallPowerUp();
        }
    }

    public void ActivateHourglassPowerUp()
    {
        if (!isFeatureActive)
        {
            isFeatureActive = true;
            // gameTimer.PauseTimerForDuration(10f); // Pause timer for 10 seconds
            Invoke(nameof(DeactivateHourglass), 10f); // Deactivate after 10 seconds
        }
    }

    private void DeactivateHourglass()
    {
        isFeatureActive = false;
    }

    public void ActivateSnackPowerUp()
    {
        // If cat in the current room player won
        amount = 0;
    }

    public void ActivateX2PowerUp()
    {
        // Double the coins earned
        // float timeLeft = GameManager.Instance.timeLeft;
        // LevelCompletion levelCompletion = FindObjectOfType<LevelCompletion>();
        // levelCompletion.x2Active = true;
        amount = 0;
    }

    public void ActivateBallPowerUp()
    {
        // If cat in the current room player won
        // amount = GameManager.Instance.getFeatureAmount(featureName);
        amount = 0;
    }

    IEnumerator AnimateAndChangeScene()
    {
        Vector3 targetScale = originalScale * 0.9f;

        float timer = 0;
        while (timer <= animationDuration / 2)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, timer / (animationDuration / 2));
            timer += Time.deltaTime;
            yield return null;
        }

        timer = 0;

        while (timer <= animationDuration / 2)
        {
            transform.localScale = Vector3.Lerp(targetScale, originalScale, timer / (animationDuration / 2));
            timer += Time.deltaTime;
            yield return null;
        }

    }

    // public void PauseTimerForDuration(float duration)
    // {
    //     StartCoroutine(PauseTimerCoroutine(duration));
    // }

    // private IEnumerator PauseTimerCoroutine(float duration)
    // {
    //     float originalTimeLeft = timeLeft;
    //     timeLeft = 0f; // Pause the timer

    //     // Update the timer display on the UI
    //     UpdateTimerText();

    //     yield return new WaitForSeconds(duration);

    //     timeLeft = originalTimeLeft; // Resume the timer
    //     UpdateTimerText();
    // }
}