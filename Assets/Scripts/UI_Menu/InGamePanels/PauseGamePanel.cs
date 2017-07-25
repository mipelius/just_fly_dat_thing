using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGamePanel : MonoBehaviour {

	public GameObject pauseGamePanel;

	void Awake() {
		Time.timeScale = 1;
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			bool isActive = pauseGamePanel.activeSelf;

			if (!isActive) {
				Time.timeScale = 0;
			} else {
				Time.timeScale = 1;
			}

			pauseGamePanel.SetActive (!isActive);
		}
	}

	public void ResumeGameClicked() {
		Time.timeScale = 1;
		pauseGamePanel.SetActive (false);
	}

	public void RestartClicked() {
		Time.timeScale = 1;	
		UILevelManager.instance.Restart ();
	}

	public void ExitLevelClicked() {
		UILevelManager.instance.ExitLevel ();
	}
}
