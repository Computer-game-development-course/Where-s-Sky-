using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

// private void Awake()
// {
//     // Subscribes to the sceneLoaded event to get notified whenever a new scene is loaded.
//     SceneManager.sceneLoaded += OnSceneLoaded;

//     // If an instance of this Singleton does not exist, this becomes the instance.
//     if (instance == null)
//     {
//         instance = this;
//         DontDestroyOnLoad(gameObject); // Prevents the object from being destroyed on scene loads.
//     }
//     else if (instance != this) // If another instance exists, destroy this to enforce the singleton pattern.
//     {
//         Destroy(gameObject);
//     }
// }

// private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
// {
//     // Callback method that gets called every time a new scene is loaded.
//     if (!ShouldPersistInCurrentScene())
//     {
//         // If the current scene is not in the list of scenes to persist in, destroy this object.
//         Destroy(gameObject);
//     }
// }

// // Determines if this Singleton object should persist in the current scene.
// private bool ShouldPersistInCurrentScene()
// {
//     // Gets the name of the currently active scene.
//     string currentSceneName = SceneManager.GetActiveScene().name;
//     // Iterates over the list of scenes where this object should persist.
//     foreach (string sceneName in persistInScenes)
//     {
//         if (currentSceneName.Equals(sceneName))
//         {
//             // If the current scene's name matches one in the list, the object should persist.
//             return true;
//         }
//     }
//     // If no match is found, the object should not persist in the current scene.
//     return false;
// }

// private void OnDestroy()
// {
//     // Unsubscribes from the sceneLoaded event when this object is destroyed.
//     // This is important to prevent memory leaks by ensuring the object can be properly garbage collected.
//     SceneManager.sceneLoaded -= OnSceneLoaded;
// }
//}