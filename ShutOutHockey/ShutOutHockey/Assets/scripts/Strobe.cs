using UnityEngine;

public class Strobe : MonoBehaviour {
    public float maxStrobe = 0.2f;
    public float speed = 0.005f;
    private float currentStrobe = 0.0f;
    public Transform logo;
    private float startScaleLogo;
    private bool increase = true;

	// Use this for initialization
	void Start () {
        startScaleLogo = logo.localScale.x;
    }
	
	// Update is called once per frame
	void Update () {
        if (logo.localScale.x <= startScaleLogo + maxStrobe && increase)
        {
            //grow
            currentStrobe += speed;
            logo.localScale = new Vector3(currentStrobe + startScaleLogo, currentStrobe + startScaleLogo, 1);
         }
        else if (logo.localScale.x >= startScaleLogo + maxStrobe && increase)
        {
            //max size
            increase = false;
        }
        else if (logo.localScale.x >= startScaleLogo - maxStrobe && !increase)
        {
            //shrink
            currentStrobe -= speed;
            logo.localScale = new Vector3(startScaleLogo + currentStrobe, startScaleLogo + currentStrobe, 1);
       }
        else if (logo.localScale.x <= startScaleLogo - maxStrobe && !increase)
        {
            //min size
            increase = true;
        }
		
	}
}
