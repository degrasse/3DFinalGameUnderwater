using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : MonoBehaviour {

	public GameObject MenuPanel;
	public GameObject AudioPanel;
	public GameObject HelpPanel;
	public GameObject InstructionPanel;
	public GameObject InstructionPanel2;

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
		if (InstructionPanel2 != null)
			InstructionPanel2.SetActive(false);
	}


	public void ShowAudioPanel(){
		if (AudioPanel != null && MenuPanel != null) {
			MenuPanel.SetActive (false);
			AudioPanel.SetActive (true);
		}
	}


}