using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameController : MonoBehaviour {
    
    public PauseController pause;
    public OffenceController offenceController;
    public ScoreController scoreController;

    private AdManager bannerWrapper;

    private BannerView bannerView;
    // Use this for initialization
    void Start () {
        Time.timeScale = 1f;
        offenceController = GameObject.FindGameObjectWithTag("GameController")?.GetComponent<OffenceController>();
        scoreController = GameObject.FindGameObjectWithTag("GameController")?.GetComponent<ScoreController>();
        pause = GameObject.FindGameObjectWithTag("GameController")?.GetComponent<PauseController>();
        UpdateSVPercent();
        //if (Application.platform == RuntimePlatform.Android)
        Screen.SetResolution(960, 640, true);
        GameObject myGameObject = GetAdServer();
        if (myGameObject != null)
        {
            bannerWrapper = myGameObject.GetComponent<AdManager>();
            bannerWrapper.bannerView?.Hide();
        }
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
        if (bannerWrapper == null)
        {
            GetAdServer();
        }
        bannerWrapper?.bannerView?.Hide();
        Time.timeScale = 1;
        scoreController.UpdatePrefs();
        SceneManager.LoadScene("menu");
    }

    public void Restart()
    {
        if (bannerWrapper == null)
        {
            GetAdServer();
        }
        bannerWrapper?.bannerView?.Hide();
        Time.timeScale = 1;
        scoreController.UpdatePrefs();
        SceneManager.LoadScene("gameplay");
    }

    private void UpdateSVPercent()
    {
        var svpercent = GameObject.Find("SvPercentGlobal");
        if (svpercent != null)
        {
            float percent = (float)PlayerPrefs.GetInt("SV") / (PlayerPrefs.GetInt("SA") > 0 ? PlayerPrefs.GetInt("SA") : 1);
            svpercent.GetComponent<Text>().text = $"SV%|{percent.ToString("0.000")}"; 
        }
    }

    private GameObject GetAdServer()
    {
        GameObject myGameObject = GameObject.Find("AdManager");
        if (myGameObject == null)
        {
            myGameObject = CreateAdServer();
        }
        bannerWrapper = myGameObject.GetComponent<AdManager>();
        return myGameObject;
    }

    private GameObject CreateAdServer()
    {
        // Create a wrapper GameObject to hold the banner.
        GameObject myGameObject = new GameObject("AdManager");
        myGameObject.AddComponent<AdManager>();
        // Mark the GameObject not to be destroyed when new scenes load.
        DontDestroyOnLoad(myGameObject);
        return myGameObject;
    }
}
