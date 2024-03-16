using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;

[System.Serializable]
public class Level
{
    public int id;
    public bool isOpen = false;
    public int stars = 0;
    public String[] rooms;
    public bool isCompleted = false;
    public int time;
}

// [System.Serializable]
// public class Room
// {
//     public int id;
//     public bool isOpen = false;
//     public bool isCatThere = false;
//     public bool isAvailable = false;
//     public int opensAtLevel = 0;
// }

[System.Serializable]
public class RoomScene
{
    public bool isOpen = false;
    public string sceneName;
    public bool isCatThere = false;
    public int opensAtLevel = 0;
}


// [System.Serializable]
// public class StageData
// {
//     public bool isOpen = false;
//     public int stars = 0;
//     public float timeLimit = 0f;
// }

// [System.Serializable]
// public class RoomData
// {
//     public bool isOpen = false;
// }

// [System.Serializable]
// public class Hourglass
// {
//     public int amount = 0;
// }

// [System.Serializable]
// public class Snack
// {
//     public int amount = 0;
// }

// [System.Serializable]
// public class X2
// {
//     public int amount = 0;
// }

// [System.Serializable]
// public class Ball
// {
//     public int amount = 0;
// }
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private const int levelsCount = 30;
    private const int roomsCount = 8;
    public Level[] levels = new Level[levelsCount];
    //public Room[] rooms = new Room[roomsCount];

    public RoomScene[] roomScenes = new RoomScene[roomsCount];

    public int coins = 0;
    public Level currentLevel = null;

    private const int timePerRoom = 10;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // levels[0].isOpen = true;
            // rooms[0].isAvailable = true;
            // int i = 0;
            // foreach (Room room in rooms)
            // {
            //     room.id = i;
            //     room.opensAtLevel = i == 0 ? 1 : (levelsCount / roomsCount) * i;
            //     i++;
            // }

            // i = 0;
            // foreach (Level level in levels)
            // {
            //     level.id = i;
            //     int levelRoomsCount = ((i + 1) / (levelsCount / roomsCount)) + 1;
            //     level.rooms = new Room[levelRoomsCount];
            //     int j = 0;
            //     foreach (Room room in rooms)
            //     {
            //         if (room.opensAtLevel <= i)
            //         {
            //             level.rooms[j++] = room;
            //         }
            //     }
            //     level.time = timePerRoom * level.rooms.Length;
            //     i++;
            // }
            int i = 0;
            for (i = 0; i < roomsCount; i++)
            {
                roomScenes[i].sceneName = $"room{(i + 1)}";
                roomScenes[i].opensAtLevel = i == 0 ? 0 : (levelsCount / roomsCount) * i - 1;
            }

            i = 0;
            foreach (Level level in levels)
            {
                level.id = i;
                int levelRoomsCount = ((i + 1) / (levelsCount / roomsCount)) + 1;
                level.rooms = new String[levelRoomsCount];
                int j = 0;
                foreach (RoomScene room in roomScenes)
                {
                    if (room.opensAtLevel <= i)
                    {
                        level.rooms[j++] = room.sceneName;
                    }
                }
                level.time = timePerRoom * level.rooms.Length;
                i++;
            }

            levels[0].isOpen = true;
            currentLevel = levels[0];
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void loadLevel(int level)
    {
        currentLevel = levels[level];
        roomScenes[0].isOpen = true;
        SceneManager.LoadScene(currentLevel.rooms[0]);
    }

    public void loadNextLevel()
    {
        currentLevel = levels[currentLevel.id + 1];
        LevelManager.Instance.destroyLevel();
        roomScenes[0].isOpen = true;
        SceneManager.LoadScene(currentLevel.rooms[0]);
    }

    public void setLevelScore(int level, int stars, bool isCompleted)
    {
        levels[level].stars = Math.Max(stars, levels[level].stars);
        levels[level].isCompleted = isCompleted;
    }

    public int GetTotalCoins()
    {
        return coins;
    }

    public void newRoomOpen()
    {
        int i = 0;
        i++;
    }

    public void AddCoins(int amount)
    {
        coins += amount;
    }

    public void RemoveCoins(int amount)
    {
        coins -= amount;
    }

    public void AddFeature(string s)
    {
        if (s.Equals("hourglass"))
        {
            coins += 0;
        }
        else if (s.Equals("snack"))
        {
            coins += 0;
        }
        else if (s.Equals("x2"))
        {
            coins += 0;
        }
        else if (s.Equals("ball"))
        {
            coins += 0;
        }
    }

    public int getFeatureAmount(string s)
    {
        if (s.Equals("hourglass"))
        {
            return coins;

        }
        else if (s.Equals("snack"))
        {
            return coins;
        }
        else if (s.Equals("x2"))
        {
            return coins;
        }
        else if (s.Equals("ball"))
        {
            return coins;
        }

        return 0;
    }

    public void RemoveFeature(string s)
    {
        if (s.Equals("hourglass"))
        {
            coins += 0;
        }
        else if (s.Equals("snack"))
        {
            coins += 0;
        }
        else if (s.Equals("x2"))
        {
            coins += 0;
        }
        else if (s.Equals("ball"))
        {
            coins += 0;
        }
    }

    // Method to open rooms
    // public void OpenRoom(int roomIndex)
    // {
    //     if (roomIndex >= 0 && roomIndex < rooms.Length)
    //     {
    //         rooms[roomIndex].isOpen = true;
    //     }
    // }
}