using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public Level level;
    [Tooltip("Total time in seconds the player has to find the cat.")]
    [SerializeField] private float timeLeft;
    [SerializeField] private float initialTime;

    [Tooltip("Names of the scenes where this object should not be destroyed.")]
    [SerializeField] string[] persistInScenes;
    [SerializeField] GameObject Timer;
    [SerializeField] GameObject LevelUpAsset;
    [SerializeField] GameObject Map;

    BoxCollider2D mapCollider;
    private FeaturesManager featuresManager;
    private bool isTimerPaused = false;
    private bool isTimerDisplayed = true;
    private GameObject[] stars;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            level = GameManager.Instance.currentLevel;

            // int levelId = GameManager.Instance.currentLevel;
            // level = GameManager.Instance.levels[levelId];
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Debug.Log("Start LevelManager");

        level = GameManager.Instance.currentLevel;

        initialTime = level.time;
        timeLeft = initialTime;

        Timer.SetActive(true);
        LevelUpAsset.SetActive(false);

        mapCollider = Map.GetComponent<BoxCollider2D>();

        if (level.rooms.Length > 1)
        {
            Map.SetActive(true);
        }
        else
        {
            Map.SetActive(false);
        }
    }

    void Update()
    {
        if (!isTimerPaused)
        {
            if (isTimerDisplayed)
            {
                if (timeLeft > 0)
                {
                    timeLeft -= Time.deltaTime;
                    UpdateTimerText();
                }
                else
                {
                    LoadLoseScene();
                }

                if (Input.GetMouseButtonDown(0))
                {
                    Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    if (mapCollider.OverlapPoint(mousePos))
                    {
                        pauseTimer();
                        hideTimer();
                        Map.SetActive(false);
                        SceneManager.LoadScene("map");
                    }
                }
            }
        }
    }

    void UpdateTimerText()
    {
        TextMeshPro timerText = GetComponentInChildren<TextMeshPro>();

        if (timerText != null)
        {
            timerText.text = $"00:{timeLeft:00}";
        }
    }


    public void pauseTimer()
    {
        isTimerPaused = true;
    }
    public void resumeTimer()
    {
        isTimerPaused = false;
    }

    public void displayTimer()
    {
        isTimerDisplayed = true;
        Timer.SetActive(true);
    }
    public void hideTimer()
    {
        isTimerDisplayed = false;

        Timer.SetActive(false);
    }

    void LoadLoseScene()
    {
        Debug.Log("Time's Up! You Lost!");
        SceneManager.LoadScene("lose");
    }

    public void playerWon()
    {
        pauseTimer();
        hideTimer();

        GameManager.Instance.currentLevel.isCompleted = true;

        stars = new GameObject[6];
        stars[0] = LevelUpAsset.transform.Find("star1").gameObject;
        stars[1] = LevelUpAsset.transform.Find("star2").gameObject;
        stars[2] = LevelUpAsset.transform.Find("star3").gameObject;
        stars[3] = LevelUpAsset.transform.Find("frameOfAStar1").gameObject;
        stars[4] = LevelUpAsset.transform.Find("frameOfAStar2").gameObject;
        stars[5] = LevelUpAsset.transform.Find("frameOfAStar3").gameObject;

        LevelUpAsset.SetActive(true);
        calculateScore();

        if (GameManager.Instance.currentLevel.id < GameManager.Instance.levels.Length - 1)
        {
            GameManager.Instance.levels[GameManager.Instance.currentLevel.id + 1].isOpen = true;
        }

    }

    private void calculateScore()
    {
        float ratio = timeLeft / initialTime;

        int starsEarned = 0;

        if (ratio < 0.25f)
        {
            stars[0].SetActive(true);
            stars[1].SetActive(false);
            stars[2].SetActive(false);
            starsEarned = 1;
        }
        else if (ratio >= 0.25f && ratio <= 0.75f)
        {
            stars[0].SetActive(true);
            stars[1].SetActive(true);
            stars[2].SetActive(false);

            starsEarned = 2;
        }
        else
        {
            stars[0].SetActive(true);
            stars[1].SetActive(true);
            stars[2].SetActive(true);

            starsEarned = 3;
        }

        stars[3].SetActive(true);
        stars[4].SetActive(true);
        stars[5].SetActive(true);

        GameManager.Instance.setLevelScore(level.id, starsEarned, true);

        int moneyEarned = (int)(timeLeft * 2);
        TextMeshPro coinsText = GetComponentInChildren<TextMeshPro>();

        if (coinsText != null)
        {
            coinsText.text = moneyEarned.ToString();
        }
        GameManager.Instance.AddCoins(moneyEarned);
    }

    public void destroyLevel()
    {
        initialTime = 0;
        timeLeft = 0;
        isTimerDisplayed = false;
        isTimerPaused = true;
        Timer.SetActive(false);
        LevelUpAsset.SetActive(false);
        level = null;
        Debug.Log("Level destroyed");
    }
}