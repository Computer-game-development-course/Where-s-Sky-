using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;

    public AudioClip background;
    public AudioClip catMaw;

    public bool isMusicPlay = false;
    public bool isSFXPlay = false;

    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep the audio manager across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        musicSource.clip = background;
        if (!isMusicPlay)
        {
            musicSource.Play();
            isMusicPlay = true;
        }
        if (!isSFXPlay)
        {
            isSFXPlay = true;
            SFXSource.clip = catMaw;
            SFXSource.Play();
            StartCoroutine(PlaySFXRandomly());
        }


    }

    // Coroutine for playing SFX at random intervals
    private IEnumerator PlaySFXRandomly()
    {
        while (isSFXPlay)
        {
            yield return new WaitForSeconds(Random.Range(5, 11)); // Wait for 5 to 10 seconds randomly
            if (isSFXPlay) // Check again in case it changed while waiting
            {
                SFXSource.clip = catMaw;
                SFXSource.Play();
            }
        }
    }

    public void MusicOnOff()
    {
        if (isMusicPlay)
        {
            musicSource.Stop();
        }
        else
        {
            musicSource.Play();
        }
        isMusicPlay = !isMusicPlay;
    }

    public void SFXOnOff()
    {
        isSFXPlay = !isSFXPlay;
        if (isSFXPlay)
        {
            StartCoroutine(PlaySFXRandomly()); // Start playing SFX again
        }
        else
        {
            SFXSource.Stop(); // Stop the SFX if it's currently playing
        }
    }
}
