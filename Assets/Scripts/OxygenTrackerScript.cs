using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OxygenTrackerScript : MonoBehaviour {
	public static bool created;
	public float oxygenLeft;
	public float initOxygen;
	public float percentOxygen;

	void Awake()
	{
		if (!created)
		{
			DontDestroyOnLoad(this.gameObject);
			created = true;
			Debug.Log("Awake: " + this.gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
		oxygenLeft -= Time.deltaTime;
		percentOxygen = oxygenLeft / initOxygen;
	}

	public void resetOxygen() {
		oxygenLeft = initOxygen;
		percentOxygen = 1f;
	}

	public void setInitOxygen (float seconds, bool reset){
		initOxygen = seconds;
		if (reset) {
			resetOxygen ();
		}
	}
}
