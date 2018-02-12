using UnityEngine;

public class CrowdController : MonoBehaviour {

    private float fall = 0.05f;
    private float scale = 4f;
    private float minHeight;
    private float startHeight;
    private bool goingDown;

	// Use this for initialization
	void Start () {
        startHeight = transform.position.y;
        minHeight = startHeight - fall;
	}
	
	// Update is called once per frame
	void Update () {
        if (goingDown)
            transform.Translate(0, -(Time.deltaTime/scale), 0);
        else
            transform.Translate(0, (Time.deltaTime/scale), 0);

        if (transform.position.y <= minHeight && goingDown)
            goingDown = false;
        if (transform.position.y >= startHeight && !goingDown)
            goingDown = true;
	}
}
