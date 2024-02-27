using UnityEngine;

[System.Serializable]
public class StageData
{
    public bool isOpen = false;
    public int stars = 0;
    public float timeLimit = 0f;
    // Additional stage-specific data can be added here
}

[System.Serializable]
public class RoomData
{
    public bool isOpen = false;
    // Room-specific data like the cat's position can be added here
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // Game data
    public int currentStage = 1;
    public StageData[] stages = new StageData[30]; // Assuming 30 stages
    public RoomData[] rooms; // Assuming you'll define the number of rooms somewhere
    public int totalCoins = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Initialize game data
            stages[0].isOpen = true; // Open the first stage
            rooms[0].isOpen = true; // Open the first stage
            // Initialize rooms and other data as needed
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OpenNextStage()
    {
        if (currentStage + 1 < stages.Length)
        {
            stages[currentStage + 1].isOpen = true;
            currentStage++;
            // Additional logic for opening next stage (like updating UI) can be added here
        }
    }

    public void SetStageStars(int stage, int stars)
    {
        if (stage >= 0 && stage < stages.Length)
        {
            stages[stage].stars = stars;
            // Update UI or other relevant components
        }
    }

    public void AddCoins(int amount)
    {
        totalCoins += amount;
        // Update UI or perform other actions
    }

    // Method to open rooms, update time limits, etc., can be added similarly
    // Example for opening a room:
    public void OpenRoom(int roomIndex)
    {
        if (roomIndex >= 0 && roomIndex < rooms.Length)
        {
            rooms[roomIndex].isOpen = true;
            // Additional logic for opening a room
        }
    }

    // Method to update the time limit for a stage
    public void SetStageTimeLimit(int stage, float time)
    {
        if (stage >= 0 && stage < stages.Length)
        {
            stages[stage].timeLimit = time;
            // Additional logic for setting time limit
        }
    }

    // Other necessary methods can be implemented in a similar manner
}