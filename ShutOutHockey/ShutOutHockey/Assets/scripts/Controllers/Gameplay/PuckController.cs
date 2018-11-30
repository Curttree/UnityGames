using System.Collections;
using UnityEngine;

public class PuckController : MonoBehaviour {

    private float puckSpeed;

    public float acceleration = 0f;

    public bool activePuck = true;

    public Transform target;

    public Transform start;

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
            //print($"hit the net with speed {puckSpeed+acceleration}");
            Destroy(gameObject);
        }
    }
}
