using UnityEngine;
using UnityEngine.SceneManagement; // Required for accessing scene management

public class Singleton : MonoBehaviour
{
    private static Singleton instance = null;

    // Array of scene names where this object should persist
    public string[] persistInScenes;

    void Awake()
    {
        // Check if we're in a scene where the object should persist
        if (ShouldPersistInCurrentScene())
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (instance != this)
            {
                Destroy(gameObject); // Destroy any duplicate objects
            }
        }
        else
        {
            if (this != instance)
            {
                Destroy(gameObject); // Destroy this object if it's not in a scene from the list
            }
        }
    }

    private bool ShouldPersistInCurrentScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        foreach (string sceneName in persistInScenes)
        {
            if (currentSceneName.Equals(sceneName))
            {
                return true; // Current scene is in the list, should persist
            }
        }
        return false; // Current scene is not in the list, should not persist
    }
}
