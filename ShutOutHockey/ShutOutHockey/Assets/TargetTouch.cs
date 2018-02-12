 using UnityEngine;
using static TargetStates;

public class TargetTouch : MonoBehaviour {
    private Renderer rend;
    public TargetState state=TargetState.Inactive;
    private TargetController targetController;
	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
        targetController = GetComponent<TargetController>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonUp(0))
            OnMouseExit();
    }

    public void OnMouseDown()
    {
        rend.material.SetColor("_Color", Color.green);
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
        if (state == TargetState.Inactive)
            rend.enabled = false;
            rend.material.SetColor("_Color", Color.red);
    }
}
