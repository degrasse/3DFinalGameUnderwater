using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Phillip Hetzler
public class DeathSceneManager : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void LoadMainMenu(){ //load main menu
		SceneManager.LoadScene ("deGrasse");
	}
	public void LoadGame(){ //load game scene
		SceneManager.LoadScene ("Phillip");
	}


}