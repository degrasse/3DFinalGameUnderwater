using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	public GameObject player;
	Coroutine shakeCoroutine;

	private Vector3 offset;
	private bool shaking = false;

	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (!shaking) {
				StartShake (3);
			}
		}
	}

	void StartShake(float seconds){
		StartCoroutine (ShakeForSeconds (seconds));
	}

	IEnumerator ShakeForSeconds(float seconds){
		shaking = true;
		shakeCoroutine = StartCoroutine (Shake ());
		yield return new WaitForSeconds (seconds);
		StopCoroutine (shakeCoroutine);
		shaking = false;
	}

	IEnumerator Shake(){
		while (true) {
			Vector3 campos = player.transform.position + offset;
			Vector3 newpos = campos + Random.insideUnitSphere * 0.3f;
			transform.position = newpos;
			yield return new WaitForSeconds(0.01f);
		}
	}
}
