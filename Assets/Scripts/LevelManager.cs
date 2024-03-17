using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;

[System.Serializable]
public class Room
{
    public GameObject room;
    public bool isCatThere;
    public bool isRoomOpen;
    public bool isRoomAvailable;
}

public class LevelManager : MonoBehaviour
{
    public Level level;
    [SerializeField] GameObject[] roomObjects;

    [Tooltip("Total time in seconds the player has to find the cat.")]
    [SerializeField] private float timeLeft;
    [SerializeField] private float initialTime;
    [SerializeField] GameObject Timer;
    [SerializeField] GameObject MapButton;
    [SerializeField] GameObject SettingButton;
    [SerializeField] Collider2D SettingButtonCollider;
    [SerializeField] GameObject MapRoom;
    [SerializeField] GameObject[] MapRoomsMenu;
    [SerializeField] GameObject Cat;
    [SerializeField] GameObject SettingMenu;
    [SerializeField] GameObject PlayerLostMenu;
    [SerializeField] Collider2D RetryButtonCollider;
    [SerializeField] Collider2D QuitButtonCollider;
    [SerializeField] Collider2D ContinueButtonCollider;
    [SerializeField] Collider2D ReplayButtonCollider;
    [SerializeField] GameObject FeaturesMenu;
    [SerializeField] GameObject Hourglass;
    [SerializeField] GameObject Snack;
    [SerializeField] GameObject X2;
    [SerializeField] GameObject Ball;
    private PolygonCollider2D CatCollider;
    private Dictionary<string, Room> rooms = new Dictionary<string, Room>();
    private string currentRoomKey;
    private string catRoomKey;
    BoxCollider2D mapCollider;
    private bool isTimerPaused = false;
    private bool isTimerDisplayed = true;
    private bool X2Activated = false;

    void Start()
    {
        level = GameManager.Instance.currentLevel;
        UpdateFeaturesState(Hourglass, GameManager.Instance.features.hourglass);
        UpdateFeaturesState(Snack, GameManager.Instance.features.snack);
        UpdateFeaturesState(X2, GameManager.Instance.features.x2);
        UpdateFeaturesState(Ball, GameManager.Instance.features.ball);

        foreach (GameObject room in roomObjects)
        {
            room.SetActive(false);
            bool isRoomAvailable = false;
            for (int i = 0; i < level.rooms.Length; i++)
            {
                if (room.name == level.rooms[i])
                {
                    isRoomAvailable = true;
                }
            }

            rooms.Add(room.name, new Room
            {
                room = room,
                isCatThere = false,
                isRoomOpen = false,
                isRoomAvailable = isRoomAvailable
            });
        }

        placeCatInRoom();

        Room firstRoom = rooms[level.rooms[0]];
        firstRoom.room.SetActive(true);
        firstRoom.isRoomOpen = true;
        currentRoomKey = level.rooms[0];

        initialTime = level.time;
        timeLeft = initialTime;

        Timer.SetActive(true);
        SettingButton.SetActive(true);
        mapCollider = MapButton.GetComponent<BoxCollider2D>();

        MapButton.SetActive(level.rooms.Length > 1);

        Cat.SetActive(currentRoomKey == catRoomKey);

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
                        openMap();
                    }
                    else if (CatCollider.OverlapPoint(mousePos))
                    {
                        playerWon();
                    }
                    else if (SettingButtonCollider.OverlapPoint(mousePos))
                    {
                        pauseTimer();
                        hideTimer();
                        Room currentRoom = rooms[currentRoomKey];
                        currentRoom.room.SetActive(false);
                        MapButton.SetActive(false);
                        Cat.SetActive(false);
                        SettingButton.SetActive(false);
                        FeaturesMenu.SetActive(false);
                        SettingMenu.SetActive(true);
                    }
                    else if (FeaturesMenu.activeSelf)
                    {
                        if (Hourglass.GetComponent<BoxCollider2D>().OverlapPoint(mousePos))
                        {
                            ActivateFeature("hourglass");
                        }
                        if (Snack.GetComponent<BoxCollider2D>().OverlapPoint(mousePos))
                        {
                            ActivateFeature("snack");
                        }
                        if (X2.GetComponent<BoxCollider2D>().OverlapPoint(mousePos))
                        {
                            ActivateFeature("x2");
                        }
                        if (Ball.GetComponent<BoxCollider2D>().OverlapPoint(mousePos))
                        {
                            ActivateFeature("ball");
                        }
                    }

                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                if (MapRoom.activeSelf)
                {
                    foreach (GameObject room in MapRoomsMenu)
                    {
                        if (room != null)
                        {
                            GameObject roomCollider = room.transform.Find("RoomCollider").gameObject;
                            if (roomCollider != null)
                            {
                                if (roomCollider.GetComponent<Collider2D>().OverlapPoint(mousePos))
                                {
                                    Room selectedRoom = rooms[room.name];
                                    if (selectedRoom.isRoomAvailable)
                                    {
                                        selectedRoom.room.SetActive(true);
                                        currentRoomKey = room.name;
                                        MapRoom.SetActive(false);
                                        MapButton.SetActive(true);
                                        Timer.SetActive(true);
                                        FeaturesMenu.SetActive(true);
                                        SettingButton.SetActive(true);
                                        isTimerDisplayed = true;
                                        isTimerPaused = false;

                                        if (currentRoomKey == catRoomKey)
                                        {
                                            Cat.SetActive(true);
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
                else if (SettingMenu.activeSelf)
                {
                    if (ContinueButtonCollider.OverlapPoint(mousePos))
                    {
                        SettingMenu.SetActive(false);
                        Room currentRoom = rooms[currentRoomKey];
                        currentRoom.room.SetActive(true);
                        if (level.rooms.Length > 1)
                        {
                            MapButton.SetActive(true);
                        }
                        else
                        {
                            MapButton.SetActive(false);
                        }
                        Timer.SetActive(true);
                        SettingButton.SetActive(true);
                        FeaturesMenu.SetActive(true);
                        isTimerDisplayed = true;
                        isTimerPaused = false;
                        if (currentRoomKey == catRoomKey)
                        {
                            Cat.SetActive(true);
                        }
                    }
                    else if (ReplayButtonCollider.OverlapPoint(mousePos))
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    }
                }
                else if (PlayerLostMenu.activeSelf)
                {
                    if (RetryButtonCollider.OverlapPoint(mousePos))
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    }
                    else if (QuitButtonCollider.OverlapPoint(mousePos))
                    {
                        GameManager.Instance.LoadLevelsMenu();
                    }
                }
            }
        }
    }

    private void placeCatInRoom()
    {
        string randomRoom = level.rooms[Random.Range(0, level.rooms.Length)];

        Room selectedRoom = rooms[randomRoom];
        List<GameObject> dynamicObjects = new List<GameObject>();
        foreach (Transform child in selectedRoom.room.transform)
        {
            if (child.tag == "Dynamic")
            {
                dynamicObjects.Add(child.gameObject);
            }
        }

        if (dynamicObjects.Count > 0)
        {
            float catOffset = 0.2f;
            GameObject selectedFurniture = dynamicObjects[Random.Range(0, dynamicObjects.Count)];

            Vector3 catPosition = selectedFurniture.transform.position;
            Cat.transform.position = new Vector3(catPosition.x - catOffset, catPosition.y - catOffset, catPosition.z - catOffset);

            float avgFurnitureSize = (selectedFurniture.transform.localScale.x + selectedFurniture.transform.localScale.y + selectedFurniture.transform.localScale.z) / 3;
            float catSize = avgFurnitureSize * 0.5f;
            Cat.transform.localScale = new Vector3(catSize, catSize, catSize);

            selectedFurniture.GetComponent<DynamicObjectController>().isCatBehind = true;
            CatCollider = Cat.GetComponent<PolygonCollider2D>();

            selectedRoom.isCatThere = true;
            catRoomKey = randomRoom;
        }
    }

    private void openMap()
    {
        pauseTimer();
        hideTimer();
        MapButton.SetActive(false);
        Cat.SetActive(false);
        SettingButton.SetActive(false);
        FeaturesMenu.SetActive(false);

        Room currentRoom = rooms[currentRoomKey];
        currentRoom.room.SetActive(false);

        MapRoom.SetActive(true);
        foreach (GameObject room in MapRoomsMenu)
        {
            if (room != null)
            {
                bool isOpen = currentRoomKey == room.name;
                Room roomObject = rooms[room.name];
                bool isAvailable = roomObject.isRoomAvailable;

                GameObject blackObject = room.transform.Find("black").gameObject;
                if (blackObject != null)
                {
                    blackObject.SetActive(!isAvailable);

                    GameObject lockObject = room.transform.Find("lock").gameObject;
                    if (lockObject != null)
                    {
                        lockObject.SetActive(!isAvailable);
                    }

                    GameObject roomCollider = room.transform.Find("RoomCollider").gameObject;
                    if (roomCollider != null)
                    {
                        roomCollider.SetActive(isAvailable);
                    }
                }
            }
        }
    }

    void UpdateTimerText()
    {
        TextMeshPro timerText = Timer.GetComponentInChildren<TextMeshPro>();

        if (timerText != null)
        {
            timerText.text = $"00:{timeLeft:00}";
        }
    }

    void UpdateFeaturesState(GameObject feature, int count)
    {
        TextMeshPro featureText = feature.GetComponentInChildren<TextMeshPro>();

        if (featureText != null)
        {
            featureText.text = count.ToString();
        }

        if (count == 0)
        {
            feature.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    void ActivateFeature(string featureName)
    {
        switch (featureName)
        {
            case "hourglass":
                if (GameManager.Instance.features.hourglass > 0)
                {
                    pauseTimer();
                    Invoke(nameof(DeactivateHourglass), 10f);
                    GameManager.Instance.RemoveFeature("hourglass");
                    UpdateFeaturesState(Hourglass, GameManager.Instance.features.hourglass);

                }
                break;
            case "snack":
                if (GameManager.Instance.features.snack > 0)
                {
                    if (Cat.activeSelf)
                    {
                        playerWon();
                    }
                    GameManager.Instance.RemoveFeature("snack");
                    UpdateFeaturesState(Snack, GameManager.Instance.features.snack);

                }
                break;
            case "x2":
                if (GameManager.Instance.features.x2 > 0)
                {
                    X2Activated = true;
                    GameManager.Instance.RemoveFeature("x2");
                    UpdateFeaturesState(X2, GameManager.Instance.features.x2);
                }
                break;
            case "ball":
                if (GameManager.Instance.features.ball > 0)
                {
                    GameManager.Instance.RemoveFeature("ball");
                    X2Activated = true;
                    if (Cat.activeSelf)
                    {
                        playerWon();
                    }
                    UpdateFeaturesState(Ball, GameManager.Instance.features.ball);
                }
                break;
        }
    }

    private void DeactivateHourglass()
    {
        isTimerPaused = false;
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
        pauseTimer();
        hideTimer();
        Room currentRoom = rooms[currentRoomKey];
        currentRoom.room.SetActive(false);
        MapButton.SetActive(false);
        Cat.SetActive(false);
        FeaturesMenu.SetActive(false);
        SettingButton.SetActive(false);
        PlayerLostMenu.SetActive(true);
    }

    public void playerWon()
    {
        pauseTimer();
        hideTimer();

        float ratio = timeLeft / initialTime;
        int starsEarned = 0;
        if (ratio < 0.25f)
        {
            starsEarned = 1;
        }
        else if (ratio >= 0.25f && ratio <= 0.75f)
        {
            starsEarned = 2;
        }
        else
        {
            starsEarned = 3;
        }
        int moneyBonus = X2Activated ? 4 : 2;
        int coinsEarned = (int)timeLeft * moneyBonus;
        GameManager.Instance.setLevelScore(level.id, starsEarned, true, coinsEarned);
    }

    void OnDestroy()
    {
        initialTime = 0;
        timeLeft = 0;
        isTimerDisplayed = false;
        isTimerPaused = true;
        Timer.SetActive(false);
        level = null;
        Debug.Log("Level destroyed");
    }
}