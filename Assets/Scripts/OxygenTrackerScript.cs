using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OxygenTrackerScript : MonoBehaviour {
	public static bool created;
	public float oxygenLeft;
	public float initOxygen;
	public float percentOxygen;
	public float timeAfterOxygen;

	void Awake()
	{
		if (!created) {
			DontDestroyOnLoad (this.gameObject);
			created = true;
			//Debug.LogError ("Awake: " + this.gameObject);
		} else {
			Destroy (this.gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
		if (SceneManager.GetActiveScene ().buildIndex == 1) {
			oxygenLeft -= Time.deltaTime;
			//Debug.Log (oxygenLeft);
			percentOxygen = oxygenLeft / initOxygen;
			if (oxygenLeft < -timeAfterOxygen) {
				//Debug.LogWarning ("dead  " + oxygenLeft);
				SceneManager.LoadScene ("DeathScene");
			}

		}




	}

	public void resetOxygen() {
		oxygenLeft = initOxygen;
		percentOxygen = 1f;
		//Debug.LogWarning ("oxygen reset " + initOxygen + "    " + oxygenLeft);
	}

	public void setInitOxygen (float seconds, bool reset){
		initOxygen = seconds;
		if (reset) {
			resetOxygen ();
		}
	}
}
