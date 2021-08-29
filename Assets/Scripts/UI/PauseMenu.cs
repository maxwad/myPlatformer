using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{    
    public Slider[] volumeSliders;
    public Toggle[] resolutionToggles;
    public Toggle fullscreenToggle;

    public int[] screnWidths;

    private int lastResolutionIndex;

    private Animator anim;

    private bool isVideoMenuOpen = false;
    private bool isSoundMenuOpen = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();

        LaodOptions();
    }

    private void Update()
    {       
        CheckSubMenu();
    }

    private void LaodOptions()
    {
        lastResolutionIndex = PlayerPrefs.GetInt("Resolution");
        resolutionToggles[lastResolutionIndex].isOn = true;

        bool isFullscreen = (PlayerPrefs.GetInt("isFullscreen") == 1 ? true : false);
        fullscreenToggle.isOn = isFullscreen;
        SetFullscreen(isFullscreen);

        volumeSliders[0].value = AudioManager.instanse.masterVolume;
        volumeSliders[1].value = AudioManager.instanse.musicVolume;
        volumeSliders[2].value = AudioManager.instanse.effectsVolume;
    }

    public void SetScreenResolution(int i)
    {
        if (resolutionToggles[i].isOn)
        {
            float aspectRatio = 16.0f / 9.0f;
            lastResolutionIndex = i;
            Screen.SetResolution(screnWidths[i], (int)(screnWidths[i] / aspectRatio), false);
            PlayerPrefs.SetInt("Resolution", lastResolutionIndex);
            PlayerPrefs.Save();
        }
    }

    public void SetFullscreen(bool isFullscreen)
    {
        for (int i = 0; i < resolutionToggles.Length; i++)
        {
            resolutionToggles[i].interactable = !isFullscreen;
        }

        if (isFullscreen)
        {
            Resolution[] allResolutions = Screen.resolutions;
            Resolution maxResolution = allResolutions[allResolutions.Length - 1];
            Screen.SetResolution(maxResolution.width, maxResolution.height, true);
        } else
        {
            SetScreenResolution(lastResolutionIndex);
        }

        PlayerPrefs.SetInt("Fullscreen", ((isFullscreen) ? 1 : 0));
        PlayerPrefs.Save();
    }

    public void SetMasterVolume(float value)
    {
        AudioManager.instanse.SetVolume(value, AudioManager.AudioChannel.Master);
    }

    public void SetMusicVolume(float value)
    {
        AudioManager.instanse.SetVolume(value, AudioManager.AudioChannel.Music);
    }

    public void SetEffectsVolume(float value)
    {
        AudioManager.instanse.SetVolume(value, AudioManager.AudioChannel.Effects);
    }

    private void CheckSubMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && PauseMenuManager.isSubMenuOpen)
        {
            if (isSoundMenuOpen)
                SoundMenuBack();

            if (isVideoMenuOpen)
                VideoMenuBack();
        }
    }

    public void SoundMenu()
    {
        anim.Play("SoundAnimation");
        isSoundMenuOpen = true;
        PauseMenuManager.isSubMenuOpen = true;
    }

    public void SoundMenuBack()
    {
        anim.Play("ReverseSoundAnimation");
        isSoundMenuOpen = false;
        PauseMenuManager.isSubMenuOpen = false;
    }

    public void VideoMenu()
    {
        anim.Play("VideoAnimation");
        isVideoMenuOpen = true;
        PauseMenuManager.isSubMenuOpen = true;
    }

    public void VideoMenuBack()
    {
        anim.Play("ReverseVideoAnimation");
        isVideoMenuOpen = false;
        PauseMenuManager.isSubMenuOpen = false;
    }

    public void QuitGame()
    {
        Time.timeScale = 1.0f;
        PauseMenuManager.isGamePaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void PlayMouseOverSound()
    {
        AudioManager.instanse.PlaySound("ButtonSound");            
    }
}
