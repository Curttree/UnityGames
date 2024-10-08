﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public int gameLength = 60;
    public int SV = 0;
    public int SA = 0;
    public int goals = 0;
    public float difficultyIncrease = 0.05f;
    public float difficultyDecrease = -0.01f;
    public float minDifficulty = 0.5f;
    private float svPecent = 0.000f;
    public GameObject svPercentUI;
    public GameObject svPercentUI2;
    private GameObject scoreUI;
    private GameObject timeUI;
    private GameObject streakUI;
    private TimeSpan gameStart;
    private PauseController pauseController;
    private OffenceController offenceController;
    // Use this for initialization
    void Start()
    {
        scoreUI = GameObject.FindGameObjectWithTag("Score");
        timeUI = GameObject.FindGameObjectWithTag("Timer");
        streakUI = GameObject.Find("Streak");
        pauseController = GetComponent<PauseController>();
        offenceController= GetComponent<OffenceController>();
        StartCoroutine(Scoreboard());

        timeUI.GetComponent<Text>().text = TimeSpan.FromSeconds(gameLength).ToString(@"m\:ss");
    }

    private void Update()
    {
        if (TimeSpan.FromSeconds(Time.timeSinceLevelLoad) > gameStart+TimeSpan.FromSeconds(gameLength+1) && !pauseController.isPaused)
        {
            if (goals == 0)
            {
                pauseController.timeOver.GetComponent<Text>().text = "SHUT OUT";
            }
            pauseController.LockGame();
        }
    }

    IEnumerator Timer()
    {
        TimeSpan gameTime;
        TimeSpan limit = TimeSpan.FromSeconds(gameLength);
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
            svPercentUI.GetComponent<Text>().text = $"Save%|{svPecent.ToString("0.000")}    SV|{SV.ToString()}     SA|{SA.ToString()}";
            svPercentUI2.GetComponent<Text>().text = $"Save%|{svPecent.ToString("0.000")}    SV|{SV.ToString()}     SA|{SA.ToString()}";
            streakUI.GetComponent<Text>().text = $"Streak|{offenceController.saveStreak}";
            scoreUI.GetComponent<Text>().text = goals.ToString();
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void StartTimer()
    {
        gameStart = TimeSpan.FromSeconds(Time.timeSinceLevelLoad);
        StartCoroutine(Timer());
    }

    public void UpdatePrefs()
    {
        int SAPref = 0;
        int SVPref = 0;
        float currentDifficulty = 1f;

        if (PlayerPrefs.HasKey("SA"))
        {
            SAPref = PlayerPrefs.GetInt("SA");
        }
        if (PlayerPrefs.HasKey("SV"))
        {
            SVPref = PlayerPrefs.GetInt("SV");
        }

        if (PlayerPrefs.HasKey("Difficulty"))
        {
            currentDifficulty = PlayerPrefs.GetFloat("Difficulty");
        }

        float difficultyChange = 0f;
        if (TimeSpan.FromSeconds(Time.timeSinceLevelLoad) > gameStart + TimeSpan.FromSeconds(gameLength + 1))
        {
            difficultyChange = goals == 0 ? difficultyIncrease : difficultyDecrease * goals;
        }
        PlayerPrefs.SetFloat("Difficulty", currentDifficulty + difficultyChange > minDifficulty ? currentDifficulty + difficultyChange : minDifficulty);
        PlayerPrefs.SetInt("SA", SAPref + SA);
        PlayerPrefs.SetInt("SV", SVPref + SV);
    }
}
