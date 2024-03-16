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
            DontDestroyOnLoad(gameObject);
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
            SFXSource.clip = catMaw;
            isSFXPlay = true;
            StartCoroutine(PlaySFXRandomly());
        }


    }

    // Coroutine for playing SFX at random intervals
    private IEnumerator PlaySFXRandomly()
    {
        while (isSFXPlay)
        {
            SFXSource.Play();
            yield return new WaitForSeconds(Random.Range(10, 16));
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
