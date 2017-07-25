using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFinishedPanel : MonoBehaviour {

	public GameObject levelFinishedPanel;

	void Awake () {
		Time.timeScale = 1;
	}

	public void Show() {
		Time.timeScale = 0;
		levelFinishedPanel.SetActive (true);

		// update score text etc...
	}

	public void RestartClicked() {
		UILevelManager.instance.Restart ();
	}

	public void ExitClicked() {
		UILevelManager.instance.ExitLevel ();
	}

	public void NextClicked() {
		// next level
	}
}
