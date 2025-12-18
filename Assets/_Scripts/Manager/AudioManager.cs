using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource bgmSource;      
    public AudioSource sfxSource;      
    public AudioSource pedalSource;   

    public AudioClip bgmClip;
    public AudioClip buttonClickClip;
    public AudioClip submitClip;
    public AudioClip resetClip;
    public AudioClip pedalLoopClip;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (bgmSource != null && bgmClip != null)
        {
            bgmSource.clip = bgmClip;
            bgmSource.loop = true;
            bgmSource.Play();
        }

        if (pedalSource != null && pedalLoopClip != null)
        {
            pedalSource.clip = pedalLoopClip;
            pedalSource.loop = true;
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void StartPedalLoop()
    {
        if (pedalSource != null && !pedalSource.isPlaying)
        {
            pedalSource.Play();
        }
    }

    public void StopPedalLoop()
    {
        if (pedalSource != null && pedalSource.isPlaying)
        {
            pedalSource.Stop();
        }
    }

    public void PlayButtonClick()
    {
        PlaySFX(buttonClickClip);
    }

    public void PlaySubmit()
    {
        PlaySFX(submitClip);
    }

    public void PlayReset()
    {
        PlaySFX(resetClip);
    }
}