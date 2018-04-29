using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Phillip Hetzler
public class OxygenTrackerScript : MonoBehaviour {
	public static bool created; //track if created
	public float oxygenLeft; //seconds of oxygen left
	public float initOxygen; //seconds of initial oxygen
	public float percentOxygen; //percent of initOxygen left
	public float timeAfterOxygen; //time after oxygen hits 0 before death

	void Awake()
	{
		if (!created) { //if it doesn't exist
			DontDestroyOnLoad (this.gameObject); //set to never be destroyed
			created = true;
			//Debug.LogError ("Awake: " + this.gameObject);
		} else { //if it already exists destroy this one
			Destroy (this.gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
		if (SceneManager.GetActiveScene ().buildIndex == 1) { //if in game scene
			oxygenLeft -= Time.deltaTime; //subtract time
			//Debug.Log (oxygenLeft);
			percentOxygen = oxygenLeft / initOxygen; //calculate percentage
			if (oxygenLeft < -timeAfterOxygen) { //if after oxygen time has run out load death scene
				//Debug.LogWarning ("dead  " + oxygenLeft);
				SceneManager.LoadScene ("DeathScene");
			}
		}
	}

	public void resetOxygen() {
		oxygenLeft = initOxygen; //reset the oxygen left and percentage
		percentOxygen = 1f;
		//Debug.LogWarning ("oxygen reset " + initOxygen + "    " + oxygenLeft);
	}

	public void setInitOxygen (float seconds, bool reset){ //change initial oxygen and decide wether to reset
		initOxygen = seconds;
		if (reset) {
			resetOxygen ();
		}
	}
}