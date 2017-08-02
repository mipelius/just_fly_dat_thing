using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialFinishedPanel : MonoBehaviour {
	public GameObject tutorialFinishedPanel;
	public GameObject tutorialScoring;

	public GameObject yourScoreTextObj;

	void Awake () {
		Time.timeScale = 1;
	}

	public void Show(int score) {
		Cursor.visible = true;

		User user = UserManager.instance.currentUser;
		Level level = LevelManager.instance.currentLevel;

		Time.timeScale = 0;
		tutorialFinishedPanel.SetActive (true);
		tutorialScoring.SetActive (true);
		Text yourScoreText = yourScoreTextObj.GetComponent<Text> ();

		try {
			yourScoreText.text = score.ToString ();
		} catch {
		}
	}

	public void ExitClicked() {
		UILevelManager.instance.ExitLevel ();
	}
}
