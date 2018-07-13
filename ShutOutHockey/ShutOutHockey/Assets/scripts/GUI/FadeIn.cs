using UnityEngine;

public class FadeIn : MonoBehaviour {

    private float alpha = 1f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        alpha = this.GetComponent<CanvasRenderer>().GetAlpha();
        if (alpha <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            this.GetComponent<CanvasRenderer>().SetAlpha(alpha - 0.01f);
        }
	}
}
