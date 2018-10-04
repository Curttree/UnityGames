﻿using UnityEngine;
using static TargetStates;
using ObjectProvider;
using System.Collections;

public class TargetController : MonoBehaviour
{
    private SpriteRenderer rend;
    public float shotFrequency;
    private float timer = 0.0f;
    private float fadeDuration = 0.0f;
    private float frequencyOffset = 0.005f;
    private float magicNumber = 25f;
    public GameObject gameController;
    public ScoreController scoreController;
    private GameObject goalie;
    private GameObject goalHorn;
    private GameObject crowd;
    private OffenceController offenceController;
    private SpriteRenderer goalLightsRenderer;
    public int targetNumber;

    // Use this for initialization
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        goalHorn = GameObject.FindGameObjectWithTag("GoalHorn");
        goalie = GameObject.FindGameObjectWithTag("Player");
        gameController = GameObject.FindGameObjectWithTag("GameController");
        crowd = GameObject.FindGameObjectWithTag("Crowd");
        scoreController = gameController.GetComponent<ScoreController>();
        offenceController = gameController.GetComponent<OffenceController>();
        goalLightsRenderer = GameObject.FindGameObjectWithTag("GoalLights").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<TargetTouch>().state == TargetState.Active)
        {
            timer += Time.deltaTime;
            if (timer >= offenceController.shotFrequency)
            {
                timer = 0.0f;
                Goal();
            }
        }
        else if (timer > 0.0f)
        {
            timer = 0.0f;
        }
    }

    public void PrepShot()
    {
        //if (offenceController.saveStreak % 10 == 0)
        //{
        //    offenceController.shotFrequency *= (1f - frequencyOffset);
        //}
        //offenceController.shotFrequency = 1/(Mathf.Log10((scoreController.SA+1)-(1-Mathf.Pow(25,scoreController.goals))/4f)+0.5f);
        //offenceController.shotFrequency
        Debug.Log(offenceController.shotFrequency.ToString());
        rend.material.color = Color.blue;
        this.GetComponent<TargetTouch>().state = TargetState.Active;
        StartCoroutine(Activate(Color.blue));
        rend.enabled = true;
    }

    public void Goal()
    {
        offenceController.saveStreak = 0;
        //offenceController.shotFrequency *= (1f + frequencyOffset * (scoreController.SA / 10));
        gameController.GetComponent<ScoreController>().SA++;
        gameController.GetComponent<ScoreController>().goals++;

        crowd.GetComponent<CrowdController>().scale = crowd.GetComponent<CrowdController>().GetExcitement();
        goalHorn.GetComponent<AudioSource>().Play();
        rend.material.color = Color.red;
        rend.enabled = false;
        offenceController.gameStart = true;
        this.StartCoroutine(GoalLight());
        this.GetComponent<TargetTouch>().state = TargetState.Inactive;
    }

    public void Save()
    {
        offenceController.saveStreak++;
        goalie.GetComponent<GoalieController>().Save(targetNumber);
        offenceController.AcceleratePuck(this.gameObject);
        gameController.GetComponent<ScoreController>().SA++;
        gameController.GetComponent<ScoreController>().SV++;
        InactivateTarget();
    }

    public void InactivateTarget()
    {
        StartCoroutine(Inactivate(Color.blue));
        this.GetComponent<TargetTouch>().state = TargetState.Held;
    }

    IEnumerator Activate(Color targetColor)
    {
        for (float f = 0f; f <= 1; f += 0.15f)
        {
            Color c = targetColor;
            c.a = f;
            rend.material.color = c;
            yield return null;
        }
    }

    IEnumerator Inactivate(Color targetColor)
    {
        for (float f = 1f; f > 0; f -= 0.1f)
        {
            Color c = targetColor;
            c.a = f;
            rend.material.color = c;
            yield return null;
        }

    }

    IEnumerator GoalLight()
    {
        for (float f=0f; f<2f; f++)
        {
            Color temp = goalLightsRenderer.color;
            if (temp.a > 0.0f)
            {
                temp.a = 0.0f;
            }
            else
            {
                temp.a = 1.0f;
            }
            goalLightsRenderer.color = temp;
            yield return new WaitForSeconds(0.75f);
        }
    }
}
