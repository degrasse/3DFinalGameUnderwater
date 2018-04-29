using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWonScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void LoadMainMenu(){
		SceneManager.LoadScene ("deGrasse");
	}
	public void LoadGame(){
		GameObject.Find ("OxygenTracker").GetComponent<OxygenTrackerScript> ().setInitOxygen (Mathf.Infinity, true);
		SceneManager.LoadScene ("Phillip");
	}


}
