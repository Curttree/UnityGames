 using UnityEngine;
using static TargetStates;

public class TargetTouch : MonoBehaviour {
    private Renderer rend;
    public TargetState state=TargetState.Inactive;
    private TargetController targetController;
    private GameObject goalie;

	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
        targetController = GetComponent<TargetController>();
        goalie = GameObject.FindGameObjectWithTag("Player");
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
            goalie.GetComponent<GoalieController>().Save(0);
            state = TargetState.Inactive;
        }
        if (state == TargetState.Inactive)
            {
                rend.enabled = false;
                //rend.material.SetColor("_Color", Color.blue);
            }
    }
}
