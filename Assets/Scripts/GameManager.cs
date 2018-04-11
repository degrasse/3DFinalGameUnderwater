using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	public GameObject playerLight;
	public float initOxygen, minLightAngle, minLightIntensity;

	private GameObject pauseMenu;
	private bool paused;
	private Light spotLight;
	private float oxygenLeft;

	void Start () {
		oxygenLeft = initOxygen;
		spotLight = playerLight.GetComponent<Light> ();
		spotLight.type = LightType.Spot;
	}

	void Awake() {
		paused = false;
		pauseMenu = GameObject.Find ("PauseMenu");
		pauseMenu.SetActive (paused);
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
			spotLight.spotAngle = 120;
			spotLight.color = Color.white;
			spotLight.intensity = 3f;
		} else if (oxygenLeft > 5) {
			spotLight.spotAngle = ((120f - minLightAngle) * ((oxygenLeft - 5f) / 40f) + minLightAngle); //slowly shrink light angle
			if (oxygenLeft <= 15) {
				spotLight.spotAngle += 5f * Mathf.Sin ((oxygenLeft-15f) * 2); //pulse light angle
				spotLight.intensity = 3f + 2f * Mathf.Sin ((oxygenLeft-15f)); //wave of intensity

			}
		} else if (oxygenLeft > 0.00001) {
			spotLight.intensity = .1f;
			spotLight.bounceIntensity = .5f;
			spotLight.color = new Color(UnityEngine.Random.Range(0,256),UnityEngine.Random.Range(0,256),UnityEngine.Random.Range(0,256));
		}
		else {
			spotLight.spotAngle = minLightAngle;
			spotLight.color = Color.red;
			spotLight.intensity = 2f;
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
}
