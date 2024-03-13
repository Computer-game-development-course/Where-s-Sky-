using UnityEngine;

[System.Serializable]
public class StageData
{
    public bool isOpen = false;
    public int stars = 0;
    public float timeLimit = 0f;
}

[System.Serializable]
public class RoomData
{
    public bool isOpen = false;
}

[System.Serializable]
public class Hourglass
{
    public int amount = 0;
}

[System.Serializable]
public class Snack
{
    public int amount = 0;
}

[System.Serializable]
public class X2
{
    public int amount = 0;
}

[System.Serializable]
public class Ball
{
    public int amount = 0;
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // Game data
    public int currentStage = 1;
    public int lastToOpen = 1;

    public StageData[] stages = new StageData[30];
    public RoomData[] rooms = new RoomData[8];
    public Hourglass hourglass;
    public Snack snack;
    public X2 x2;
    public Ball ball;
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
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ResetTime()
    {
        isReset = true;
        initial_time = stages[currentStage].timeLimit;
        timeLeft = stages[currentStage].timeLimit;
    }

    public void InittTime()
    {
        isReset = false;
        initial_time = stages[currentStage].timeLimit;
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

    public int GettotalCoins()
    {
        return totalCoins;
    }

    public void OpenNextStage()
    {
        if (currentStage + 1 < stages.Length)
        {
            stages[currentStage + 1].isOpen = true;
            if (currentStage + 1 > lastToOpen)
            {
                lastToOpen = currentStage + 1;
            }
            if (currentStage + 1 == 2)// Open a room after completing level 2
            {
                rooms[1].isOpen = true;
            }
            else if (currentStage + 1 == 4)// Open a room after completing level 4
            {
                rooms[2].isOpen = true;
            }
            else if (currentStage + 1 == 8)// Open a room after completing level 8
            {
                rooms[3].isOpen = true;
            }
            else if (currentStage + 1 == 12)// Open a room after completing level 12
            {
                rooms[4].isOpen = true;
            }
            else if (currentStage + 1 == 17)// Open a room after completing level 17
            {
                rooms[5].isOpen = true;
            }
            else if (currentStage + 1 == 21)// Open a room after completing level 21
            {
                rooms[6].isOpen = true;
            }
            else if (currentStage + 1 == 25)// Open a room after completing level 25
            {
                rooms[7].isOpen = true;
            }
        }
    }

    public void SetStageStars(int stage, int stars)
    {
        if (stage >= 0 && stage < stages.Length)
        {
            stages[stage].stars = stars;
        }
    }

    public void AddCoins(int amount)
    {
        totalCoins += amount;
    }

    public void RemoveCoins(int amount)
    {
        totalCoins -= amount;
    }

    public void AddFeature(string s)
    {
        if (s.Equals("hourglass"))
        {
            hourglass.amount++;
        }
        else if (s.Equals("snack"))
        {
            snack.amount++;
        }
        else if (s.Equals("x2"))
        {
            x2.amount++;
        }
        else if (s.Equals("ball"))
        {
            ball.amount++;
        }
    }

    public void RemoveFeature(string s)
    {
        if (s.Equals("hourglass"))
        {
            hourglass.amount--;
        }
        else if (s.Equals("snack"))
        {
            snack.amount--;
        }
        else if (s.Equals("x2"))
        {
            x2.amount--;
        }
        else if (s.Equals("ball"))
        {
            ball.amount--;
        }
    }

    // Method to open rooms
    public void OpenRoom(int roomIndex)
    {
        if (roomIndex >= 0 && roomIndex < rooms.Length)
        {
            rooms[roomIndex].isOpen = true;
        }
    }

    // Method to update the time limit for a level
    public void SetStageTimeLimit(int stage, float time)
    {
        if (stage >= 0 && stage < stages.Length)
        {
            stages[stage].timeLimit -= time;
        }
    }
}