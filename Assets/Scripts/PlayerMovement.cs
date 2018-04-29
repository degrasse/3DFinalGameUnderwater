using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Phillip Hetzler
public class PlayerMovement : MonoBehaviour {
	public float verticalSpeed, horizontalSpeed, rotationalSpeed,verticalLookAngle, verticalLookSpeed; //movement speeds for all directions
	public GameObject playerCamera; //camera

	private GameManager gM;
	private float up, down, spinL, spinR, forward, look, currentAngle; //used in movment
	private Rigidbody rb;//rigidbody to add force to

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}

	void Awake() {
		gM = GameObject.Find ("GameManager").GetComponent<GameManager>(); //get game manager and display error if not found
		if (gM == null) {
			Debug.LogError ("Add Game Manager to scene");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale == 0) { //if paused skip all below
			return;
		}

		if (Input.GetKey (KeyCode.UpArrow)) { //player move up
			up = verticalSpeed;
		}

		if (Input.GetKey (KeyCode.DownArrow)) { //player move down
			down = verticalSpeed;
		}

		if (Input.GetKey (KeyCode.LeftArrow)) { //player rotate left
			spinL = rotationalSpeed;
		}

		if (Input.GetKey (KeyCode.RightArrow)) { //player rotate right
			spinR = rotationalSpeed;
		}
			
		if (Input.GetKey (KeyCode.Space)) { //player move forward
			forward = horizontalSpeed;
		}

		//the following code slowly moves the camera to the desired angle while moving vertically

		currentAngle = playerCamera.transform.rotation.eulerAngles.x; //get current angle

		if (currentAngle > 180) { //set range to -180 to 180
			currentAngle -= 360;
		}

		if (up != 0 && down == 0) { //if moving up
			if (currentAngle > -verticalLookAngle) { //if not at desired angle
				look = (-verticalLookAngle - currentAngle) * verticalLookSpeed; //calculate desired angle to rotate and multiply to slow down
				if (look > -.005) { //if close, snap to desired angle
					look = (-verticalLookAngle - currentAngle);
				}
			}
		} else if (up == 0 && down != 0) { //if moving down
			if (currentAngle < verticalLookAngle) { //if not at desired angle
				look = (verticalLookAngle - currentAngle) * verticalLookSpeed; //calculate desired angle to rotate and multiply to slow down
				if (look < .005) { //if close, snap to desired angle
					look = (verticalLookAngle - currentAngle);
				}
			}
		} else { //if not moving up or down
			if (currentAngle != 0) { //if not at 0
				look = (0 - currentAngle) * verticalLookSpeed; //calculate desired angle to rotate and multiply to slow down
				if (look < .005 && look > -.005) { //if close, snap to desired angle
					look = (0 - currentAngle);
				}
			}
		}
		playerCamera.transform.Rotate (look, 0, 0); //rotate camera desired angle

		transform.Rotate (0, spinR - spinL, 0); //rotate player horizontally
		rb.AddRelativeForce (0, up - down, forward); //add force to player vertically and forward
		//subtraction allows cancelation if user is hitting both buttons

		look = 0; //reset movement variables
		spinL = 0;
		spinR = 0;
		up = 0;
		down = 0;
		forward = 0;
	}


	void OnTriggerEnter(Collider coll){ //if player hits trigger collider
		if (coll.gameObject.tag.Equals ("Oxygen")) { //call function and destroy oxygen tank
			gM.HitOxygenTank ();
			Destroy(coll.gameObject);
		} else if(coll.gameObject.tag.Equals("Goal")) { //if goal hit, call function
			gM.GoalReached();
		}
		if (coll.gameObject.CompareTag ("Pick Up"))
        {
        	//add code for infintie oxygen
            Destroy(coll.gameObject);
        }

	}
	
}
