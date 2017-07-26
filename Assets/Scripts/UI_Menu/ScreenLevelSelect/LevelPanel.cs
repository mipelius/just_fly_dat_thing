using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelPanel : MonoBehaviour {

	public static LevelPanel instance; 

	public GameObject levelInfoPanel;

	public GameObject levelNumberTextObj;
	public GameObject levelNameTextObj;
	public GameObject yourScoreTextObj;
	public GameObject highScoreTextObj;

	private List<LevelButton> levelButtons; 

	void Awake () {
		if (instance == null) {
			instance = this;
			levelButtons = new List<LevelButton> ();
		}
		else if (instance != this)
			Destroy(gameObject);		
	}

	void Update () {
		int userLevel = 100;

		if (UserManager.instance.currentUser != null) {
			userLevel = UserManager.instance.currentUser.level;
		}

		foreach (LevelButton button in levelButtons) {
			if (button.levelNumber <= userLevel) {
				button.SetActive (true);
			} else {
				button.SetActive (false);
			}
		}	
	}

	public void AddLevelButton(LevelButton button) {
		levelButtons.Add (button);
	}

	public void MainMenuClick () {
		UnityEngine.SceneManagement.SceneManager.LoadScene ("ScreenMain");
	}

	public void ShowLevelInfo(int levelNumber) {
		User user = UserManager.instance.currentUser;
		Level level = LevelManager.instance.GetLevel (levelNumber);

		Text levelNumberText = levelNumberTextObj.GetComponent<Text> ();
		Text levelNameText = levelNameTextObj.GetComponent<Text> ();
		Text yourScoreText = yourScoreTextObj.GetComponent<Text> ();
		Text highScoreText = highScoreTextObj.GetComponent<Text> ();

		// level number & level name

		levelNumberText.text = "Level " + levelNumber.ToString ();
		levelNameText.text = level.name;

		// player score

		string playerScoreStr = "-";
		Score playerScore = level.BestScoreForUser (user);		
		if (playerScore != null) {
			playerScoreStr = playerScore.score.ToString();
		}
		yourScoreText.text = playerScoreStr;

		// highscore

		Score highscore = level.highscore;
		string highscoreStr = "-";

		if (highscore != null) {
			highscoreStr = highscore.score.ToString();
		}
		highScoreText.text = highscoreStr;

		// --

		levelInfoPanel.SetActive (true);
	}

	public void HideLevelInfo() {
		levelInfoPanel.SetActive (false);
	}

}
