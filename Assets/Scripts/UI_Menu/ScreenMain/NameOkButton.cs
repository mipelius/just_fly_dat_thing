using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameOkButton : MonoBehaviour {

	public GameObject newPlayerNameText;
	public GameObject errorPanel;

	private Button button;
	private Text text;

	void Start() {
		button = GetComponent<Button> ();
		text = newPlayerNameText.GetComponent<Text> ();
	}

	void Update () {		
		if (text.text.Equals ("")) {
			errorPanel.SetActive (false);
			button.interactable = false;	
		} else if (UserManager.instance.GetUser (text.text) != null) {
			errorPanel.SetActive (true);
			button.interactable = false;
		}
		else {
			errorPanel.SetActive (false);
			button.interactable = true;
		}
	}

	public void Click() {
		gameObject.SetActive (false);
		User user = UserManager.instance.CreateUser (text.text);
		UserManager.instance.currentUser = user;
		UnityEngine.SceneManagement.SceneManager.LoadScene ("ScreenLevelSelect");
	}
}
