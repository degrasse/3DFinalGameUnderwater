using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public GameObject vignette;
	public int initOxygen;
	public float minVignetteXScale, minVignetteYScale;

	private float oxygenLeft, initVignetteYScale, initVignetteXScale;
	private SpriteRenderer srVignette;
	private int currentVignette, vignetteFrameCount;
	void Start () {
		oxygenLeft = initOxygen;
		srVignette = vignette.GetComponent<SpriteRenderer> ();
		currentVignette = 0;
		vignetteFrameCount = 0;
		initVignetteXScale = vignette.transform.localScale.x;
		initVignetteYScale = vignette.transform.localScale.y;
	}

	void Update () {
		oxygenLeft -= 1;
		updateVignette ();
	}


	void updateMaterial(){
		
	}


	void updateVignette (){
		srVignette.flipX = !srVignette.flipX;
		float oxyRatio = oxygenLeft / initOxygen;

		if (oxyRatio > .75) {
			if (currentVignette != 0) {
				vignetteFrameCount = 0;
				currentVignette = 0;
				srVignette.color = Color.black;
				vignette.transform.localScale = new Vector3 (initVignetteXScale, initVignetteYScale, 1.0f);
			}

			vignetteFrameCount++;
		}
		else if (oxyRatio > .5) {
			if (currentVignette != 1) {
				vignetteFrameCount = 0;
				currentVignette = 1;
			}

			vignette.transform.localScale -= new Vector3(0.007f,0.007f,0);

			/*
			if (vignetteFrameCount == 20) {
				vignetteFrameCount = 0;
				srVignette.color = Random.ColorHSV ();
			}
			//*/
			vignetteFrameCount++;
			Debug.Log ("50%");
		}
		else if (oxyRatio > .25) {
			if (currentVignette != 2) {
				vignetteFrameCount = 0;
				currentVignette = 2;
			}

			vignette.transform.localScale -= new Vector3(0.02f,0.02f,0);

			if (vignetteFrameCount == 10) {
				vignetteFrameCount = 0;
				srVignette.color = Random.ColorHSV ();
			}
			Debug.Log ("25%");
		} else if (oxyRatio > 0.01) {
			if (currentVignette != 3) {
				vignetteFrameCount = 0;
				currentVignette = 3;
			}

			vignette.transform.localScale -= new Vector3(0.05f,0.05f,0);

			if (vignetteFrameCount == 5) {
				vignetteFrameCount = 0;
				srVignette.color = Random.ColorHSV ();
			}
			vignetteFrameCount++;
			Debug.Log ("0.0001%"); 
		}
		else {
			if (currentVignette != 4) {
				vignetteFrameCount = 0;
				currentVignette = 4;
			}

			vignette.transform.localScale -= new Vector3(0.1f,0.1f,0);

			srVignette.color = Random.ColorHSV ();
			
			vignetteFrameCount++;
		}

		if (vignette.transform.localScale.x < minVignetteXScale) {
			vignette.transform.localScale = new Vector3(minVignetteXScale,vignette.transform.localScale.y,1.0f);
		}
		if (vignette.transform.localScale.y < minVignetteYScale) {
			vignette.transform.localScale = new Vector3(vignette.transform.localScale.x,minVignetteYScale,1.0f);
		}
	}
}
