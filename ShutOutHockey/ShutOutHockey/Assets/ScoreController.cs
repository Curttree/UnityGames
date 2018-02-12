using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {
    public int SV = 0;
    public int SA = 0;
    public int goals = 0;
    private float svPecent = 0.000f;
    private GameObject svPercentUI;
    private GameObject scoreUI;
    // Use this for initialization
    void Start () {
        svPercentUI = GameObject.FindGameObjectWithTag("SVPercent");
        scoreUI = GameObject.FindGameObjectWithTag("Score");
    }
	
	// Update is called once per frame
	void Update () {
        if (SA > 0)
            svPecent =(float) SV / SA;
        //Debug.Log($"SV: {SV}, SA: {SA}, SV%:{svPecent}");
        svPercentUI.GetComponent<Text>().text = $"Save% = {svPecent.ToString("0.000")}    SV = {SV.ToString()}     SA = {SA.ToString()}";
        scoreUI.GetComponent<Text>().text = goals.ToString();
    }
}
