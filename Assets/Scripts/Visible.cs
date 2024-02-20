using UnityEngine;
using UnityEngine.SceneManagement; // Required for accessing and managing scenes within Unity.


public class Visible : MonoBehaviour
{
    [Tooltip("Names of the scenes where this object should not be destroyed.")]
    [SerializeField] string[] persistInScenes;

    private void Awake()
    {
        // Subscribes to the sceneLoaded event to get notified whenever a new scene is loaded.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Callback method that gets called every time a new scene is loaded.
        if (!ShouldPersistInCurrentScene())
        {
            SpriteRenderer mainSpriteRenderer = GetComponent<SpriteRenderer>();
            if (mainSpriteRenderer != null)
            {
                mainSpriteRenderer.enabled = false;
            }

            // Iterate through all child objects to set their visibility
            foreach (Transform child in transform)
            {
                // Handle SpriteRenderer components (for most child objects)
                SpriteRenderer sr = child.GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    sr.enabled = false;
                }

                // Handle the special case for the "TEXT" object with TextMeshPro
                MeshRenderer tmpText = child.GetComponent<MeshRenderer>();
                if (tmpText != null)
                {
                    // For TextMeshPro components, control visibility through the enabled property
                    tmpText.enabled = false;
                }
            }
        }
        else
        {
            SpriteRenderer mainSpriteRenderer = GetComponent<SpriteRenderer>();
            if (mainSpriteRenderer != null)
            {
                mainSpriteRenderer.enabled = true;
            }

            // Iterate through all child objects to set their visibility
            foreach (Transform child in transform)
            {
                // Handle SpriteRenderer components (for most child objects)
                SpriteRenderer sr = child.GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    sr.enabled = true;
                }

                // Handle the special case for the "TEXT" object with TextMeshPro
                MeshRenderer tmpText = child.GetComponent<MeshRenderer>();
                if (tmpText != null)
                {
                    // For TextMeshPro components, we control visibility through the enabled property
                    tmpText.enabled = true;
                }
            }

        }
    }

    // Determines if this object should persist in the current scene.
    private bool ShouldPersistInCurrentScene()
    {
        // Gets the name of the currently active scene.
        string currentSceneName = SceneManager.GetActiveScene().name;
        // Iterates over the list of scenes where this object should persist.
        foreach (string sceneName in persistInScenes)
        {
            if (currentSceneName.Equals(sceneName))
            {
                // If the current scene's name matches one in the list, the object should persist.
                return true;
            }
        }
        // If no match is found, the object should not persist in the current scene.
        return false;
    }
}