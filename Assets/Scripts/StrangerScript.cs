using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrangerScript : MonoBehaviour {

	public Vector3 startPos;
	public Vector3 endPos;
	public float moveDuration;

	// Use this for initialization
	void Start () {
		gameObject.transform.position = startPos;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.O)) {
			GetComponentInChildren<StrangerHeadScript> ().StartShakeHead (moveDuration);
			StartCoroutine (Move ());
		}
	}

	IEnumerator Move(){
		int atanGrade = 6;
		float atanHeight = 2.55f;
		for (int i = 0; i < moveDuration * 20; i++) {
			float i2 = i * 1.0f / (moveDuration * 20);
			float t = (Mathf.Atan(atanGrade*i2 - atanGrade * 0.5f) / atanHeight) + 0.5f;
			gameObject.transform.position = startPos + (endPos - startPos) * t;
			yield return new WaitForSeconds (0.05f);
		}
	}
}
