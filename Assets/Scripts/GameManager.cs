using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Phillip Hetzler
public class GameManager : MonoBehaviour {
	public GameObject playerLight; //light that the player uses to see
	public GameObject[] oxygenTanks; //array of collectible oxygen tanks
	public float minLightAngle, minLightIntensity; //initial oxygen in seconds, minimum light angle, minimum light intensity
	public GameObject terrain; //terrain in game

	private GameObject oxyTracker; //hold oxygen tracker
	private OxygenTrackerScript oxyTrackerScript; //hold script that controls oxygen

	private GameObject pauseMenu;
	private bool paused; //tell if game is paused or not
	private Light spotLight; //spot light attached to player
	private float oxygenLeft; //amount of oxygen left in seconds
	private int currentTank; //current tank to collect
	private TerrainScript terrScript; //script on terrain game object
	private Coroutine waveCoroutine; //coroutine that runs wave through terrain


	void Start () { //initialize variables
		waveCoroutine = null;
		spotLight = playerLight.GetComponent<Light> ();
		spotLight.type = LightType.Spot;
		currentTank = 0;
	}


	IEnumerator beginGame(){ //set up terrain for game so p[layer has sneak peek of what is coming
		yield return new WaitForSecondsRealtime (1f); //pause before terrain rise
		Time.timeScale = 0f; //stop player movement

		Coroutine raisingTerrain = StartCoroutine(terrScript.RaiseFromFlat()); //raise terrain to put the player in the environment
		while (terrScript.terrainRaising) { //wait till terrain is done raising
			yield return null; //wait for next frame
		}
		Time.timeScale = 1f; //resume game time
	}



	void Awake() { //initialize variables from other scripts and objects
		paused = false; //game not paused and pause menu not visible
		pauseMenu = GameObject.Find ("PauseMenu");
		pauseMenu.SetActive (paused);

		oxyTracker = GameObject.Find ("OxygenTracker"); //get object and component
		oxyTrackerScript = oxyTracker.GetComponent<OxygenTrackerScript> ();
		oxyTrackerScript.resetOxygen (); //set oxygen to inital oxygen

		terrScript = terrain.GetComponent<TerrainScript> (); //get terrain
		StartCoroutine (beginGame()); //start coroutine that sets the game area up properly
	}

	void Update () {
		oxygenLeft = oxyTrackerScript.oxygenLeft; //get amount of oxygen left

		if (!paused) { //if the game is not paused count down oxygen left
			//oxygenLeft -= Time.deltaTime;
			updatePlayerLight (); //update the players spot light
		}

		if (Input.GetKeyDown (KeyCode.Escape)) { //if the player hits escape change the pause menu
			TogglePauseMenu ();
		}
	}

	void updatePlayerLight () { //change the players light based on amount of oxygen left
		
		if (oxygenLeft > 45) { //light setting for  greater than 45 seconds of oxygen left
			if (waveCoroutine != null) { //if the wave routine is running
				StopCoroutine (waveCoroutine); //stop it and set to null
				waveCoroutine = null;
			}
			spotLight.range = 75; //set range of spotlight
			spotLight.spotAngle = 120; //120 degree angle for light
			spotLight.color = Color.white; //make sure light color is white
			spotLight.intensity = 3f; //intensity set to three;
		} else if (oxygenLeft > 5) { //oxygen left ranges from 5 to 45 seconds
			spotLight.spotAngle = ((120f - minLightAngle) * ((oxygenLeft - 5f) / 40f) + minLightAngle); //slowly shrink light angle to minimum light angle
			if (oxygenLeft <= 15) { //oxygen left ranges from 5 to 15 seconds


				spotLight.spotAngle += 5f * Mathf.Sin ((oxygenLeft-15f) * 2); //pulse light angle based on sin wave
				spotLight.intensity = 3f + 2f * Mathf.Sin ((oxygenLeft-15f)); //pulse intensity
				if (waveCoroutine == null) { //if wave hasen't started start it
					waveCoroutine = StartCoroutine(terrScript.Wave()); //call start coroutine and set it to waveCoroutine so we can end it later
				}
			}
		} else if (oxygenLeft > 0.00001) { //oxygen Left ranges from 0.00001 to 5 seconds
			spotLight.spotAngle = 90f; //set angle to 90
			spotLight.intensity = 3f; //set intensity very low
			//spotLight.color = new Color(UnityEngine.Random.Range(0,256),UnityEngine.Random.Range(0,256),UnityEngine.Random.Range(0,256)); //choose random color to flash
			
			



		}
		else { //oxygen left is less than 0
			if (waveCoroutine != null) { //if the wave is running end and remove it
				StopCoroutine (waveCoroutine);
				waveCoroutine = null;
			}
			spotLight.color = Color.red;//set light to red and increase intensity
			spotLight.intensity = 5f;
		} //*/
	}




	public void TogglePauseMenu() {
		if (paused)  //if game is paused unpause it
		{
			pauseMenu.SetActive(!paused);
			Time.timeScale = 1.0f; //restart game time
		}
		else //if not paused pause it
		{
			pauseMenu.SetActive(!paused);
			Time.timeScale = 0f; //Stop game time
		}
		paused = !paused; //flip pause variable
	}

	public void LoadMainMenu(){ //load main menu scene
		SceneManager.LoadScene("deGrasse");
	}

	public void RestartScene(){ //reset the current level
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

	public void GoalReached(){ //load the GameWon scene
		SceneManager.LoadScene ("GameWon");
	}

	public void HitOxygenTank(){ //called on collision with Oxygen tank
		oxyTrackerScript.resetOxygen();
	}
}
