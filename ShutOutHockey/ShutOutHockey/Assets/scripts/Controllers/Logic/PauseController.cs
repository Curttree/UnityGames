using UnityEngine;

public class PauseController : MonoBehaviour {
    public bool isPaused=false;
    public bool timeRemaining = true;
    public GameObject whistle;
    public GameObject pauseBG;
    public GameObject pauseMenu;
    public GameObject resumeButton;
    public GameObject timeOver;
    private MusicController musicController;
    private AdManager bannerWrapper;
    private void Start()
    {
        whistle = GameObject.FindGameObjectWithTag("Whistle");
        pauseBG = GameObject.FindGameObjectWithTag("PauseBG");
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        resumeButton = GameObject.FindGameObjectWithTag("ResumeButton");
        timeOver = GameObject.Find("TimeOver");
        musicController = GameObject.FindGameObjectWithTag("GameController").GetComponent<MusicController>();
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
        if (timeRemaining)
        {
            musicController.PlaySource(whistle.GetComponent<AudioSource>(),AudioCategory.SoundEffect);
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
        if (bannerWrapper == null)
        {
            GetAdServer();
        }
        bannerWrapper?.bannerView?.Hide();
        isPaused = false;
        pauseBG.SetActive(false);
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    private void Pause()
    {
        if (bannerWrapper == null)
        {
            GetAdServer();
        }
        bannerWrapper?.bannerView?.Show();
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
        musicController.PlaySource(whistle.GetComponent<AudioSource>(),AudioCategory.SoundEffect);
        Pause();
    }

    private void GetAdServer()
    {
        GameObject myGameObject = GameObject.Find("AdManager");
        if (myGameObject == null)
        {
            CreateAdServer();
        }
        bannerWrapper = myGameObject.GetComponent<AdManager>();
    }

    private void CreateAdServer()
    {
        // Create a wrapper GameObject to hold the banner.
        GameObject myGameObject = new GameObject("AdManager");
        myGameObject.AddComponent<AdManager>();
        // Mark the GameObject not to be destroyed when new scenes load.
        DontDestroyOnLoad(myGameObject);
    }
}
