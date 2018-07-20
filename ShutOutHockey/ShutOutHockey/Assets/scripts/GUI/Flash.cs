using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour {
    private Color c;
    // Use this for initialization
    void Start () {
        c = Color.blue;
    }
	
	// Update is called once per frame
	void Update () {
        c.a = c.a - 0.01f;
        foreach (Transform child in transform)
        {
            child.gameObject.GetComponent<SpriteRenderer>().material.color=c;
        }
		
	}
}
