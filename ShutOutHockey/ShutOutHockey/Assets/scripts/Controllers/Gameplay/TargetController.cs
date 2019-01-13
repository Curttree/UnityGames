using UnityEngine;
using static TargetStates;
using System.Collections;

public class TargetController : MonoBehaviour
{
    private SpriteRenderer rend;
    public float shotFrequency;
    private float timer = 0.0f;
    public GameObject gameController;
    public ScoreController scoreController;
    private GameObject goalie;
    private GameObject goalHorn;
    private GameObject crowd;
    private OffenceController offenceController;
    private SpriteRenderer goalLightsRenderer;
    private MusicController musicController;
    public Color activeColor;
    public Transform reflectionTarget;

    public int targetNumber;

    // Use this for initialization
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        goalHorn = GameObject.FindGameObjectWithTag("GoalHorn");
        goalie = GameObject.FindGameObjectWithTag("Player");
        gameController = GameObject.FindGameObjectWithTag("GameController");
        musicController = gameController.GetComponent<MusicController>();
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
            if (timer >= offenceController.timeToNet)
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
        rend.material.color = activeColor;
        this.GetComponent<TargetTouch>().state = TargetState.Active;
        StartCoroutine(Activate(activeColor));
        rend.enabled = true;
    }

    public void Goal()
    {
        offenceController.saveStreak = 0;
        gameController.GetComponent<ScoreController>().SA++;
        gameController.GetComponent<ScoreController>().goals++;
        
        crowd.GetComponent<CrowdController>().scale = crowd.GetComponent<CrowdController>().GetExcitement();
        musicController.PlaySource(goalHorn.GetComponent<AudioSource>(),AudioCategory.SoundEffect);
        rend.material.color = Color.red;
        rend.enabled = false;
        offenceController.gameStart = true;
        this.StartCoroutine(GoalLight());
        offenceController.InactivateAllTargets();
        this.GetComponent<TargetTouch>().state = TargetState.Inactive;
    }

    public void Save()
    {
        offenceController.saveStreak++;
        goalie.GetComponent<GoalieController>().Save(targetNumber);
        offenceController.AcceleratePuck(this.gameObject, offenceController.puckAcceleration);
        gameController.GetComponent<ScoreController>().SA++;
        gameController.GetComponent<ScoreController>().SV++;
        InactivateTarget();
    }

    public void InactivateTarget()
    {
        StartCoroutine(Inactivate(activeColor));
        this.GetComponent<TargetTouch>().state = TargetState.Held;
    }

    IEnumerator Activate(Color targetColor)
    {
        for (float f = 0f; f <= 1; f += 0.05f)
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
