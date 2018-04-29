using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	public void Load(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void LoadEasy(){ //set easy oxygen amount and load the game
		GameObject.Find ("OxygenTracker").GetComponent<OxygenTrackerScript> ().setInitOxygen (105, true);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
	public void LoadMedium(){ //set medium oxygen amount and load the game
		GameObject.Find ("OxygenTracker").GetComponent<OxygenTrackerScript> ().setInitOxygen (90, true);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
	public void LoadHard(){ //set hard oxygen amount and load the game
		GameObject.Find ("OxygenTracker").GetComponent<OxygenTrackerScript> ().setInitOxygen (75, true);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}


	public void Quit()
	{
		Application.Quit();
	}
}
