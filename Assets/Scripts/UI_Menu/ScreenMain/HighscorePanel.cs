using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscorePanel : MonoBehaviour {

	public static HighscorePanel instance;

	public GameObject HighscoreView;

	public GameObject[] playerNameTexts;
	public GameObject[] scoreTexts;

	void Awake () {
		if (instance == null) {
			instance = this;
		}
		else if (instance != this)
			Destroy(gameObject);	
	}

	public void SetCurrentLevel(int levelNumber) {
		HighscoreView.SetActive (true);

		Level level = LevelManager.instance.GetLevel (levelNumber);

		List<Score> scores = level.scores;

		scores.Sort ();
		scores.Reverse ();

		for (int i = 0; i < 10; i++) {			
			Text playerText = playerNameTexts[i].GetComponent<Text> ();
			Text scoreText = scoreTexts[i].GetComponent<Text> ();

			if (i < scores.Count) {
				User user = UserManager.instance.GetUser (scores [i].user_id);

				playerText.text = user.name;
				scoreText.text = scores [i].score.ToString();
			} else {
				playerText.text = "";
				scoreText.text = "";
			}
		}
	}

	public void HideScoreview () {
		HighscoreView.SetActive (false);
	}
}
