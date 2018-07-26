using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using System;

public class BandControl : MonoBehaviour {
	public AudioMixer masterMixer;
	float lvl;

	//Spotlights and Actors
	public Light spotlightVox;
	public Light spotlightDrums;
	public Light spotlightAcoustic;
	public Light spotlightGuitar;
	public GameObject[] cameras;
	public GameObject[] extras;
	public int currentCam = 0;
	public GameObject guitar;
	public GameObject drummer;
	public GameObject singer;
	public GameObject bassist;

	//Initialize Spotlight targets
	float voxIntensityTarget = 5f;
	float drumsIntensityTarget = 5f;
	float acousticIntensityTarget = 5f;
	float guitarIntensityTarget=5f;

	// Use this for initialization
	void Start () {
		cameras= GameObject.FindGameObjectsWithTag("Camera");
		extras= GameObject.FindGameObjectsWithTag("Extra");
		Array.Sort (cameras, CompareObNames);
		SwapCamera (0);
	}
	
	// Update is called once per frame
	void Update () {
		Controls ();
		UpdateSpotlights ();
	}

	int CompareObNames( GameObject x, GameObject y ){
		return x.name.CompareTo( y.name );
	}

	void UpdateSpotlights(){
		spotlightVox.intensity=Mathf.Lerp (spotlightVox.intensity,voxIntensityTarget,Time.deltaTime*4);
		spotlightDrums.intensity=Mathf.Lerp (spotlightDrums.intensity,drumsIntensityTarget,Time.deltaTime*4);
		spotlightAcoustic.intensity=Mathf.Lerp (spotlightAcoustic.intensity,acousticIntensityTarget,Time.deltaTime*4);
		spotlightGuitar.intensity=Mathf.Lerp (spotlightGuitar.intensity,guitarIntensityTarget,Time.deltaTime*4);
	}

	void SwapCamera(int i){
		Camera activecam;
		for(int x=0;x<cameras.Length;x++){
			activecam=cameras[x].GetComponent<Camera>();
			if(x==i)
				activecam.enabled=true;
			else
				activecam.enabled=false;
		}
		LookAtCamera (guitar, cameras [i]);
		LookAtCamera (drummer, cameras [i]);
		LookAtCamera (singer, cameras [i]);
		LookAtCamera (bassist, cameras [i]);
		for (int y=0; y<extras.Length; y++) {
			LookAtCamera (extras[y], cameras [i]);
		}
	}

	void LookAtCamera(GameObject band, GameObject camera){
		band.transform.LookAt (camera.transform);
	}

	void Controls(){
		if (Input.GetButtonDown ("A")) {
			masterMixer.GetFloat("VoxVol",out lvl);
			if (lvl==-80f){
				masterMixer.ClearFloat("VoxVol");
				voxIntensityTarget=5f;
			}
			else{
				masterMixer.SetFloat ("VoxVol",-80f);
				voxIntensityTarget=0f;
			}
		}
		if (Input.GetButtonDown ("Y")) {
			masterMixer.GetFloat("DrumsVol",out lvl);
			if (lvl==-80f){
				masterMixer.ClearFloat("DrumsVol");
				drumsIntensityTarget=5f;
			}
			else{
				masterMixer.SetFloat ("DrumsVol",-80f);
				drumsIntensityTarget=0f;
			}
		}
		if (Input.GetButtonDown ("B")) {
			masterMixer.GetFloat("GuitarVol",out lvl);
			if (lvl==-80f){
				masterMixer.ClearFloat("GuitarVol");
				guitarIntensityTarget=5f;
			}
			else{
				masterMixer.SetFloat ("GuitarVol",-80f);
				guitarIntensityTarget=0f;
			}
		}
		if (Input.GetButtonDown ("X")) {
			masterMixer.GetFloat("AcousticVol",out lvl);
			if (lvl==-80f){
				masterMixer.ClearFloat("AcousticVol");
				acousticIntensityTarget=5f;
			}
			else{
				masterMixer.SetFloat ("AcousticVol",-80f);
				acousticIntensityTarget=0f;
			}
		}
		if (Input.GetButtonDown ("R")){
			if (currentCam<cameras.Length-1)
				currentCam+=1;
			else
				currentCam=0;
			SwapCamera (currentCam);
		}
		if (Input.GetButtonDown ("L")){
			if (currentCam>0)
				currentCam-=1;
			else
				currentCam=cameras.Length-1;
			SwapCamera (currentCam);
		}
	}
}
	