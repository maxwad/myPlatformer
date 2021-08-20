using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Camera cam;
    public Slider slider;
    private AudioSource audioSource;
    private Animator anim;

    private void Awake()
    {
        audioSource = cam.GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            audioSource.volume = PlayerPrefs.GetFloat("musicVolume");
            slider.value = audioSource.volume;
        }
        else
            PlayerPrefs.SetFloat("musicVolume", 1.0f);
    }

    private void Update()
    {
        ChangeVolume();
    }

    private void ChangeVolume()
    {
        audioSource.volume = slider.value;
        PlayerPrefs.SetFloat("musicVolume", slider.value);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Options()
    {
        anim.Play("OptionsAnimation");
    }

    public void OptionsBack()
    {
        anim.Play("ReverseOptionsAnimation");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
