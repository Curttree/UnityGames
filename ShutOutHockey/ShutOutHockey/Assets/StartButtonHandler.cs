using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonHandler : MonoBehaviour {

    private GameObject fade;
	// Use this for initialization
	void Start () {
        fade = GameObject.FindGameObjectWithTag("FadeIn");
        fade.GetComponent<CanvasRenderer>().SetAlpha(0f);
        fade.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClick()
    {
        StartCoroutine(FadeOut());
    }

    public IEnumerator FadeOut()
    {
        fade.SetActive(true);
        fade.GetComponent<CanvasRenderer>().SetAlpha(0f);
        float alpha = fade.GetComponent<CanvasRenderer>().GetAlpha();
        while (alpha < 1f)
        {
            fade.GetComponent<CanvasRenderer>().SetAlpha(alpha + 0.025f);
            alpha = fade.GetComponent<CanvasRenderer>().GetAlpha();
            yield return new WaitForSeconds(0.01f);
        }
        SceneManager.LoadScene("gameplay");
    }
}
