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
    [SerializeField] GameObject MapRoom;
    [SerializeField] GameObject[] MapRoomsMenu;
    [SerializeField] GameObject[] MovingArrows;
    [SerializeField] GameObject Cat;
    private PolygonCollider2D CatCollider;
    private Dictionary<string, Room> rooms = new Dictionary<string, Room>();
    private string currentRoomKey;
    private string catRoomKey;
    BoxCollider2D mapCollider;
    private FeaturesManager featuresManager;
    private bool isTimerPaused = false;
    private bool isTimerDisplayed = true;

    void Start()
    {
        level = GameManager.Instance.currentLevel;

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

        mapCollider = MapButton.GetComponent<BoxCollider2D>();

        if (level.rooms.Length > 1)
        {
            MapButton.SetActive(true);
        }
        else
        {
            MapButton.SetActive(false);
        }

        if (currentRoomKey == catRoomKey)
        {
            Cat.SetActive(true);
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
                        openMap();
                    }
                    else if (CatCollider.OverlapPoint(mousePos))
                    {
                        playerWon();
                    }
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && MapRoom.activeSelf)
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

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
        GameManager.Instance.setLevelScore(level.id, starsEarned, true, (int)timeLeft * 2);
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