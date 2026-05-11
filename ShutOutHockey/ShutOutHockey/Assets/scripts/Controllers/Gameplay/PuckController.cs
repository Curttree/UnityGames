using UnityEngine;

public class PuckController : MonoBehaviour {

    private float puckSpeed;

    public float acceleration = 0f;

    public bool activePuck = true;

    public Transform target;

    public Transform start;

    public GameController gameController;

    private float targetOffset = 0.5f;
    
    public void Shot(Transform startTransform,Transform targetTransform,float speed)
    {
        //TODO: Based on timeToNet. Use the proper shot speed in OffenceController, then remove the multiplication.
        puckSpeed = speed * 3f;
        target = targetTransform;
        start = startTransform;
    }
    public void UpdateSpeed(float newSpeed)
    {
        puckSpeed = newSpeed;
    }

    void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, target.position) >= (targetOffset + 0.01f) && activePuck)
        {
            float speed = (puckSpeed + acceleration);
            transform.position = Vector3.MoveTowards(transform.position, target.position - new Vector3(0f, targetOffset, 0f), speed);
        }
        else
        {
            var save = target.gameObject.GetComponent<TargetTouch>()?.state == TargetStates.TargetState.Held;
            var newTarget = target.gameObject.GetComponent<TargetController>()?.reflectionTarget;
            if (newTarget != null && (save || target.gameObject.GetComponent<TargetTouch>().IsHeld()))
            {
                target = newTarget;
                puckSpeed = 0.5f;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
