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

[System.Serializable]
public class RoomScene
{
    public bool isOpen = false;
    public string sceneName;
    public bool isCatThere = false;
    public int opensAtLevel = 0;
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private const int levelsCount = 30;
    private const int roomsCount = 8;
    public Level[] levels = new Level[levelsCount];
    public RoomScene[] roomScenes = new RoomScene[roomsCount];
    public int coins = 0;
    public Level currentLevel = null;
    private const int timePerRoom = 10;
    public int previousLevelMoneyEarned = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            string[] roomNames = { "EntryRoom", "Bathroom", "Backyard", "Bedroom", "Kitchen", "LaundryRoom", "LivingRoom", "OfficeRoom" };
            for (int i = 0; i < roomsCount; i++)
            {
                roomScenes[i].sceneName = roomNames[i];
                roomScenes[i].opensAtLevel = i == 0 ? 0 : (levelsCount / roomsCount) * i - 1;
            }

            for (int i = 0; i < levelsCount; i++)
            {
                levels[i].id = i;
                int levelRoomsCount = ((i + 1) / (levelsCount / roomsCount)) + 1;
                levels[i].rooms = new String[levelRoomsCount];
                for (int j = 0; j < roomsCount; j++)
                {
                    if (roomScenes[j].opensAtLevel <= i)
                    {
                        levels[i].rooms[j] = roomScenes[j].sceneName;
                    }
                }
                levels[i].time = timePerRoom * levels[i].rooms.Length;
            }
            levels[0].isOpen = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadLevel(int levelId)
    {
        levels[levelId].isOpen = true;
        currentLevel = levels[levelId];
        SceneManager.LoadScene("Level");
    }

    public void LoadNextLevel()
    {
        currentLevel = levels[currentLevel.id + 1];
        SceneManager.LoadScene("Level");
    }

    public void LoadLevelsMenu()
    {
        SceneManager.LoadScene("select_level");
    }

    public void setLevelScore(int level, int stars, bool isCompleted, int moneyEarned)
    {
        levels[level].stars = Math.Max(stars, levels[level].stars);
        levels[level].isCompleted = isCompleted;
        currentLevel = levels[level];
        previousLevelMoneyEarned = moneyEarned;
        AddCoins(moneyEarned);

        if (level < levelsCount - 1)
        {
            levels[level + 1].isOpen = true;
        }
        SceneManager.LoadScene("PlayerWon");
    }

    public int GetTotalCoins()
    {
        return coins;
    }

    public void AddCoins(int amount)
    {
        coins += amount;
    }

    public void RemoveCoins(int amount)
    {
        coins -= amount;
    }
}