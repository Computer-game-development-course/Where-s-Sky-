using UnityEngine;

public class LevelStarsManager : MonoBehaviour
{
    [SerializeField] GameObject[] levelsButtons;
    void OnEnable()
    {
        UpdateLevelUI();
    }

    private void UpdateLevelUI()
    {
        for (int i = 0; i < GameManager.Instance.levels.Length; i++)
        {
            GameObject levelObject = levelsButtons[i];

            if (levelObject != null)
            {
                bool isLevelOpen = GameManager.Instance.levels[i].isOpen;

                GameObject lockObject = levelObject.transform.Find("lock").gameObject;
                if (lockObject != null)
                {
                    lockObject.SetActive(!isLevelOpen);

                    BoxCollider2D buttonCollider = levelObject.GetComponent<BoxCollider2D>();
                    if (buttonCollider != null)
                    {
                        buttonCollider.enabled = isLevelOpen;
                    }
                }

                GameObject starsObject = levelObject.transform.Find("stars").gameObject;
                if (starsObject != null)
                {
                    GameObject star1 = starsObject.transform.Find("star1").gameObject;
                    GameObject star2 = starsObject.transform.Find("star2").gameObject;
                    GameObject star3 = starsObject.transform.Find("star3").gameObject;

                    int starsEarned = GameManager.Instance.levels[i].stars;
                    star1.SetActive(starsEarned >= 1);
                    star2.SetActive(starsEarned >= 2);
                    star3.SetActive(starsEarned == 3);

                }
            }
        }
    }
}
