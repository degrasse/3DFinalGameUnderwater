using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncouragementScript : MonoBehaviour {

	public Transform playerTransform;

	private TextMesh tm;

	string encouragement1 = "youre doing great kiddo\nyour mother and i are so\nproud";
	string encouragement2 = "youre trying your best\nand thats all that matters\nxoxo mum";
	string encouragement3 = "whos got two thumbs and\nbelieves in you thats\nright this guy right here";
	string[] encs;

	// Use this for initialization
	void Start () {
		encs = new string[]{ encouragement1, encouragement2, encouragement3 };
		tm = GetComponentInChildren<TextMesh> ();
		tm.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		if (Mathf.Abs (((Vector3)(playerTransform.position - gameObject.transform.position)).magnitude) < 60f) {
			tm.text = encs[(int)((Time.time / 5) % 3)];
		} else {
			tm.text = "";
		}
	}
}
