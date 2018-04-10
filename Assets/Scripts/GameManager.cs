using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public GameObject playerLight;
	public float initOxygen, minLightAngle, minLightIntensity;

	private Light spotLight;
	private float oxygenLeft;

	void Start () {
		oxygenLeft = initOxygen;
		spotLight = playerLight.GetComponent<Light> ();
		spotLight.type = LightType.Spot;
	}

	void Update () {
		oxygenLeft -= Time.deltaTime;
		updatePlayerLight ();
	}

	void updatePlayerLight (){
		
		///*
		if (oxygenLeft > 45) { //set initial light
			spotLight.spotAngle = 120;
			spotLight.color = Color.white;
			spotLight.intensity = 3f;
		} else if (oxygenLeft > 5) {
			spotLight.spotAngle = ((120f - minLightAngle) * ((oxygenLeft - 5f) / 40f) + minLightAngle); //slowly shrink light angle
			if (oxygenLeft <= 15) {
				spotLight.spotAngle += 5f * Mathf.Sin ((oxygenLeft-15f) * 2);//pulse light angle
				spotLight.intensity = 3f + .5f * Mathf.Sin ((oxygenLeft-15f));

			}
		} else if (oxygenLeft > 0.00001) {
			spotLight.intensity = .1f;
			spotLight.bounceIntensity = .5f;
			spotLight.color = new Color(UnityEngine.Random.Range(0,256),UnityEngine.Random.Range(0,256),UnityEngine.Random.Range(0,256));
		}
		else {
			//shrink to minLightAngle and Red lower intensity
			spotLight.spotAngle = minLightAngle;
			spotLight.color = Color.red;
			spotLight.intensity = 2f;
		}//*/
	}
}
