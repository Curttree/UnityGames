using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckController : MonoBehaviour {

    private float puckSpeed;

	public IEnumerator Shot(Transform start,Transform target,float speed)
    {
        puckSpeed = speed;
        //float delay = Vector2.Distance(start.position, target.position) / puckSpeed/(1/Time.deltaTime*10f);
        //Debug.Log(delay);
        while (Vector2.Distance(transform.position, target.position) >= 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, puckSpeed);
            Debug.Log(puckSpeed.ToString());
            yield return new WaitForSeconds(0.02f);
        }
        Destroy(gameObject);
    }
    public void UpdateSpeed(float newSpeed)
    {
        puckSpeed = newSpeed;
    }
}
