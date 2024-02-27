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
    public int lastToOpen = 1;

    public StageData[] stages = new StageData[30]; // Assuming 30 stages
    public RoomData[] rooms; // Assuming you'll define the number of rooms somewhere
    public int totalCoins = 0;

    public float timeLeft = 40f; // Total time for the game (in seconds)
    public float initial_time = 40f;

    public bool isReset = false;

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

    public void ResetTime()
    {
        isReset = true;
    }

    public void InittTime()
    {
        isReset = false;
        initial_time = timeLeft;
        timeLeft = stages[currentStage].timeLimit;
    }

    public float GetInitialTime()
    {
        return initial_time;
    }

    public float GetTimeLeft()
    {
        return timeLeft;
    }

    public void SetInitialTime(float sec)
    {
        initial_time = sec;
    }

    public void SetTimeLeft(float sec)
    {
        timeLeft = sec;
    }

    public void OpenNextStage()
    {
        // if (currentStage.Equals(lastToOpen))
        // {
        //     if (currentStage + 1 < stages.Length)
        //     {
        //         stages[currentStage + 1].isOpen = true;
        //         lastToOpen++;
        //         //currentStage++;
        //     }
        // }
        if (currentStage + 1 < stages.Length)
        {
            stages[currentStage + 1].isOpen = true;
            if (currentStage + 1 > lastToOpen)
            {
                lastToOpen = currentStage + 1;
            }
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
}