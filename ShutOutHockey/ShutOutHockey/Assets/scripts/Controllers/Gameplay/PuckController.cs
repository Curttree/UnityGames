using System.Collections;
using UnityEngine;

public class PuckController : MonoBehaviour {

    private float puckSpeed;

    public float acceleration = 0f;

    public GameObject targetObject;

    public bool activePuck = true;

	public IEnumerator Shot(Transform start,Transform targetTransform,float speed)
    {
        puckSpeed = speed;
        //float delay = Vector2.Distance(start.position, target.position) / puckSpeed/(1/Time.deltaTime*10f);
        while (Vector2.Distance(transform.position, targetTransform.position) >= 0.01f && activePuck)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, puckSpeed+acceleration);
            yield return new WaitForSeconds(0.02f);
        }
        Destroy(gameObject);
    }
    public void UpdateSpeed(float newSpeed)
    {
        puckSpeed = newSpeed;
    }
}
