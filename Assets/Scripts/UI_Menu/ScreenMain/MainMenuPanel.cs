using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanel : MonoBehaviour {

	public Transform canvas;

	public GameObject resumeGamePanel;
	public GameObject newGamePanel;
	public GameObject highscoresPanel;

	public GameObject backButton;

	private GameObject currentPanel = null;


	public void ResumeGameClick() {
		OpenPanel (resumeGamePanel);
	}

	public void NewGameClick() {
		OpenPanel (newGamePanel);
	}

	public void HighscoresClick() {
		OpenPanel (highscoresPanel);
	}

	public void QuitClick() {
		Application.Quit ();
	}

	private void OpenPanel(GameObject panel) {
		currentPanel = Instantiate (panel, canvas);
		gameObject.SetActive (false);
		CreateBackButton ();
	}

	private void CreateBackButton() {
		GameObject currentBackButton = Instantiate (
			backButton, currentPanel.transform
		);

		Button button = currentBackButton.GetComponent<Button> ();
		button.onClick.AddListener (delegate {
			BackButtonClick ();
		});
	}

	private void BackButtonClick() {
		Destroy (currentPanel);
		currentPanel = null;
		gameObject.SetActive (true);
	}
}
