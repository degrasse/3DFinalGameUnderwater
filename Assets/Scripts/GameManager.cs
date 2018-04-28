using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	public GameObject playerLight;
	public GameObject[] oxygenTanks;
	public float initOxygen, minLightAngle, minLightIntensity;
	public GameObject terrain; 

	private GameObject pauseMenu;
	private bool paused;
	private Light spotLight;
	private float oxygenLeft;
	private int currentTank;
	private TerrainScript terrScript;
	private Coroutine waveCoroutine;


	void Start () {
		waveCoroutine = null;
		oxygenLeft = initOxygen;
		spotLight = playerLight.GetComponent<Light> ();
		spotLight.type = LightType.Spot;
		currentTank = 0;
		if (oxygenTanks.Length == 0) {
			Debug.Log ("No Oxygen Tanks");
		} else {
			/*
			for (int i = 1; i < oxygenTanks.Length; i++) {
				oxygenTanks [i].SetActive (true);
			}
			//*/
		}

		//StartCoroutine (beginGame());
	}


	IEnumerator beginGame(){
		//Time.timeScale = 0f;
		yield return new WaitForSecondsRealtime (3f);
		terrScript.InitialRaise();
		yield return new WaitForSecondsRealtime (10f);
	}



	void Awake() {
		paused = false;
		pauseMenu = GameObject.Find ("PauseMenu");
		pauseMenu.SetActive (paused);
		terrScript = terrain.GetComponent<TerrainScript> ();
		StartCoroutine (beginGame());
		Time.timeScale = 1.0f;
	}

	void Update () {
		if (!paused) {
			oxygenLeft -= Time.deltaTime;
			updatePlayerLight ();
		}
		if (Input.GetKeyDown (KeyCode.Escape)) {
			TogglePauseMenu ();
		}
	}

	void updatePlayerLight (){
		
		///*
		if (oxygenLeft > 45) { //set initial light
			if (waveCoroutine != null) {
				StopCoroutine (waveCoroutine);
				waveCoroutine = null;
			}
			spotLight.spotAngle = 120;
			spotLight.color = Color.white;
			spotLight.intensity = 3f;
		} else if (oxygenLeft > 5) {
			spotLight.spotAngle = ((120f - minLightAngle) * ((oxygenLeft - 5f) / 40f) + minLightAngle); //slowly shrink light angle
			if (oxygenLeft <= 15) {
				spotLight.spotAngle += 5f * Mathf.Sin ((oxygenLeft-15f) * 2); //pulse light angle
				spotLight.intensity = 3f + 2f * Mathf.Sin ((oxygenLeft-15f)); //wave of intensity
				if (waveCoroutine == null) {
					waveCoroutine = StartCoroutine(terrScript.Wave());
				}
			}
		} else if (oxygenLeft > 0.00001) {
			spotLight.spotAngle = 90f;
			spotLight.intensity = .01f;
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
