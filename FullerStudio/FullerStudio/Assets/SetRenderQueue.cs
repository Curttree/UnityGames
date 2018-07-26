using UnityEngine;
using System.Collections;

public class SetRenderQueue : MonoBehaviour {
	public int renderQueue=2000;
	// Use this for initialization
	void Start () {
		Renderer renderer = GetComponent<Renderer>();
		if (!renderer || !renderer.sharedMaterial)
			return;
		renderer.sharedMaterial.renderQueue = renderQueue;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
