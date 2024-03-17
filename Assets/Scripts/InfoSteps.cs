using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoSteps : MonoBehaviour
{
    [SerializeField] GameObject[] gameSteps;
    private int currentStep = 0;
    void Start()
    {
        gameSteps[0].gameObject.SetActive(true);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            for (int i = 0; i < gameSteps.Length - 1; i++)
            {
                if (gameSteps[i].gameObject.activeSelf)
                {
                    if (gameSteps[i].transform.Find("nextButton").gameObject.GetComponent<Collider2D>().OverlapPoint(mousePos))
                    {
                        gameSteps[i].gameObject.SetActive(false);
                        if (i < gameSteps.Length - 1)
                        {
                            Invoke(nameof(LoadNextStep), 0.4f);
                        }
                    }
                }
            }
        }
    }

    void LoadNextStep()
    {
        gameSteps[currentStep + 1].gameObject.SetActive(true);
        currentStep++;
    }

}
