using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowScript : MonoBehaviour {

	public float bounceFreq;
	public float bounceHeight;

	private float alphaDelta = 0.001f;
	private float origy;

	// Use this for initialization
	void Start () {
		origy = gameObject.transform.position.y;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.B)) {
			StartCoroutine (Bounce ());
		}
	}

	IEnumerator Bounce(){
		float offsetx = -0.493f;
		float modval = 1.4809f;
		float freqmult = 20f;
		float startTime = Time.time;
		while (true) {
			float dtime = modval - ((bounceFreq * (Time.time - startTime / 100f)) % modval);
			gameObject.transform.position = new Vector3(gameObject.transform.position.x, 
				origy + bounceHeight * Mathf.Abs(Mathf.Sin(Mathf.Sqrt(freqmult * (dtime - offsetx)))),
				gameObject.transform.position.z);
			yield return new WaitForSeconds (0.01f);
		}
	}
}