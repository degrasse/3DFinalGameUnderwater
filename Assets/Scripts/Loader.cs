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

	public void LoadEasy(){
		GameObject.Find ("OxygenTracker").GetComponent<OxygenTrackerScript> ().setInitOxygen (105, true);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
	public void LoadMedium(){
		GameObject.Find ("OxygenTracker").GetComponent<OxygenTrackerScript> ().setInitOxygen (90, true);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
	public void LoadHard(){
		GameObject.Find ("OxygenTracker").GetComponent<OxygenTrackerScript> ().setInitOxygen (75, true);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}


	public void Quit()
	{
		Application.Quit();
	}
}
