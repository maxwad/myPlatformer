using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public static bool isGamePaused;
    public static bool isSubMenuOpen;

    public GameObject pauseMenuUI;
    public WeaponIK playerWeaponIK;

    private void Awake()
    {
        isGamePaused = false;
        isSubMenuOpen = false;

        //enable/disable WeaponIK for player
        playerWeaponIK = GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponIK>();
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
        isGamePaused = false;
    }

    public void Pause()
    {
        AudioManager.instanse.PlayMusic(AudioManager.instanse.pauseTheme);

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        playerWeaponIK.enabled = false;
        isGamePaused = true;
    }
     
}
