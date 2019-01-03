using System.Collections;
using UnityEngine;

public class PuckController : MonoBehaviour {

    private float puckSpeed;

    public float acceleration = 0f;

    public bool activePuck = true;

    public Transform target;

    public Transform start;

    public GameController gameController;
    
    public void Shot(Transform startTransform,Transform targetTransform,float speed)
    {
        puckSpeed = speed;
        target = targetTransform;
        start = startTransform;
    }
    public void UpdateSpeed(float newSpeed)
    {
        puckSpeed = newSpeed;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, target.position) >= 0.01f && activePuck)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, (puckSpeed + acceleration));
        }
        else
        {
            var save = target.gameObject.GetComponent<TargetTouch>()?.state == TargetStates.TargetState.Held;
            var newTarget = target.gameObject.GetComponent<TargetController>()?.reflectionTarget;
            if (newTarget != null && save)
            {
                target = newTarget;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
