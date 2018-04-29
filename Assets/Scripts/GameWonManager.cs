using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Phillip Hetzler
public class GameWonManager : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void LoadMainMenu(){ //load main menu
		SceneManager.LoadScene ("deGrasse");
	}
	public void LoadGame(){ //set infinite oxygen and load game scene
		GameObject.Find ("OxygenTracker").GetComponent<OxygenTrackerScript> ().setInitOxygen (Mathf.Infinity, true);
		SceneManager.LoadScene ("Phillip");
	}


}