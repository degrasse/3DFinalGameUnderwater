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
			GetComponentInChildren<StrangerHeadScript> ().StartShakeHead ();
			StartCoroutine (Move ());
		}
	}

	IEnumerator Move(){
		for (int i = 0; i < moveDuration * 20; i++) {
			float i2 = i * 1.0f / (moveDuration * 20);
			gameObject.transform.position = startPos + (endPos - startPos) * i2;
			yield return new WaitForSeconds (0.05f);
		}
	}
}
