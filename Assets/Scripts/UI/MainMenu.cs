using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Camera cam;
    public Slider slider;
    private Animator anim;

    private AudioSource audioSource;
    public AudioClip clickSound;

    private void Awake()
    {
        audioSource = cam.GetComponent<AudioSource>();
        anim = GetComponent<Animator>();

        if (!PlayerPrefs.HasKey("masterVolume"))
        {
            PlayerPrefs.SetFloat("masterVolume", 0.5f);
            PlayerPrefs.Save();
        }        

        audioSource.volume = PlayerPrefs.GetFloat("masterVolume");
        slider.value = audioSource.volume;

    }

    public void ChangeVolume(float value)
    {
        audioSource.volume = value;
        PlayerPrefs.SetFloat("masterVolume", value);
        PlayerPrefs.Save();
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

    public void PlayMouseOverSound()
    {
        audioSource.PlayOneShot(clickSound);
    }
}
