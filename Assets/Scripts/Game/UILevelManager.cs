using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILevelManager : MonoBehaviour {

	public static UILevelManager instance = null;

	public RectTransform healthBar;
	public GameObject bombPanel;
	public GameObject bombPanelText;
	public GameObject goldPanel;
	public GameObject goldPanelText;
	public GameObject exitPanel;
	public GameObject timeText;

	private Player player;

	private float awakeTimeStamp;

	void Awake () {
		if (instance == null) {
			instance = this;

			bombPanel.SetActive (false);
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
		if (player.bombs > 0) {
			bombPanel.SetActive (true);
			Text text = bombPanelText.GetComponent<Text> ();
			text.text = player.bombs.ToString ();

		} else {
			bombPanel.SetActive (false);
		}
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
		User user = UserManager.instance.currentUser;

		if (user != null) {
			// if this level number equals User.level
			// then user -> level up
		}

		LevelFinishedPanel panel = gameObject.GetComponent<LevelFinishedPanel> ();
		panel.Show ();
	}
}
