using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public static bool isGamePaused;
    public static bool isSubMenuOpen;

    public GameObject pauseMenuUI;
    private GameObject targeting;
    private WeaponIK playerWeaponIK;

    private void Awake()
    {
        isGamePaused = false;
        isSubMenuOpen = false;

        //enable/disable WeaponIK and targetimg for player
        playerWeaponIK = GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponIK>();
        targeting = GameObject.FindGameObjectWithTag("Targeting");
    }
    // Update is called once per frame
    void Update()
    {
        CheckPause();
    }

    private void CheckPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                if (!isSubMenuOpen)
                    ContinueGame();
            }                
            else
                Pause();
        }
    }

    public void ContinueGame()
    {
        AudioManager.instanse.PlayMusic(AudioManager.instanse.mainTheme);

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        playerWeaponIK.enabled = true;
        targeting.SetActive(true);
        isGamePaused = false;
    }

    public void Pause()
    {
        AudioManager.instanse.PlayMusic(AudioManager.instanse.pauseTheme);

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        targeting.SetActive(false);
        playerWeaponIK.enabled = false;
        isGamePaused = true;
    }
     
}
