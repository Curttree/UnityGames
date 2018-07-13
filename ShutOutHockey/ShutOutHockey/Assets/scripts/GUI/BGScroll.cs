using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroll : MonoBehaviour {
    private float horizontalSpeed = 0.15f;
    private float verticalSpeed = 0.3f;
    private float horizontalOffset;
    private float verticalOffset;

    private void Start()
    {
        Time.timeScale = 1;
    }
    // Update is called once per frame
    void FixedUpdate () {
        horizontalOffset = Time.time * horizontalSpeed;
        verticalOffset = Time.time * verticalSpeed;
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(horizontalOffset, -verticalOffset);
    }
}
