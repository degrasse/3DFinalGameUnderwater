using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarScript : MonoBehaviour {


	private float oxygen;
	private float updatedScale;
	private float fullScale = 5.3f;
	private float minScale = 0f;

	//need to grab oxygen percentage from oxygen object

	private GameObject oxyTracker;
	private OxygenTrackerScript oxyTrackerScript;



	void Awake (){
		oxyTracker = GameObject.Find ("OxygenTracker");
		oxyTrackerScript = oxyTracker.GetComponent<OxygenTrackerScript> ();
		oxyTrackerScript.resetOxygen (); //DELETE

	}



	// Update is called once per frame
	void Update () {
		UpdateSlider ();

	}


	//max scale = 5.3

	private float checkOxygen (){

		oxygen = oxyTrackerScript.percentOxygen;
		if (oxygen >= 1) {
			updatedScale = fullScale;
		} else if (oxygen <= 0) {
			updatedScale = minScale;
		} else {
			updatedScale = oxygen * fullScale;
		}

		return updatedScale;

	}


	void UpdateSlider(){
		updatedScale = checkOxygen();
		Vector3 scale = transform.localScale;
		scale.x = updatedScale;
		transform.localScale = scale;
	}
}
