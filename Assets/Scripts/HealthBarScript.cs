using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarScript : MonoBehaviour {

	private RectTransform rect;
	private float oxygen;
	private float updatedScale;
	private float maxYAnchor;
	private float minYAnchor;
	//private float fullScale = 5.3f;
	//private float minScale = 0f;

	//need to grab oxygen percentage from oxygen object

	private GameObject oxyTracker;
	private OxygenTrackerScript oxyTrackerScript;



	void Awake (){
		
		oxyTracker = GameObject.Find ("OxygenTracker");
		oxyTrackerScript = oxyTracker.GetComponent<OxygenTrackerScript> ();
		rect = this.gameObject.GetComponent<RectTransform> ();
		maxYAnchor = rect.anchorMax.y;
		minYAnchor = rect.anchorMin.y;


	}



	// Update is called once per frame
	void Update () {
		UpdateSlider ();

	}


	//max scale = 5.3

	private float checkOxygen (){

		oxygen = oxyTrackerScript.percentOxygen;
		if (oxygen >= 1) {
			updatedScale = maxYAnchor;
		} else if (oxygen <= 0) {
			updatedScale = minYAnchor;
		} else {
			updatedScale = oxygen * (maxYAnchor - minYAnchor) + minYAnchor;
		}

		return updatedScale;

	}


	void UpdateSlider(){
		updatedScale = checkOxygen();
		//Vector3 scale = transform.localScale;
		//scale.x = updatedScale;
		Vector2 temp = new Vector2 (rect.anchorMax.x, updatedScale);
		rect.anchorMax = temp;
		//transform.localScale = scale;
	}
}
