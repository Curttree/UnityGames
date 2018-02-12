using UnityEngine;
using static TargetStates;
using ObjectProvider;

public class TargetController : MonoBehaviour {
    private Renderer rend;
    private float shotFrequency;
    private float timer = 0.0f;
    public GameObject gameController;
    private GameObject goalie;
    private GameObject goalHorn;
    public int targetNumber;

    // Use this for initialization
    void Start()
    {
        rend = GetComponent<Renderer>();
        goalHorn = GameObject.FindGameObjectWithTag("GoalHorn");
        goalie = GameObject.FindGameObjectWithTag("Player");
        gameController = GameObject.FindGameObjectWithTag("GameController");
        shotFrequency = gameController.GetComponent<OffenceController>().shotFrequency;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<TargetTouch>().state == TargetState.Active) { 
        timer += Time.deltaTime;
        if (timer >= shotFrequency)
            {
                timer = 0.0f;
                Goal();
            }
        }
        else if (timer>0.0f)
        {
            timer = 0.0f;
        }
    }
    
    public void PrepShot()
    {
        rend.enabled = true;
        rend.material.SetColor("_Color", Color.yellow);
        this.GetComponent<TargetTouch>().state = TargetState.Active;
        //Debug.Log(rend.gameObject.name);
    }

    public void Goal()
    {
        gameController.GetComponent<ScoreController>().SA++;
        gameController.GetComponent<ScoreController>().goals++;
        goalHorn.GetComponent<AudioSource>().Play();
        rend.enabled = false;
        this.GetComponent<TargetTouch>().state = TargetState.Inactive;
    }

    public void Save()
    {
        goalie.GetComponent<GoalieController>().Save(targetNumber);
        gameController.GetComponent<ScoreController>().SA++;
        gameController.GetComponent<ScoreController>().SV++;
        rend.enabled = false;
        this.GetComponent<TargetTouch>().state = TargetState.Inactive;
    }
}
