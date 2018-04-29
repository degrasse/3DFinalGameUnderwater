using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrangerHeadScript : MonoBehaviour {

	public float shakeFreq;
	public float shakeAmp;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		/*
		if (Input.GetKeyDown (KeyCode.B)) {
			StartCoroutine (BobHead (3.0f));
		}
		if (Input.GetKeyDown (KeyCode.S)) {
			StartCoroutine (ShakeHead (20.0f));
		}
		*/
	}

	public void StartShakeHead(float duration){
		StartCoroutine (ShakeHead (duration));
	}

	IEnumerator BobHead(float duration){
		for (int c = 0; c < duration * 10; c++) {
			yield return new WaitForSeconds (0.1f);
		}
	}

	IEnumerator ShakeHead(float duration){
		Quaternion startRot = gameObject.transform.localRotation;
		float startTime = Time.time;
		for (int c = 0; c < duration * 20; c++) {
			float dtime = Time.time - startTime;
			gameObject.transform.localRotation = Quaternion.AngleAxis(shakeAmp * Mathf.Sin (shakeFreq * dtime), Vector3.right);
			//gameObject.transform.RotateAround (gameObject.transform.position, new Vector3 (1, 0, 0), shakeAmp * Mathf.Sin (shakeFreq * dtime));
			yield return new WaitForSeconds (0.05f);
		}
	}
}
