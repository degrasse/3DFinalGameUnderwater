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
		if (MenuPanel != null)
			MenuPanel.SetActive (true);
		if (AudioPanel != null)
			AudioPanel.SetActive(false);
		if (HelpPanel != null)
			HelpPanel.SetActive(false);
		if (InstructionPanel != null)
			InstructionPanel.SetActive(false);
	}


	public void ShowAudioPanel(){
		MenuPanel.SetActive (false);
		AudioPanel.SetActive (true);

	}


}