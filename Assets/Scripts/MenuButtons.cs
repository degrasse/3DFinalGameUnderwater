using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : MonoBehaviour {

	public GameObject MenuPanel;
	public GameObject AudioPanel;
	public GameObject HelpPanel;
	public GameObject InstructionPanel;

	// Use this for initialization
	void Start () {
		MenuPanel.SetActive (true);
		AudioPanel.SetActive(false);
		HelpPanel.SetActive(false);
		InstructionPanel.SetActive(false);
	}


	public void ShowAudioPanel(){
		MenuPanel.SetActive (false);
		AudioPanel.SetActive (true);

	}


}