using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public bool countDown = false;
    public int SV = 0;
    public int SA = 0;
    public int goals = 0;
    private float svPecent = 0.000f;
    private GameObject svPercentUI;
    private GameObject scoreUI;
    private GameObject timeUI;
    private TimeSpan gameStart;
    private PauseController pauseController;
    // Use this for initialization
    void Start()
    {
        svPercentUI = GameObject.FindGameObjectWithTag("SVPercent");
        scoreUI = GameObject.FindGameObjectWithTag("Score");
        timeUI = GameObject.FindGameObjectWithTag("Timer");
        pauseController = GetComponent<PauseController>();
        StartCoroutine(Scoreboard());
    }

    private void Update()
    {
        if (TimeSpan.FromSeconds(Time.timeSinceLevelLoad) > gameStart+TimeSpan.FromSeconds(121))
        {
            pauseController.LockGame();
        }
    }

    IEnumerator Timer()
    {
        TimeSpan gameTime;
        TimeSpan limit = TimeSpan.FromSeconds(120);
        TimeSpan remainingTime = limit;
        while (remainingTime > TimeSpan.FromSeconds(0))
        {
            gameTime = TimeSpan.FromSeconds(Time.timeSinceLevelLoad) - gameStart;
            remainingTime = limit - TimeSpan.FromSeconds(Math.Round(gameTime.TotalSeconds));
            timeUI.GetComponent<Text>().text = remainingTime.ToString(@"m\:ss");
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator Scoreboard()
    {
        while (true)
        {
            if (SA > 0)
                svPecent = (float)SV / SA;
            //Debug.Log($"SV: {SV}, SA: {SA}, SV%:{svPecent}");
            svPercentUI.GetComponent<Text>().text = $"Save% = {svPecent.ToString("0.000")}    SV = {SV.ToString()}     SA = {SA.ToString()}";
            scoreUI.GetComponent<Text>().text = goals.ToString();
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void StartTimer()
    {
        gameStart = TimeSpan.FromSeconds(Time.timeSinceLevelLoad);
        StartCoroutine(Timer());
    }
}
