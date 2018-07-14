using UnityEngine;
using static TargetStates;

public class TouchController : MonoBehaviour {
    private GameObject[] targets;
    private GameObject goalie;
    private OffenceController offenceController;

    // Use this for initialization
    void Start () {
        targets = GameObject.FindGameObjectsWithTag("Target");
        goalie = GameObject.FindGameObjectWithTag("Player");
        offenceController = GameObject.FindGameObjectWithTag("GameController")?.GetComponent<OffenceController>();
        DeactivateTargets();
    }

    // Update is called once per frame
    void Update() {
        if (Input.touchCount > 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint((Input.GetTouch(0).position)), Vector2.zero);
            if (hit.collider.CompareTag("Target") && !offenceController.gameStart)
            {
                TargetTouch targetTouch = hit.collider.gameObject.GetComponent<TargetTouch>();
                targetTouch.OnMouseDown();
            }
        }
        else if (Input.GetMouseButton(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if(hit.collider != null)
            {
                if (hit.collider.CompareTag("Target"))
                {
                    TargetTouch targetTouch = hit.collider.gameObject.GetComponent<TargetTouch>();
                    targetTouch.OnMouseDown();
                }
            }
        }
        //else if (Input.touchCount <= 0 && !Input.GetMouseButton(0))
        else
        {
            DeactivateTargets();
        }
        if (Input.touchCount <= 0 && !Input.GetMouseButton(0))
        {
            goalie.GetComponent<GoalieController>().Save(0);
        }
    }

    void DeactivateTargets()
    {
        foreach (GameObject target in targets)
        {
              target.GetComponent<TargetTouch>().OnMouseExit();
        }
    }
}
