using System.Collections;
using UnityEngine;
using static TargetStates;

public class TargetTouch : MonoBehaviour {
    private Renderer rend;
    public TargetState state = TargetState.Inactive;
    public GameObject effect;
    private TargetController targetController;
    private GoalieController goalie;
    private float saveStartTime;
    private float minSaveTime = 0.25f;

    // Use this for initialization
    void Start () {
        state = TargetState.Inactive;
        rend = GetComponent<Renderer>();
        targetController = GetComponent<TargetController>();
        goalie = GameObject.FindGameObjectWithTag("Player").GetComponent<GoalieController>();
    }
	
    public void OnMouseDown()
    {
        if (state == TargetState.Active)
        {
            if (effect != null)
            {
                Instantiate(effect, gameObject.transform);
            }
            saveStartTime = Time.time;
            targetController.Save();
        }
    }

    private void OnMouseEnter()
    {
        if (Input.GetMouseButton(0))
        {
            OnMouseDown();
        }
    }
    public void OnMouseExit()
    {
        if (state == TargetState.Held)
        {
            float saveDuration = Time.time - saveStartTime;
            if (saveDuration < minSaveTime)
            {
                goalie.GetComponent<GoalieController>().delayedSave = true;
                StartCoroutine(DelayedSave());
            }
            else
            {
                goalie.Save(0);
            }
            state = TargetState.Inactive;
        }
        if (state == TargetState.Inactive)
        {
            rend.enabled = false;
        }
    }

    IEnumerator DelayedSave()
    {
        for (float f = 0; f < 1; f++)
        {
            yield return new WaitForSeconds(minSaveTime);
        }
        if (goalie.GetComponent<GoalieController>().delayedSave)
        {
            goalie.Save(0);
        }
    }

    public bool IsHeld()
    {
        return goalie.GetComponent<GoalieController>().delayedSave;
    }
}
