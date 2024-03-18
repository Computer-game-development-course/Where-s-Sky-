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
public class Features
{
    public int hourglass = 0;
    public int snack = 0;
    public int x2 = 0;
    public int ball = 0;
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private const int levelsCount = 30;
    private const int roomsCount = 8;
    public Level[] levels = new Level[levelsCount];
    public int coins = 0;
    public Level currentLevel = null;
    public Features features = new Features();
    private const int timePerRoom = 10;
    public int previousLevelMoneyEarned = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            string[] roomNames = { "EntryRoom", "Bathroom", "LaundryRoom", "OfficeRoom", "LivingRoom", "Kitchen", "Bedroom", "Backyard" };

            for (int i = 0; i < levelsCount; i++)
            {
                levels[i].id = i;
                int levelRoomsCount = ((i + 1) / (levelsCount / roomsCount)) + 1;
                levels[i].rooms = new String[levelRoomsCount];
                for (int j = 0; j < roomsCount; j++)
                {
                    int roomOpensAt = j == 0 ? 0 : (levelsCount / roomsCount) * j - 1;
                    if (roomOpensAt <= i)
                    {
                        levels[i].rooms[j] = roomNames[j];
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
        if (levelId != -2)
        {
            currentLevel = levels[levelId];
        }

        SceneManager.LoadScene("Level");
    }

    public void LoadNextLevel()
    {
        if (currentLevel.id != 29)
        {
            currentLevel = levels[currentLevel.id + 1];
            SceneManager.LoadScene("Level");
        }
    }

    public void LoadLevelsMenu()
    {
        SceneManager.LoadScene("SelectLevel");
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

    public void AddFeature(string feature)
    {
        if (feature == "hourglass")
        {
            features.hourglass++;
        }
        else if (feature == "snack")
        {
            features.snack++;
        }
        else if (feature == "x2")
        {
            features.x2++;
        }
        else if (feature == "ball")
        {
            features.ball++;
        }
    }

    public void RemoveFeature(string feature)
    {
        if (feature == "hourglass")
        {
            features.hourglass--;
        }
        else if (feature == "snack")
        {
            features.snack--;
        }
        else if (feature == "x2")
        {
            features.x2--;
        }
        else if (feature == "ball")
        {
            features.ball--;
        }
    }
}