using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    [SerializeField] GameObject[] rooms;
    int activeRoom = -1;
    void OnEnable()
    {
        UpdateLevelUI();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            for (int i = 0; i < rooms.Length; i++)
            {
                GameObject room = rooms[i];
                if (room != null)
                {
                    if (room.GetComponent<Collider2D>().OverlapPoint(mousePos))
                    {
                        if (activeRoom != -1)
                        {
                            GameManager.Instance.roomScenes[activeRoom].isOpen = false;
                        }

                        GameManager.Instance.roomScenes[i].isOpen = true;
                        SceneManager.LoadScene($"room{i + 1}");
                    }
                }
            }
        }
    }

    private void UpdateLevelUI()
    {
        int levelId = GameManager.Instance.currentLevel.id;
        for (int i = 0; i < rooms.Length; i++)
        {
            // Finding each room object by name
            GameObject room = rooms[i];
            bool isOpen = GameManager.Instance.roomScenes[i].isOpen;
            bool isAvailable = GameManager.Instance.roomScenes[i].opensAtLevel <= levelId;

            if (room != null)
            {
                // Update lock and black status
                GameObject blackObject = room.transform.Find("black").gameObject;
                if (blackObject != null)
                {
                    // Hide or show black and its children based on room status
                    blackObject.SetActive(!isOpen);

                    // Check for optional "black2" object
                    Transform black2Transform = blackObject.transform.Find("black2");
                    if (black2Transform != null)
                    {
                        black2Transform.gameObject.SetActive(!isOpen);
                    }

                    GameObject lockObject = blackObject.transform.Find("lock").gameObject;
                    if (lockObject != null)
                    {
                        lockObject.SetActive(!isAvailable);
                    }

                    // Enable or disable the AssetClick script based on the room's open status
                    if (room.GetComponent<AssetClick>() != null)
                    {
                        room.GetComponent<AssetClick>().enabled = isAvailable;
                    }
                }

                if (isOpen)
                {
                    activeRoom = i;
                }
            }
        }
    }
}
