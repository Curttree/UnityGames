using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour {
    public bool isPaused=false;
    public GameObject whistle;
    public GameObject pauseBG;

    private void Start()
    {
        whistle = GameObject.FindGameObjectWithTag("Whistle");
        pauseBG = GameObject.FindGameObjectWithTag("PauseBG");
        pauseBG.SetActive(false);
    }

    public void OnClick()
    {
        whistle.GetComponent<AudioSource>().Play();
        if (isPaused)
        {
            UnPause();
        }
        else
        {
            Pause();
        }
    }
    private void UnPause()
    {
        isPaused = false;
        pauseBG.SetActive(false);
        Time.timeScale = 1;
    }
    private void Pause()
    {
        isPaused = true;
        pauseBG.SetActive(true);
        Time.timeScale = 0;
    }
}
