using UnityEngine;
using TMPro;


public class LevelCompletion : MonoBehaviour
{
    public GameObject level_up_asse;
    //public GameObject timeVisual;
    //private GameTimer gameTimer; // Reference to the GameTimer script

    private GameObject[] stars;
    public TMP_Text moneyText;

    void Start()
    {
        //timeVisual.SetActive(true);
        // Find the GameTimer component in the scene
        //gameTimer = FindObjectOfType<GameTimer>(); // Adjust this if the structure is different

        // Initialize stars array
        stars = new GameObject[6];
        stars[0] = level_up_asse.transform.Find("star1").gameObject;
        stars[1] = level_up_asse.transform.Find("star2").gameObject;
        stars[2] = level_up_asse.transform.Find("star3").gameObject;
        stars[3] = level_up_asse.transform.Find("frameOfAStar1").gameObject;
        stars[4] = level_up_asse.transform.Find("frameOfAStar2").gameObject;
        stars[5] = level_up_asse.transform.Find("frameOfAStar3").gameObject;

        // Initially turn off the level up assets
        SetLevelUpAssetsVisibility(false);
    }

    void On_Mouse_Down()
    {
        // Calculate stars and money
        UpdateLevelUpAssets();

        // Show the level up assets
        SetLevelUpAssetsVisibility(true);

        //GameManager.Instance.ResetTime();
    }

    void Update()
    {
        // Check for mouse button (left click) press
        if (Input.GetMouseButtonDown(0))
        {
            // Convert mouse position to world coordinates
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Check if the mouse click is over this object's collider
            if (GetComponent<Collider2D>().OverlapPoint(mousePos))
            {
                // Start the scale animation and scene change coroutine
                On_Mouse_Down();
            }
        }
    }

    void UpdateLevelUpAssets()
    {
        // Check if GameTimer reference is available
        float timeLeft = GameManager.Instance.timeLeft;
        //Debug.Log(timeLeft);
        float initialTime = GameManager.Instance.initial_time;
        //Debug.Log(initialTime);

        GameManager.Instance.ResetTime();

        int levelNum = GameManager.Instance.currentStage;
        GameManager.Instance.OpenNextStage();

        // Calculate ratio and proceed as before
        float ratio = timeLeft / initialTime;
        // Determine number of stars
        if (ratio < 0.25f)
        {
            stars[0].SetActive(true);
            stars[1].SetActive(false);
            stars[2].SetActive(false);
            stars[3].SetActive(true);
            stars[4].SetActive(true);
            stars[5].SetActive(true);
            //gameTimer.SetStarsForLevel(1, 1);
            GameManager.Instance.SetStageStars(levelNum, 1);
        }
        else if (ratio >= 0.25f && ratio <= 0.75f)
        {
            stars[0].SetActive(true);
            stars[1].SetActive(true);
            stars[2].SetActive(false);
            stars[3].SetActive(true);
            stars[4].SetActive(true);
            stars[5].SetActive(true);
            //gameTimer.SetStarsForLevel(1, 2);
            GameManager.Instance.SetStageStars(levelNum, 2);
        }
        else
        {
            stars[0].SetActive(true);
            stars[1].SetActive(true);
            stars[2].SetActive(true);
            stars[3].SetActive(true);
            stars[4].SetActive(true);
            stars[5].SetActive(true);
            ///gameTimer.SetStarsForLevel(1, 3);
            GameManager.Instance.SetStageStars(levelNum, 3);
        }

        // Calculate money
        int moneyEarned = (int)(timeLeft * 2);
        moneyText.text = " +" + moneyEarned.ToString();
        GameManager.Instance.AddCoins(moneyEarned);
    }

    void SetLevelUpAssetsVisibility(bool isVisible)
    {
        // Set the visibility of the "level_up_asse" object itself
        SpriteRenderer mainSpriteRenderer = level_up_asse.GetComponent<SpriteRenderer>();
        if (mainSpriteRenderer != null)
        {
            mainSpriteRenderer.enabled = isVisible;
        }

        // Iterate through all child objects to set their visibility
        foreach (Transform child in level_up_asse.transform)
        {
            // Handle SpriteRenderer components (for most child objects)
            SpriteRenderer sr = child.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.enabled = isVisible;
            }

            // Handle the special case for the "TEXT" object with TextMeshPro
            MeshRenderer tmpText = child.GetComponent<MeshRenderer>();
            if (tmpText != null)
            {
                // For TextMeshPro components, control visibility through the enabled property
                tmpText.enabled = isVisible;
            }
        }
    }
}