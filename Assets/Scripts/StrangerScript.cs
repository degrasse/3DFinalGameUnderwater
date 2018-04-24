using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrangerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.B)) {
			StartCoroutine (BobHead (3.0f));
		}
		if (Input.GetKeyDown (KeyCode.S)) {
			StartCoroutine (ShakeHead (3.0f));
		}
	}

	IEnumerator BobHead(float duration){
		for (int c = 0; c < duration * 10; c++) {
			yield return new WaitForSeconds (0.1f);
		}
	}

	IEnumerator ShakeHead(float duration){
		for (int c = 0; c < duration * 10; c++) {
			yield return new WaitForSeconds (0.1f);
		}
	}
}
