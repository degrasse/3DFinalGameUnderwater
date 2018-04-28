using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	public GameObject playerLight; //light that the player uses to see
	public GameObject[] oxygenTanks; //array of collectible oxygen tanks
	public float initOxygen, minLightAngle, minLightIntensity; //initial oxygen in seconds, minimum light angle, minimum light intensity
	public GameObject terrain; //terrain in game

	private GameObject pauseMenu;
	private bool paused; //tell if game is paused or not
	private Light spotLight; //spot light attached to player
	private float oxygenLeft; //amount of oxygen left in seconds
	private int currentTank; //current tank to collect
	private TerrainScript terrScript; //script on terrain game object
	private Coroutine waveCoroutine; //coroutine that runs wave through terrain


	void Start () { //initialize variables
		waveCoroutine = null;
		oxygenLeft = initOxygen;
		spotLight = playerLight.GetComponent<Light> ();
		spotLight.type = LightType.Spot;
		currentTank = 0;

	}


	IEnumerator beginGame(){
		yield return new WaitForSecondsRealtime (3f); //wait so player has time to look around and see goals
		Time.timeScale = 0f; //stop player movement
		terrScript.InitialRaise(); //raise terrain to put the player in the environment
		yield return new WaitForSecondsRealtime (10f);

		//<------------------------------------------------------------------------------------------------------------CHANGE TO WAIT TIL RAISE IS DONE THEN RESUME
		//<---------------------------ADD DISABLE OXYGEN TANKS

		Time.timeScale = 1f; //resume time
	}



	void Awake() { //initialize variables from other scripts and objects
		paused = false;
		pauseMenu = GameObject.Find ("PauseMenu");
		pauseMenu.SetActive (paused);
		terrScript = terrain.GetComponent<TerrainScript> ();
		StartCoroutine (beginGame()); //start coroutine that sets the game area up properly
	}

	void Update () {
		if (!paused) { //if the game is not paused count down oxygen left
			oxygenLeft -= Time.deltaTime;
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
			spotLight.spotAngle = 120; //120 degree angle for light
			spotLight.color = Color.white; //make sure light color is white
			spotLight.intensity = 3f; //intensity set to three;
		} else if (oxygenLeft > 5) { //oxygen left ranges from 5 to 45 seconds
			spotLight.spotAngle = ((120f - minLightAngle) * ((oxygenLeft - 5f) / 40f) + minLightAngle); //slowly shrink light angle to minimum light angle
			if (oxygenLeft <= 15) { //oxygen left ranges from 5 to 15 seconds
				spotLight.spotAngle += 5f * Mathf.Sin ((oxygenLeft-15f) * 2); //pulse light angle based on sin wave
				spotLight.intensity = 3f + 2f * Mathf.Sin ((oxygenLeft-15f)); //pulse intensity
				if (waveCoroutine == null) { //if 
					waveCoroutine = StartCoroutine(terrScript.Wave());
				}
			}
		} else if (oxygenLeft > 0.00001) {
			spotLight.spotAngle = 90f;
			spotLight.intensity = .005f;
			spotLight.color = new Color(UnityEngine.Random.Range(0,256),UnityEngine.Random.Range(0,256),UnityEngine.Random.Range(0,256));
		}
		else {
			if (waveCoroutine != null) {
				StopCoroutine (waveCoroutine);
				waveCoroutine = null;
			}
			//spotLight.spotAngle = minLightAngle;
			spotLight.color = Color.red;
			spotLight.intensity = 5f;

		}//*/
	}


	public void TogglePauseMenu() {
		if (paused)
		{
			pauseMenu.SetActive(!paused);
			Time.timeScale = 1.0f;
		}
		else
		{
			pauseMenu.SetActive(!paused);
			Time.timeScale = 0f;
		}
		paused = !paused;
	}

	public void LoadMainMenu(){
		SceneManager.LoadScene("MainMenu");
	}

	public void RestartScene(){
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

	public void ResetOxygen(){
		//Debug.Log ("Success");
		oxygenLeft = initOxygen;
	}

	public void HitOxygenTank(){
		ResetOxygen ();
		if (currentTank + 1 < oxygenTanks.Length) {
			oxygenTanks [currentTank + 1].SetActive (true);
			currentTank++;
		}
	}
}
