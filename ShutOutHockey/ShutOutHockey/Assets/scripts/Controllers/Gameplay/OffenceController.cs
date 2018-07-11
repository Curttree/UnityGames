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
    public Transform shotStart;
    public ScoreController scoreController;
    private float timer = 0.0f;

    // Use this for initialization
    void Start()
    {
        whistle = GameObject.FindGameObjectWithTag("Whistle");
        targets = GameObject.FindGameObjectsWithTag("Target");
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
        target.GetComponent<TargetController>().PrepShot();
        float speed = CalculateShotSpeed(target.transform, shotStart);
        GameObject puckClone = Instantiate(puck, shotStart);
        StartCoroutine(puckClone.GetComponent<PuckController>().Shot(shotStart,target.transform,1f));
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
        float speed=0f;
        speed = Vector2.Distance(target.position, puck.position) / shotFrequency;
        return speed; 
    }
}
