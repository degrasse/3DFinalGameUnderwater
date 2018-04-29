using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncouragementScript : MonoBehaviour {

	public Transform playerTransform;

	private TextMesh tm;

	string encouragement1 = "youre doing great kiddo\nyour mother and i are so\nproud";

	// Use this for initialization
	void Start () {
		tm = GetComponentInChildren<TextMesh> ();
		tm.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		if (Mathf.Abs (((Vector3)(playerTransform.position - gameObject.transform.position)).magnitude) < 20f) {
			tm.text = encouragement1;
		} else {
			tm.text = "";
		}
	}
}
