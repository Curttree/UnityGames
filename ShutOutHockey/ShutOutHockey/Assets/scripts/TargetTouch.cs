 using UnityEngine;
using static TargetStates;

public class TargetTouch : MonoBehaviour {
    private Renderer rend;
    public TargetState state=TargetState.Inactive;
    public GameObject effect;
    private TargetController targetController;
    private GoalieController goalie;

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
            state = TargetState.Inactive;
        }
        if (state == TargetState.Inactive)
            {
                rend.enabled = false;
            }
    }
}
