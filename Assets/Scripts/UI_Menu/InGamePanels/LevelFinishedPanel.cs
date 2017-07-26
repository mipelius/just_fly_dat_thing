using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelFinishedPanel : MonoBehaviour {

	public GameObject levelFinishedPanel;

	public GameObject nextButtonObj;

	public GameObject yourScoreTextObj;
	public GameObject yourBestScoreTextObj;
	public GameObject highscoreTextObj;

	void Awake () {
		Time.timeScale = 1;
	}

	public void Show(int score) {
		User user = UserManager.instance.currentUser;
		Level level = LevelManager.instance.currentLevel;

		if (level == null || level.id >= LevelManager.instance.lastLevelNumber) {
			Button button = nextButtonObj.GetComponent<Button> ();
			button.interactable = false;
		}

		Time.timeScale = 0;
		levelFinishedPanel.SetActive (true);

		Text yourScoreText = yourScoreTextObj.GetComponent<Text> ();
		Text yourBestScoreText = yourBestScoreTextObj.GetComponent<Text> ();
		Text highscoreText = highscoreTextObj.GetComponent<Text> ();

		try {
			yourScoreText.text = score.ToString ();

			yourBestScoreText.text = 
				level.BestScoreForUser (user).score.ToString ();
			highscoreText.text = 
				level.highscore.score.ToString ();
		} catch {
		}

	}

	public void RestartClicked() {
		UILevelManager.instance.Restart ();
	}

	public void ExitClicked() {
		UILevelManager.instance.ExitLevel ();
	}

	public void NextClicked() {
		Level level = LevelManager.instance.currentLevel;
		LevelManager.instance.LoadLevel (level.id + 1);
	}
}
