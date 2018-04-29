using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	public GameObject playerLight; //light that the player uses to see
	public GameObject[] oxygenTanks; //array of collectible oxygen tanks
	public float minLightAngle, minLightIntensity; //initial oxygen in seconds, minimum light angle, minimum light intensity
	public GameObject terrain; //terrain in game

	private GameObject oxyTracker;
	private OxygenTrackerScript oxyTrackerScript;

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


	IEnumerator beginGame(){
		yield return new WaitForSecondsRealtime (3f); //wait so player has time to look around and see goals
		Time.timeScale = 0f; //stop player movement

		Coroutine raisingTerrain = StartCoroutine(terrScript.RaiseFromFlat()); //raise terrain to put the player in the environment
		while (terrScript.terrainRaising) {
			yield return null;
		}
		//<------------------------------------------------------------------------------------------------------------CHANGE TO WAIT TIL RAISE IS DONE THEN RESUME

		//<---------------------------ADD DISABLE OXYGEN TANKS

		for (int i = 1; i < oxygenTanks.Length; i++) { //set all tanks but first to 
			oxygenTanks [i].SetActive (false);
		}

		Time.timeScale = 1f; //resume time
	}



	void Awake() { //initialize variables from other scripts and objects
		paused = false;
		pauseMenu = GameObject.Find ("PauseMenu");
		pauseMenu.SetActive (paused);

		oxyTracker = GameObject.Find ("OxygenTracker");
		oxyTrackerScript = oxyTracker.GetComponent<OxygenTrackerScript> ();
		oxyTrackerScript.resetOxygen ();
		//Debug.Log ("OxygenReset");
		terrScript = terrain.GetComponent<TerrainScript> ();
		StartCoroutine (beginGame()); //start coroutine that sets the game area up properly
	}

	void Update () {
		oxygenLeft = oxyTrackerScript.oxygenLeft;
		if (!paused) { //if the game is not paused count down oxygen left
			//oxygenLeft -= Time.deltaTime;
			updatePlayerLight (); //update the players spot light
		}
		if (Input.GetKeyDown (KeyCode.Escape)) { //if the player hits escape change the pause menu
			TogglePauseMenu ();
		}
	}

	void updatePlayerLight (){
		
		if (oxygenLeft > 45) { //light setting for  greater than 45 seconds of oxygen left
			if (waveCoroutine != null) { //if the wave routine is running
				StopCoroutine (waveCoroutine); //stop it and set to null
				waveCoroutine = null;
			}
			spotLight.range = 75;
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
			spotLight.intensity = .005f; //set intensity very low
			spotLight.color = new Color(UnityEngine.Random.Range(0,256),UnityEngine.Random.Range(0,256),UnityEngine.Random.Range(0,256)); //choose random color to flash
		}
		else { //oxygen left is less than 0
			if (waveCoroutine != null) { //if the wave is running end and remove it
				StopCoroutine (waveCoroutine);
				waveCoroutine = null;
			}
			spotLight.color = Color.red;//set light to red and increase intensity
			spotLight.intensity = 5f;

		}//*/
	}


	public void TogglePauseMenu() {
		if (paused)  //if game is paused unpause it
		{
			pauseMenu.SetActive(!paused);
			Time.timeScale = 1.0f;
		}
		else //if not paused pause it
		{
			pauseMenu.SetActive(!paused);
			Time.timeScale = 0f;
		}
		paused = !paused; //flip pause variable
	}

	public void LoadMainMenu(){ //load main menu scene
		SceneManager.LoadScene("deGrasse");
	}

	public void RestartScene(){ //reset the current level
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

	public void GoalReached(){
		SceneManager.LoadScene ("GameWon");
	}

	public void HitOxygenTank(){ //called on collision with Oxygen tank
		oxyTrackerScript.resetOxygen();
		if (currentTank + 1 < oxygenTanks.Length) { //activate next Oxygen tank if it exists
			oxygenTanks [currentTank + 1].SetActive (true);
			currentTank++;
		}
	}
}
