using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Phillip Hetzler
public class OxygenScript : MonoBehaviour {
	public GameObject AreaLight; //light in oxygen tank
	private Light oxyLight;
	// Use this for initialization
	void Start () {
		oxyLight = AreaLight.GetComponent<Light> ();
	}
	
	// Update is called once per frame
	void Update () {
		oxyLight.intensity = 2 * Mathf.Sin (Time.time * 2) + 3; //pulse light intensity
	}
}
