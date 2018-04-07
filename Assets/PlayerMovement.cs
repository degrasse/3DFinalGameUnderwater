using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	public float verticalSpeed, horizontalSpeed, rotationalSpeed;
	private float up, down, spinL, spinR, forward;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.UpArrow)) {
			up = Time.deltaTime * verticalSpeed;
		} else {
			up = 0;
		}

		if (Input.GetKey (KeyCode.DownArrow)) {
			down = Time.deltaTime * verticalSpeed;
		} else {
			down = 0;
		}

		if (Input.GetKey (KeyCode.LeftArrow)) {
			spinL = Time.deltaTime * rotationalSpeed;
		} else {
			spinL = 0;
		}

		if (Input.GetKey (KeyCode.RightArrow)) {
			spinR = Time.deltaTime * rotationalSpeed;
		} else {
			spinR = 0;
		}
			
		if (Input.GetKey (KeyCode.Space)) {
			forward = Time.deltaTime * horizontalSpeed;
		} else {
			forward = 0;
		}

		transform.Rotate (0, spinR - spinL, 0);
		transform.Translate (0, up - down, forward);
	}
}
