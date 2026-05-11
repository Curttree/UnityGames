using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameController : MonoBehaviour {
    
    public PauseController pause;
    public OffenceController offenceController;
    public ScoreController scoreController;

    // Use this for initialization
    void Start () {
        Time.timeScale = 1f;
        offenceController = GameObject.FindGameObjectWithTag("GameController")?.GetComponent<OffenceController>();
        scoreController = GameObject.FindGameObjectWithTag("GameController")?.GetComponent<ScoreController>();
        pause = GameObject.FindGameObjectWithTag("GameController")?.GetComponent<PauseController>();
        UpdateSVPercent();
        //if (Application.platform == RuntimePlatform.Android)
        //TODO: Test this resolution..
        int height = 810;
        int width = 1440;
        float currentRatio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        width = Mathf.Clamp((int)(height * currentRatio), 1200, 1440);
        Screen.SetResolution(width, height, true);
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
        scoreController.UpdatePrefs();
        SceneManager.LoadScene("menu");
    }

    public void Restart()
    {
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
}
