using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarScript : MonoBehaviour {

	private RectTransform rect;
	private float oxygen;
	private float updatedScale;
	private float maxXAnchor;
	private float minXAnchor;
	//private float fullScale = 5.3f;
	//private float minScale = 0f;

	//need to grab oxygen percentage from oxygen object

	private GameObject oxyTracker;
	private OxygenTrackerScript oxyTrackerScript;



	void Awake (){
		
		oxyTracker = GameObject.Find ("OxygenTracker");
		oxyTrackerScript = oxyTracker.GetComponent<OxygenTrackerScript> ();
		rect = this.gameObject.GetComponent<RectTransform> ();
		maxXAnchor = rect.anchorMax.x;
		minXAnchor = rect.anchorMin.x;


	}



	// Update is called once per frame
	void Update () {
		UpdateSlider ();

	}


	//max scale = 5.3

	private float checkOxygen (){

		oxygen = oxyTrackerScript.percentOxygen;
		if (oxygen >= 1) {
			updatedScale = maxXAnchor;
		} else if (oxygen <= 0) {
			updatedScale = minXAnchor;
		} else {
			updatedScale = oxygen * (maxXAnchor - minXAnchor) + minXAnchor;
		}

		return updatedScale;

	}


	void UpdateSlider(){
		updatedScale = checkOxygen();
		//Vector3 scale = transform.localScale;
		//scale.x = updatedScale;
		Vector2 temp = new Vector2 (updatedScale, rect.anchorMax.y);
		Debug.Log (updatedScale);
		rect.anchorMax = temp;
		//transform.localScale = scale;
	}
}
