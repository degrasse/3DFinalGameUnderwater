using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundEffectClass : MonoBehaviour {

	private AudioSource source;
	private float oxygenLevels;
	public bool updatePitchToOxygen;
	public bool on;
	public bool siren;

	private GameObject oxyTracker;
	private OxygenTrackerScript oxyTrackerScript;

	//volumeControl

	//grabOxygenPercentage

	// Use this for initialization
	/*
	 * music effects will all be based on pitch
	 * 
	 * if in the upper 70% range there will be no changes to audio
	 * 
	 * 
	 * at 70% the pitch will raise; and it will quicken significantly 
	 * 
	 * at 50% the pitch will be slooowed down significantly
	 * 
	 * 
	 * at 30%:
	 *    pitch will rise and fall gradually
	 * 
	 * 20:
	 * 
	 * random pitch back and forth / frantic / chaotic
	 * 
	 * 
	 * 10%:
	 * 
	 * really slow, in right direction, and then in reverse direction; switching back and forth
	 * 
	 * 
	 * 
	 * 
	 * 
	 * 
	 */


	void Awake (){
		source = GetComponent<AudioSource>();
		oxyTracker = GameObject.Find ("OxygenTracker");
		oxyTrackerScript = oxyTracker.GetComponent<OxygenTrackerScript> ();
		oxyTrackerScript.resetOxygen (); //DELETE
	

	}

	
	// Update is called once per frame
	void Update () {

		if (updatePitchToOxygen) {
			updatePitch ();
		}


		if (siren) {

			oxygenLevels = oxyTrackerScript.percentOxygen;
			if ((oxygenLevels <= .2) && (!source.isPlaying)) {
				source.Play ();
			} else if ((oxygenLevels > .2) && (source.isPlaying)) {
				source.Stop ();
			}

		}
			
	//only update for wavy music


	}

	void updatePitch(){

		oxygenLevels = oxyTrackerScript.percentOxygen;
		if (oxygenLevels >= .7f) {
			source.pitch = 1f;
		}
		else if(oxygenLevels >= .5f){
			source.pitch = .3f;
		}
		else if(oxygenLevels >= .3f){
			source.pitch = -2.4f;
		}		
		else if(oxygenLevels >= .2f){
			source.pitch = 2f;
		}
		else if(oxygenLevels >= .1f){
			source.pitch = -.3f;
		}

	}



}

/*
 * 
 * audio ideas: first, can play around with pitch/3d/spread/volume/ different ways of distorting the thang
 * but then also, can have different songs that play based on the different audio 
 * 
 * 
 * 
 * 
 * 
 */