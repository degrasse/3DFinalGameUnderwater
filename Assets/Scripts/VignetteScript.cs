using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VignetteScript : MonoBehaviour {
	public SpriteRenderer sr;
	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		sr.flipX = !sr.flipX;
		sr.flipY = !sr.flipY;

		sr.color = UnityEngine.Random.ColorHSV();
	}
}
