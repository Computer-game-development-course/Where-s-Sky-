using UnityEngine;

public class LevelStarsManager : MonoBehaviour
{
    void OnEnable()
    {
        UpdateLevelUI();
    }

    private void UpdateLevelUI()
    {
        for (int i = 0; i < GameManager.Instance.stages.Length; i++)
        {
            // Finding each level object by name
            GameObject levelObject = GameObject.Find($"Level {i + 1}");

            if (levelObject != null)
            {
                // Update lock status
                GameObject lockObject = levelObject.transform.Find("lock").gameObject;
                if (lockObject != null)
                {
                    bool isStageOpen = GameManager.Instance.stages[i].isOpen;
                    lockObject.SetActive(!isStageOpen);

                    // Enable or disable the SceneToLoad script based on the stage's open status
                    AssetClick sceneToLoadScript = levelObject.GetComponent<AssetClick>();
                    if (sceneToLoadScript != null)
                    {
                        sceneToLoadScript.enabled = isStageOpen;
                    }
                }

                // Update stars
                GameObject starsObject = levelObject.transform.Find("stars").gameObject;
                if (starsObject != null)
                {
                    GameObject star1 = starsObject.transform.Find("star1").gameObject;
                    GameObject star2 = starsObject.transform.Find("star2").gameObject;
                    GameObject star3 = starsObject.transform.Find("star3").gameObject;

                    // Update visibility based on the number of stars earned
                    int starsEarned = GameManager.Instance.stages[i].stars;
                    star1.SetActive(starsEarned >= 1);
                    star2.SetActive(starsEarned >= 2);
                    star3.SetActive(starsEarned == 3);
                }
            }
        }
    }
}
