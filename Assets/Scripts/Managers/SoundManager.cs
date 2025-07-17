using Unity.VisualScripting;
using UnityEngine;

public enum SoundType
{
    WIN_GAME,
    LOSE_GAME,
    START,
    HOVER,
    DOOR,
}

public enum MusicType
{
    BACKGROUND,
}

[RequireComponent(typeof(AudioSource)), RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [Header("SFX Clips")]
    [SerializeField] private AudioClip[] soundList;

    [Header("Music Sources")]
    [SerializeField] private AudioClip[] musicList;

    public static SoundManager instance;
    private AudioSource sfxSource;
    private AudioSource musicSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        AudioSource[] sources = GetComponents<AudioSource>();

        if(sources.Length < 2)
        {
            while (sources.Length == 2)
            {
                gameObject.AddComponent<AudioSource>();
                sources = GetComponents<AudioSource>();
            }
        }

        sfxSource = sources[0];
        musicSource = sources[1];

        sfxSource.loop = false;
        sfxSource.playOnAwake = false;
        musicSource.loop = true;
    }

    public static void PlaySfx(SoundType sfxSound, float volume = 0.2f)
    {
        AudioClip clip = instance.soundList[(int)sfxSound];
        if (instance.sfxSource != null && clip != null)
        {
            instance.sfxSource.volume = volume;
            instance.sfxSource.PlayOneShot(clip);
        }
    }

    public static void PlayMusic(MusicType music, float volumen = 0.2f)
    {
        AudioClip clip = instance.musicList[(int)music];
        if (instance.sfxSource != null && clip != null)
        {
            instance.musicSource.clip = clip;
            instance.musicSource.volume = volumen;
            instance.musicSource.Play();
        }
    }

    public static void StopMusic() {
        if (instance.musicSource != null)
        {
            instance.musicSource.Stop();
        }
    }
}
