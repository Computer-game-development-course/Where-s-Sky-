using UnityEngine;

public class roomManager : MonoBehaviour
{
    void OnEnable()
    {
        UpdateLevelUI();
    }

    private void UpdateLevelUI()
    {
        // Assuming the GameManager.Instance.rooms array is 0-indexed and matches room numbers starting from room1
        for (int i = 1; i < GameManager.Instance.rooms.Length; i++)
        {
            // Finding each room object by name
            GameObject roomObject = GameObject.Find($"room{i + 1}");

            if (roomObject != null)
            {
                // Update lock and black status
                GameObject blackObject = roomObject.transform.Find("black").gameObject;
                if (blackObject != null)
                {
                    bool isRoomOpen = GameManager.Instance.rooms[i].isOpen;
                    blackObject.SetActive(!isRoomOpen); // Hide or show black and its children based on room status

                    // Check for optional "black2" object
                    Transform black2Transform = blackObject.transform.Find("black2");
                    if (black2Transform != null)
                    {
                        black2Transform.gameObject.SetActive(!isRoomOpen);
                    }

                    GameObject lockObject = blackObject.transform.Find("lock").gameObject;
                    if (lockObject != null)
                    {
                        lockObject.SetActive(!isRoomOpen);
                    }

                    // Enable or disable the AssetClick script based on the room's open status
                    AssetClick sceneToLoadScript = roomObject.GetComponent<AssetClick>();
                    if (sceneToLoadScript != null)
                    {
                        sceneToLoadScript.enabled = isRoomOpen;
                    }
                }
            }
        }
    }
}
