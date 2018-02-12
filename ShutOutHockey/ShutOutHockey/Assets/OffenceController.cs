using UnityEngine;
using static TargetStates;

public class OffenceController : MonoBehaviour {
    private float startDelay = 2.5f;
    private bool gameStart = true;
    public float shotFrequency = 0.5f;
    private GameObject[] targets;
    public GameObject whistle;
    private float timer = 0.0f;

    // Use this for initialization
    void Start()
    {
        whistle = GameObject.FindGameObjectWithTag("Whistle");
        targets = GameObject.FindGameObjectsWithTag("Target");
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
            gameStart = false;
            whistle.GetComponent<AudioSource>().Play();
        }
	}

    void ShootPuck()
    {
        int shotLocation = SelectTarget(); 
        GameObject target = targets[shotLocation];
        target.GetComponent<TargetController>().PrepShot();
    }

    int SelectTarget()
    {
        int shotLocation = Random.Range(0, targets.Length);
        TargetState targetState = targets[shotLocation].GetComponent<TargetTouch>().state;
        if (targetState == TargetState.Active)
        {
            shotLocation = SelectTarget();
        }
        return shotLocation;
    }
}
