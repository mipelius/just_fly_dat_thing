using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGamePanel : MonoBehaviour {

	public GameObject pauseGamePanel;

	void Awake() {
		Cursor.visible = false;
		Time.timeScale = 1;
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (UILevelManager.instance.levelFinished) {
				return;
			}

			bool isActive = pauseGamePanel.activeSelf;

			if (!isActive) {
				Time.timeScale = 0;
				Cursor.visible = true;
			} else {
				Time.timeScale = 1;
				Cursor.visible = false;
			}

			pauseGamePanel.SetActive (!isActive);
		}
	}

	public void ResumeGameClicked() {
		Time.timeScale = 1;
		pauseGamePanel.SetActive (false);
	}

	public void RestartClicked() {
		Cursor.visible = false;
		Time.timeScale = 1;	
		UILevelManager.instance.Restart ();
	}

	public void ExitLevelClicked() {
		UILevelManager.instance.ExitLevel ();
	}
}
