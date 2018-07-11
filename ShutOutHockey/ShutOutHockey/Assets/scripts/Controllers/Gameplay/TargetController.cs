using UnityEngine;
using static TargetStates;
using ObjectProvider;
using System.Collections;

public class TargetController : MonoBehaviour
{
    private SpriteRenderer rend;
    private float shotFrequency;
    private float timer = 0.0f;
    private float fadeDuration = 0.0f;
    private float frequencyOffset = 0.075f;
    private int saveStreak = 0;
    public GameObject gameController;
    public ScoreController scoreController;
    private GameObject goalie;
    private GameObject goalHorn;
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
        scoreController = gameController.GetComponent<ScoreController>();
        offenceController = gameController.GetComponent<OffenceController>();
        goalLightsRenderer = GameObject.FindGameObjectWithTag("GoalLights").GetComponent<SpriteRenderer>();
        shotFrequency = offenceController.shotFrequency;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<TargetTouch>().state == TargetState.Active)
        {
            timer += Time.deltaTime;
            if (timer >= shotFrequency)
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
        if (saveStreak >= 10)
        {
            saveStreak = 0;
            offenceController.shotFrequency *= (1f - frequencyOffset);
        }
        rend.material.color = Color.blue;
        this.GetComponent<TargetTouch>().state = TargetState.Active;
        StartCoroutine(Activate(Color.blue));
        rend.enabled = true;
        //Debug.Log(rend.gameObject.name + '=' + this.GetComponent<TargetTouch>().state.ToString());
    }

    public void Goal()
    {
        saveStreak = 0;
        offenceController.shotFrequency *= (1f + frequencyOffset * (scoreController.SA / 10));
        gameController.GetComponent<ScoreController>().SA++;
        gameController.GetComponent<ScoreController>().goals++;
        goalHorn.GetComponent<AudioSource>().Play();
        rend.material.color = Color.red;
        rend.enabled = false;
        offenceController.gameStart = true;
        this.StartCoroutine(GoalLight());
        this.GetComponent<TargetTouch>().state = TargetState.Inactive;
    }

    public void Save()
    {
        saveStreak++;
        goalie.GetComponent<GoalieController>().Save(targetNumber);
        gameController.GetComponent<ScoreController>().SA++;
        gameController.GetComponent<ScoreController>().SV++;
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
