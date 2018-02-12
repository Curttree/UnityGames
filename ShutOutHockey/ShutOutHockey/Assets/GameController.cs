using UnityEngine;

public class GameController : MonoBehaviour {

    bool isPaused = false;
    public GameObject whistle;

    // Use this for initialization
    void Start () {
        whistle = GameObject.FindGameObjectWithTag("Whistle");
        if(Application.platform == RuntimePlatform.Android)
            Screen.SetResolution(960, 640, true);
	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnApplicationFocus(bool hasFocus)
    {
        isPaused = !hasFocus;
        OnApplicationPause(isPaused);
        if (hasFocus)
            StartPlay();
    }

    void OnApplicationPause(bool pauseStatus)
    {
        isPaused = pauseStatus;
        if (isPaused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
    public void StartPlay()
    {
        //whistle.GetComponent<AudioSource>().Play();
    }
}
