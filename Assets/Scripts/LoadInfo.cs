using UnityEngine;

public class LoadInfo : MonoBehaviour
{
    public static LoadInfo Instance;
    public SpriteRenderer[] infoDisplays;
    public int currentInfo = 0;

    void Awake()
    {
        // Initialize the Singleton instance
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int GetCurrentInfo()
    {
        return currentInfo;
    }

    public void SetCurrentInfo(int info)
    {
        currentInfo = info;
    }

    void Update()
    {
        for (int i = 0; i < infoDisplays.Length; i++)
        {
            if (i + 1 == currentInfo)
            {
                // Show the Sprite Renderer in infoDisplays[i]
                infoDisplays[i].enabled = true;
            }
            else
            {
                // Turn off the Sprite Renderer in infoDisplays[i]
                infoDisplays[i].enabled = false;
            }
        }
    }
}
