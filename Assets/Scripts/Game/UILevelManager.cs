using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILevelManager : MonoBehaviour {
	public bool isTutorialMode;

	public static UILevelManager instance = null;

	public RectTransform healthBar;
	public GameObject bombPanel;
	public GameObject bombPanelText;
	public GameObject goldPanel;
	public GameObject goldPanelText;
	public GameObject exitPanel;
	public GameObject timeText;

	public float scoreMultiplier = 1000;
	public float scoreTimeAddition = 40;

	private Player player;

	private float awakeTimeStamp;

	void Awake () {
		if (instance == null) {
			instance = this;

			bombPanel.SetActive (true);
			goldPanel.SetActive (false);
			exitPanel.SetActive (false);

			awakeTimeStamp = Time.time;
		}
		else if (instance != this)
			Destroy(gameObject);
	}
	
	void Update () {
		UpdateHealthUI ();
		UpdateBombsUI ();
		UpdateGoldUI ();
		UpdateTimeUI ();
	}

	private void UpdateHealthUI() {
		if (player != null) {
			healthBar.offsetMax = new Vector2(player.health + healthBar.offsetMin.x, healthBar.offsetMax.y);
		}
	}

	private void UpdateBombsUI() {		
		Text text = bombPanelText.GetComponent<Text> ();
		text.text = player.bombs.ToString ();
	}

	private void UpdateGoldUI() {
		if (GoldManager.instance.GoldCount () > 0) {
			goldPanel.SetActive (true);
			exitPanel.SetActive (false);

			Text text = goldPanelText.GetComponent<Text> ();
			text.text = GoldManager.instance.GoldCount ().ToString ();
		} else {
			goldPanel.SetActive (false);
			exitPanel.SetActive (true);
		}
	}

	private void UpdateTimeUI() {
		Text text = timeText.GetComponent<Text> ();
		float time = Time.time - awakeTimeStamp;

		string timeStr = string.Format("{0:0}:{1:00}.{2:0}",
			Mathf.Floor(time / 60),//minutes
			Mathf.Floor(time) % 60,//seconds
			Mathf.Floor((time * 10) % 10));//miliseconds

		text.text = "Time: " + timeStr;
	}

	public void SetPlayer(Player player) {
		this.player = player;
	}

	public void Restart () {		
		UnityEngine.SceneManagement.Scene scene = 
			UnityEngine.SceneManagement.SceneManager.GetActiveScene ();

		UnityEngine.SceneManagement.SceneManager.LoadScene(scene.name);
	}

	public void ExitLevel () {
		UnityEngine.SceneManagement.SceneManager.LoadScene ("ScreenLevelSelect");
	}

	public void LevelFinished () {
		if (isTutorialMode) {
			UnityEngine.SceneManagement.Scene scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene ();

			if (scene.name == "TutorialBasics")
				UnityEngine.SceneManagement.SceneManager.LoadScene ("TutorialDoors");
			else if (scene.name == "TutorialDoors")
				UnityEngine.SceneManagement.SceneManager.LoadScene ("TutorialBombsAndStuff");
			else {
				TutorialFinishedPanel tutorialPanel = gameObject.GetComponent<TutorialFinishedPanel> ();
				tutorialPanel.Show (CalculateScore ());
			}

			return;
		}

		User user = UserManager.instance.currentUser;
		Level level = LevelManager.instance.currentLevel;

		if (user != null && level != null) {
			// raise user level up if necessary

			if (user.level <= level.id) {
				UserManager.instance.currentUserLevelUp ();
			}

			// give some score

			Score score = new Score();

			score.level_id = level.id;
			score.user_id = user.id;
			score.score = CalculateScore();

			level.AddScore (score);
			LevelManager.instance.SaveScores ();
		}

		LevelFinishedPanel panel = gameObject.GetComponent<LevelFinishedPanel> ();
		panel.Show (CalculateScore());
	}

	public int CalculateScore() {
		float time = Time.time - awakeTimeStamp;

		int score = Mathf.FloorToInt (
			(player.health / (time + scoreTimeAddition)) 
			* scoreMultiplier
		);

		return score;
	}

}