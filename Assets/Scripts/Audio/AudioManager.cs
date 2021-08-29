using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instanse;

    public enum AudioChannel {Master, Music, Effects};

    private float defaultMasterVolume = 0.5f;
    private float defaultMusicVolume = 1f;
    private float defaultEffectsVolume = 1f;
    public float masterVolume { get; private set; }
    public float musicVolume { get; private set; }
    public float effectsVolume { get; private set; }

    private int activeMusicSourceIndex;

    private AudioSource[] musicSources;
    public AudioClip mainTheme;
    public AudioClip pauseTheme;

    private AudioSource effectsAudioSource;

    public Transform audioListener;
    public Transform tPlayer;

    private SoundLibrary library;

    private void Awake()
    {
        if (instanse != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instanse = this;
            library = GetComponent<SoundLibrary>();

            LoadVolumes();
            CreateAudioSources();

            PlayMusic(mainTheme, 2.0f);
        }
        
    }

    private void Update()
    {
        if (tPlayer != null)
            audioListener.position = tPlayer.position;
    }

    private void CreateAudioSources()
    {
        // create audio source for effects
        GameObject effectsSource = new GameObject("EffectsSource");
        effectsAudioSource = effectsSource.AddComponent<AudioSource>();
        effectsAudioSource.transform.parent = transform;


        // create audio sources for music clips
        musicSources = new AudioSource[2];
        for (int i = 0; i < 2; i++)
        {
            GameObject newMusicSource = new GameObject("MusicSource_0" + (i + 1));
            musicSources[i] = newMusicSource.AddComponent<AudioSource>();
            newMusicSource.transform.parent = transform;
        }
    }

    private void LoadVolumes()
    {
        if (PlayerPrefs.HasKey("masterVolume"))
        {
            masterVolume = PlayerPrefs.GetFloat("masterVolume");
        }
        else
        {
            PlayerPrefs.SetFloat("masterVolume", defaultMasterVolume);
            masterVolume = defaultMasterVolume;
        }

        if (PlayerPrefs.HasKey("musicVolume"))
        {
            musicVolume = PlayerPrefs.GetFloat("musicVolume");
        }
        else
        {
            PlayerPrefs.SetFloat("musicVolume", defaultMusicVolume);
            musicVolume = defaultMusicVolume; 
        }

        if (PlayerPrefs.HasKey("effectsVolume"))
        {
            effectsVolume = PlayerPrefs.GetFloat("effectsVolume");
        }
        else
        {
            PlayerPrefs.SetFloat("effectsVolume", defaultEffectsVolume);
            effectsVolume = defaultEffectsVolume;
        }

    }

    public void SetVolume(float volumePercent, AudioChannel channel)
    {
        switch (channel)
        {
            case AudioChannel.Master:
                masterVolume = volumePercent;
                PlayerPrefs.SetFloat("masterVolume", masterVolume);
                break;

            case AudioChannel.Music:
                musicVolume = volumePercent;
                PlayerPrefs.SetFloat("musicVolume", musicVolume);
                break;

            case AudioChannel.Effects:
                effectsVolume = volumePercent;
                PlayerPrefs.SetFloat("effectsVolume", effectsVolume);
                break;
        }

        musicSources[activeMusicSourceIndex].volume = musicVolume * masterVolume;
        PlayerPrefs.Save();
    } 

    public void PlayMusic(AudioClip clip, float fadeDuration = 1.0f)
    {
        activeMusicSourceIndex = 1 - activeMusicSourceIndex;
        musicSources[activeMusicSourceIndex].clip = clip;
        musicSources[activeMusicSourceIndex].Play();

        StartCoroutine(AnimateMusicCrossfade(fadeDuration));
    }

    IEnumerator AnimateMusicCrossfade(float duration)
    {
        float percent = 0;

        while (percent < 1.0f)
        {
            percent += (0.02f * 1 / duration);
            musicSources[activeMusicSourceIndex].volume = Mathf.Lerp(0, musicVolume * masterVolume, percent);
            musicSources[1 - activeMusicSourceIndex].volume = Mathf.Lerp(musicVolume * masterVolume, 0, percent);

            yield return null;
        }
    }

    // method for specific effects
    public void PlaySound(AudioClip clip, Vector3 pos)
    {
        AudioSource.PlayClipAtPoint(clip, pos, effectsVolume * masterVolume);
    }

    // method for random effects
    public void PlaySound(string nameClip, Vector3 pos)
    {
        PlaySound(library.GetRandomClipFromGroup(nameClip), pos);
    }

    // method for UI effects
    public void PlaySound(string nameClip)
    {
        effectsAudioSource.PlayOneShot(library.GetSpecificClipFromName(nameClip), effectsVolume * masterVolume);        
    }
}
