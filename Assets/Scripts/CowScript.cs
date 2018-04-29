using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowScript : MonoBehaviour {

	public Transform playerTransform;

	public float bounceFreq;
	public float bounceHeight;

	private float currBounceHeight;
	private float alphaDelta = 0.001f;
	private float origy;
	private bool bouncing = false;

	// Use this for initialization
	void Start () {
		origy = gameObject.transform.position.y;
	}

	// Update is called once per frame
	void Update () {
		float dist = Mathf.Abs (((Vector3)(new Vector3(gameObject.transform.position.x, origy, gameObject.transform.position.z) - playerTransform.position)).magnitude);
		if (dist < 60f) {
			if (!bouncing) {
				bouncing = true;
				StartCoroutine (Bounce ());
			}
			currBounceHeight = bounceHeight * 1.0f / (dist / 60.0f);
			//currBounceFreq = bounceFreq * 1.0f / (dist / 60.0f);
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
				origy + currBounceHeight * Mathf.Abs(Mathf.Sin(Mathf.Sqrt(freqmult * (dtime - offsetx)))),
				gameObject.transform.position.z);
			yield return new WaitForSeconds (0.01f);
		}
	}
}