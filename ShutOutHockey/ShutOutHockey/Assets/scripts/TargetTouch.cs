 using UnityEngine;
using static TargetStates;

public class TargetTouch : MonoBehaviour {
    private Renderer rend;
    public TargetState state=TargetState.Inactive;
    private TargetController targetController;
    private OffenceController offenceController;
    private GoalieController goalie;

	// Use this for initialization
	void Start () {
        state = TargetState.Inactive;
        rend = GetComponent<Renderer>();
        targetController = GetComponent<TargetController>();
        offenceController = GameObject.FindGameObjectWithTag("GameController").GetComponent<OffenceController>();
        goalie = GameObject.FindGameObjectWithTag("Player").GetComponent<GoalieController>();
    }
	
    public void OnMouseDown()
    {
        if (state == TargetState.Active)
        {

            targetController.Save();
        }
    }

    private void OnMouseEnter()
    {
        if (Input.GetMouseButton(0))
            OnMouseDown();
    }
    public void OnMouseExit()
    {
        if (state == TargetState.Held)
        {
            goalie.Save(0);
            //offenceController.AcceleratePuck(this.gameObject, offenceController.puckAcceleration * 3f);
            state = TargetState.Inactive;
        }
        if (state == TargetState.Inactive)
            {
                rend.enabled = false;
                //rend.material.SetColor("_Color", Color.blue);
            }
    }
}
