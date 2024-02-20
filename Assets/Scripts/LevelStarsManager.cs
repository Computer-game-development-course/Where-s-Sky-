using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelStarsManager : MonoBehaviour
{
    [System.Serializable]
    public class Level
    {
        public GameObject starsContainer; // The parent object of the stars for each level
        public GameObject levelLock;
        public GameObject[] stars; // Individual star objects
    }

    private GameTimer gameTimer; // Reference to the GameTimer script

    public Level[] levelsStars; // Array of all levels and their stars

    [Tooltip("Names of the scenes where this object should not be destroyed.")]
    [SerializeField] string[] persistInScenes;

    void Update()
    {
        gameTimer = FindObjectOfType<GameTimer>();
        UpdateStarsVisibility();
    }

    void UpdateStarsVisibility()
    {
        for (int i = 0; i < levelsStars.Length; i++)
        {
            levelsStars[i].starsContainer.SetActive(true);
            levelsStars[i].levelLock.SetActive(true);

            foreach (GameObject star in levelsStars[i].stars)
            {
                star.SetActive(false);
            }

            if (gameTimer != null)
            {
                if (gameTimer.levels[i].level_stars[0] == true)
                {
                    levelsStars[i].stars[0].SetActive(true);
                }
                if (gameTimer.levels[i].level_stars[1] == true)
                {
                    levelsStars[i].stars[1].SetActive(true);
                }
                if (gameTimer.levels[i].level_stars[2] == true)
                {
                    levelsStars[i].stars[2].SetActive(true);
                }
            }
        }
    }
}