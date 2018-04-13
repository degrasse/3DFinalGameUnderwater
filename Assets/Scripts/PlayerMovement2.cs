using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour {

	public float mouseLookSpeed;

	private Camera camera;

	// Use this for initialization
	void Start () {
		camera = GetComponentInChildren<Camera> ();
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {
		float mousex = mouseLookSpeed * Input.GetAxis ("Mouse X");
		float mousey = -1f * mouseLookSpeed * Input.GetAxis ("Mouse Y");
		if (camera.transform.rotation.x > 0.3f) {
			if (mousey < 0) {
				camera.transform.Rotate (new Vector3 (mousey, 0f, 0f));
			}
		} else if (camera.transform.rotation.x < -0.3f) {
			if (mousey > 0) {
				camera.transform.Rotate (new Vector3 (mousey, 0f, 0f));
			}
		} else {
			camera.transform.Rotate (new Vector3 (mousey, 0f, 0f));
		}
		camera.transform.RotateAround (camera.transform.position, new Vector3 (0f, 1f, 0f), mousex);
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Cursor.lockState = CursorLockMode.None;
		}
	}
}
