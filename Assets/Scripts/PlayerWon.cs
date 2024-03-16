using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class PlayerWon : MonoBehaviour
{
    [SerializeField] GameObject LevelUpAsset;
    [SerializeField] GameObject[] stars;
    [SerializeField] GameObject[] startsOutline;
    [SerializeField] GameObject[] responces;
    [SerializeField] CapsuleCollider2D nextLevelButton;
    [SerializeField] CapsuleCollider2D selectLevelButton;

    void Start()
    {
        Level level = GameManager.Instance.currentLevel;
        int starts = level.stars;
        int moneyEarned = GameManager.Instance.previousLevelMoneyEarned;

        for (int i = 0; i < starts; i++)
        {
            stars[i].SetActive(true);
        }

        TextMeshPro coinsText = GetComponentInChildren<TextMeshPro>();

        if (coinsText != null)
        {
            coinsText.text = moneyEarned.ToString();
        }

        int randomResponce = UnityEngine.Random.Range(0, responces.Length);
        responces[randomResponce].SetActive(true);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (nextLevelButton.OverlapPoint(mousePos))
            {
                GameManager.Instance.LoadNextLevel();
            }
            else if (selectLevelButton.OverlapPoint(mousePos))
            {
                GameManager.Instance.LoadLevelsMenu();
            }
        }
    }
}