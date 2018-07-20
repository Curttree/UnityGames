using System;
using UnityEngine;

public class TextScroll : MonoBehaviour {
    
    private GameObject svPercentUI;
    public int cutoff = 150;
    public int startPos = 1400;

    // Use this for initialization
    void Start ()
    {
        svPercentUI = GameObject.FindGameObjectWithTag("SVPercent");
    }

    // Update is called once per frame
    private void Update()
    {
        if (svPercentUI.transform.position.x <= cutoff)
        {
            svPercentUI.transform.position = new Vector3(startPos, svPercentUI.transform.position.y);
        }
        svPercentUI.transform.Translate(new Vector3(-0.4f, 0, 0));
    }
}
