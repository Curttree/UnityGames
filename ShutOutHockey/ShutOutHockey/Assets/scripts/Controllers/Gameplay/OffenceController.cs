using System.Collections.Generic;
using UnityEngine;
using static TargetStates;

public class OffenceController : MonoBehaviour {
    private float startDelay = 2.5f;
    public bool gameStart = true;
    public bool timerStarted = false;
    public float shotFrequency = 0.5f;
    public float timeToNet = 1f;
    private GameObject[] targets;
    public GameObject whistle;
    public GameObject puck;
    private GameObject crowd;
    public Transform shotStart;
    public ScoreController scoreController;
    private MusicController musicController;
    private PauseController pauseController;
    private float timer = 0.0f;
    public float puckAcceleration = 1.25f;
    public float gameDifficulty = 1f;
    public int saveStreak = 0;
    public float defaultDifficulty = 0.75f;
    public AudioSource slapShot;
    public AudioSource slapShot2;
    public AudioSource organ;
    public AudioSource crowdSound;

    // Use this for initialization
    void Start()
    {
        whistle = GameObject.FindGameObjectWithTag("Whistle");
        targets = GameObject.FindGameObjectsWithTag("Target");
        crowd = GameObject.FindGameObjectWithTag("Crowd");
        shotStart = GameObject.FindGameObjectWithTag("ShotStart").transform;
        scoreController = GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreController>();
        musicController = GameObject.FindGameObjectWithTag("GameController").GetComponent<MusicController>();
        pauseController = GameObject.FindGameObjectWithTag("GameController").GetComponent<PauseController>();
        gameDifficulty = GetDifficulty();
        if (PlayerPrefs.HasKey("BGMusic") & PlayerPrefs.GetInt("BGMusic") != 0)
        {
            musicController.PlaySource(organ, AudioCategory.BGMusic);
            musicController.PlaySource(crowdSound, AudioCategory.BGMusic);
        }
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;
        if (timer >= shotFrequency/gameDifficulty*1.2f && !gameStart && !pauseController.isPaused)
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
            musicController.PlaySource(whistle.GetComponent<AudioSource>(), AudioCategory.SoundEffect);
        }
	}

    void ShootPuck()
    {
        int shotLocation = SelectTarget(targets); 
        GameObject target = targets[shotLocation];
        PlaySlapShotSound(target.GetComponent<TargetController>().targetNumber);
        CalculateShotSpeed(target.transform, shotStart.transform);
        target.GetComponent<TargetController>().PrepShot();
        crowd.GetComponent<CrowdController>().scale = crowd.GetComponent<CrowdController>().GetExcitement();

        GameObject puckClone = Instantiate(puck, shotStart.position,shotStart.rotation);
        puckClone.GetComponent<PuckController>().Shot(shotStart,target.transform,1f/(timeToNet*1.5f));
    }

    int SelectTarget(GameObject[] possibleTargets)
    {
        int shotLocation = Random.Range(0, targets.Length);
        TargetState targetState = targets[shotLocation].GetComponent<TargetTouch>().state;
        if (targetState == TargetState.Held || targetState == TargetState.Active)
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
        float dynamicBonus = Mathf.Log((saveStreak > 0 ? saveStreak:1f),100f) / 5f ;
        timeToNet = (shotFrequency / gameDifficulty) - dynamicBonus;
        timeToNet *= Time.deltaTime * 100f;
        float calcSpeed = Vector3.Distance(puck.position, target.position) / timeToNet;
        float retVal = calcSpeed * gameDifficulty;
        //print($"STREAK: {saveStreak} SPEED: {retVal.ToString()}");
        return retVal;
    }

    public void AcceleratePuck(GameObject target, float acceleration)
    {
        foreach (GameObject puck in GameObject.FindGameObjectsWithTag("Puck"))
        {
            if (puck.GetComponent<PuckController>().target = target.transform)
            {
                //print($"accelerating puck heading towards {target.GetComponent<TargetController>().targetNumber.ToString()}");
                puck.GetComponent<PuckController>().acceleration = acceleration;
            }
        }
    }

    public void InactivateAllTargets()
    {
        foreach (GameObject puck in GameObject.FindGameObjectsWithTag("Puck"))
        {
            Destroy(puck);
        }
        foreach (GameObject target in GameObject.FindGameObjectsWithTag("Target"))
        {
            if (target.GetComponent<TargetTouch>().state != TargetState.Held)
            {
                target.GetComponent<TargetController>().InactivateTarget();
                target.GetComponent<TargetTouch>().state = TargetState.Inactive;
                target.GetComponent<Renderer>().enabled = false;
            }
        }
    }


    public void PlaySlapShotSound(int targetNumber)
    {
        if (targetNumber > 2)
        {
            if (slapShot.isActiveAndEnabled)
            {
                musicController.PlaySource(slapShot,AudioCategory.SoundEffect);
            }
        }
        else
        {
            if (slapShot2.isActiveAndEnabled)
            {
                musicController.PlaySource(slapShot2,AudioCategory.SoundEffect);
            } 
        }
    }

    private float GetDifficulty()
    {
        float difficulty = PlayerPrefs.HasKey("Difficulty") ? PlayerPrefs.GetFloat("Difficulty") : defaultDifficulty;
        return difficulty;
    }
}
