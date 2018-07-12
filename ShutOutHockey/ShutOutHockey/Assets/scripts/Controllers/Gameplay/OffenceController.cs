using System.Collections.Generic;
using UnityEngine;
using static TargetStates;

public class OffenceController : MonoBehaviour {
    private float startDelay = 2.5f;
    public bool gameStart = true;
    public bool timerStarted = false;
    public float shotFrequency = 0.5f;
    private GameObject[] targets;
    public GameObject whistle;
    public GameObject puck;
    private GameObject crowd;
    public Transform shotStart;
    public ScoreController scoreController;
    private float timer = 0.0f;
    public float puckAcceleration = 10f;
    public float magicSpeed = 25f;
    public int saveStreak = 0;

    // Use this for initialization
    void Start()
    {
        whistle = GameObject.FindGameObjectWithTag("Whistle");
        targets = GameObject.FindGameObjectsWithTag("Target");
        crowd = GameObject.FindGameObjectWithTag("Crowd");
        shotStart = GameObject.FindGameObjectWithTag("ShotStart").transform;
        scoreController = GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreController>();
    }

    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;
        if (timer >= shotFrequency && !gameStart)
        {
            timer = 0.0f;
            ShootPuck();
        }
        if (timer >= startDelay && gameStart)
        {
            timer = 0.0f;
            if (!timerStarted)
            {
                timerStarted = true;
                scoreController.StartTimer();
            }
            gameStart = false;
            whistle.GetComponent<AudioSource>().Play();
        }
	}

    void ShootPuck()
    {
        int shotLocation = SelectTarget(targets); 
        GameObject target = targets[shotLocation];
        float speed = CalculateShotSpeed(target.transform, shotStart);

        target.GetComponent<TargetController>().PrepShot();
        crowd.GetComponent<CrowdController>().scale = crowd.GetComponent<CrowdController>().GetExcitement();

        GameObject puckClone = Instantiate(puck, shotStart.position,shotStart.rotation);
        puckClone.GetComponent<PuckController>().targetObject = target;
        StartCoroutine(puckClone.GetComponent<PuckController>().Shot(shotStart,target.transform,speed));
    }

    int SelectTarget(GameObject[] possibleTargets)
    {
        int shotLocation = Random.Range(0, targets.Length);
        TargetState targetState = targets[shotLocation].GetComponent<TargetTouch>().state;
        if (targetState == TargetState.Held)
        {
            List<GameObject> list = new List<GameObject>(possibleTargets);
            list.Remove(targets[shotLocation]);
            possibleTargets = list.ToArray();
            shotLocation = SelectTarget(possibleTargets);
        }
        return shotLocation;
    }

    float CalculateShotSpeed(Transform target,Transform puck)
    {
        return (Vector3.Distance(puck.position, target.position) / (shotFrequency*magicSpeed));
    }

    public void AcceleratePuck(GameObject target)
    {
        foreach (GameObject puck in GameObject.FindGameObjectsWithTag("Puck"))
        {
            if (puck.GetComponent<PuckController>().targetObject = target)
            {
                puck.GetComponent<PuckController>().acceleration = puckAcceleration;
            }
        }
    }
}
