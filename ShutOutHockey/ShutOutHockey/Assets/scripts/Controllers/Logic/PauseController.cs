using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour {
    public bool isPaused=false;
    public bool timeRemaining = true;
    public GameObject whistle;
    public GameObject pauseBG;
    public GameObject pauseMenu;
    public GameObject resumeButton;
    public GameObject timeOver;

    private void Start()
    {
        whistle = GameObject.FindGameObjectWithTag("Whistle");
        pauseBG = GameObject.FindGameObjectWithTag("PauseBG");
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        resumeButton = GameObject.FindGameObjectWithTag("ResumeButton");
        timeOver = GameObject.Find("TimeOver");
        pauseBG.SetActive(false);
        pauseMenu.SetActive(false);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
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
        foreach (GameObject target in GameObject.FindGameObjectsWithTag("Target"))
        {
            target.GetComponent<TargetController>().InactivateTarget();
        }
        foreach (GameObject puck in GameObject.FindGameObjectsWithTag("Puck"))
        {
            StopCoroutine("Shot");
            puck.GetComponent<PuckController>().activePuck = false;
        }
        pauseBG.SetActive(true);
        pauseMenu.SetActive(true);
        if (timeRemaining)
        {
            timeOver.SetActive(false);
            resumeButton.SetActive(true);
        }
        else
        {
            timeOver.SetActive(true);
            resumeButton.SetActive(false);
        }
        Time.timeScale = 0;
    }

    public void LockGame()
    {
        timeRemaining = false;
        whistle.GetComponent<AudioSource>().Play();
        Pause();
    }
}
