using UnityEngine;

public class TextScroll : MonoBehaviour {

    private GameObject[] svPercentUI;
    public int cutoff = 150;
    public int startPos = 1400;

    // Use this for initialization
    void Start()
    {
        svPercentUI = GameObject.FindGameObjectsWithTag("SVPercent");
        startPos = Screen.width;
    }

    // Update is called once per frame
    private void Update()
    {
        foreach (var svPercent in svPercentUI)
        {
            if (svPercent.transform.position.x <= cutoff)
            {
                svPercent.transform.position = new Vector3(startPos, svPercent.transform.position.y);
            }
            svPercent.transform.Translate(new Vector3(-0.4f, 0, 0));
        }
    }
}
