using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowScript : MonoBehaviour {

	private float alphaDelta = 0.001f;

	public Material cowMat;
	// Use this for initialization
	void Start () {
		Color origColor = cowMat.color;
		cowMat.color = new Color (cowMat.color.r, cowMat.color.g, cowMat.color.b, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
		if (cowMat.color.a < 0)
			alphaDelta = 0.001f;
		if (cowMat.color.a > 1f)
			alphaDelta = -0.1f;
		
		cowMat.color = new Color (cowMat.color.r, cowMat.color.g, cowMat.color.b, cowMat.color.a + alphaDelta);
	}
}
