using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    bool isPaused = false;
    public PauseController pause;
    public OffenceController offenceController;

    // Use this for initialization
    void Start () {
        Time.timeScale = 1;
        offenceController = GameObject.FindGameObjectWithTag("GameController")?.GetComponent<OffenceController>();
        pause = GameObject.FindGameObjectWithTag("GameController")?.GetComponent<PauseController>();
        if (Application.platform == RuntimePlatform.Android)
            Screen.SetResolution(960, 640, true);
	}

    void OnApplicationFocus(bool hasFocus)
    {
        if (pause != null)
        {
            pause.isPaused = false;
            pause.OnClick();
        }
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pause != null)
        {
            pause.isPaused = false;
            pause.OnClick();
        }
    }

    public void ExitToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("menu");
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("gameplay");
    }
}
