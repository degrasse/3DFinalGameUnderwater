using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	public float verticalSpeed, horizontalSpeed, rotationalSpeed,verticalLookAngle, verticalLookSpeed;
	public GameObject camera;
	private float up, down, spinL, spinR, forward, look, currentAngle;
	private Rigidbody rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale == 0) {
			return;
		}

		if (Input.GetKey (KeyCode.UpArrow)) {
			up = verticalSpeed;
		} else {
			up = 0;
		}

		if (Input.GetKey (KeyCode.DownArrow)) {
			down = verticalSpeed;
		} else {
			down = 0;
		}

		if (Input.GetKey (KeyCode.LeftArrow)) {
			spinL = rotationalSpeed;
		} else {
			spinL = 0;
		}

		if (Input.GetKey (KeyCode.RightArrow)) {
			spinR = rotationalSpeed;
		} else {
			spinR = 0;
		}
			
		if (Input.GetKey (KeyCode.Space)) {
			forward = horizontalSpeed;
		} else {
			forward = 0;
		}


		currentAngle = camera.transform.rotation.eulerAngles.x;
		if (currentAngle > 180) {
			currentAngle -= 360;
		}

		if (up != 0 && down == 0) {
			if (currentAngle > -verticalLookAngle) {
				look = (-verticalLookAngle - currentAngle) * verticalLookSpeed;
			}
		} else if (up == 0 && down != 0) {
			if (currentAngle < verticalLookAngle) {
				look = (verticalLookAngle - currentAngle) * verticalLookSpeed;
			}
		} else {
			if (currentAngle != 0) {
				look = (0 - currentAngle) * verticalLookSpeed;
			}
		}
		camera.transform.Rotate (look, 0, 0);
		look = 0;
		transform.Rotate (0, spinR - spinL, 0);
		//transform.Translate (0, up - down, forward);
		//rb.AddRelativeTorque(0,spinR-spinL,0);
		//rb.velocity = new Vector3 (0, up - down, forward);
		rb.AddRelativeForce (0, up - down, forward);

	}
}
