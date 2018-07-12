﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckController : MonoBehaviour {

    private float puckSpeed;

    public float acceleration = 0f;

    public GameObject targetObject;

	public IEnumerator Shot(Transform start,Transform targetTransform,float speed)
    {
        puckSpeed = speed;
        //float delay = Vector2.Distance(start.position, target.position) / puckSpeed/(1/Time.deltaTime*10f);
        //Debug.Log(delay);
        while (Vector2.Distance(transform.position, targetTransform.position) >= 0.01f)
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
