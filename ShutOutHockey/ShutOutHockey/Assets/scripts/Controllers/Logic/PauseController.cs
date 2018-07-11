using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour {
    public bool isPaused=false;
    public bool timeRemaining = true;
    public GameObject whistle;
    public GameObject pauseBG;
    public GameObject pauseMenu;

    private void Start()
    {
        whistle = GameObject.FindGameObjectWithTag("Whistle");
        pauseBG = GameObject.FindGameObjectWithTag("PauseBG");
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        pauseBG.SetActive(false);
        pauseMenu.SetActive(false);
    }

    public void OnClick()
    {
        whistle.GetComponent<AudioSource>().Play();
        if (timeRemaining)
        {
            if (isPaused)
            {
                UnPause();
            }
            else
            {
                Pause();
            }
        }
    }
    private void UnPause()
    {
        isPaused = false;
        pauseBG.SetActive(false);
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    private void Pause()
    {
        isPaused = true;
        pauseBG.SetActive(true);
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void LockGame()
    {
        timeRemaining = false;
        whistle.GetComponent<AudioSource>().Play();
        Pause();
    }
}
