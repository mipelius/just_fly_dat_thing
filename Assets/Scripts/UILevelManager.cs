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

	private Player player;

	void Awake () {
		if (instance == null) {
			instance = this;

			bombPanel.SetActive (false);
			goldPanel.SetActive (false);
			exitPanel.SetActive (false);
		}
		else if (instance != this)
			Destroy(gameObject);
	}
	
	void Update () {
		if (player != null) {
			healthBar.offsetMax = new Vector2(player.health + healthBar.offsetMin.x + 1, healthBar.offsetMax.y);
		}
		if (player.bombs > 0) {
			bombPanel.SetActive (true);
			Text text = bombPanelText.GetComponent<Text> ();
			text.text = player.bombs.ToString ();

		} else {
			bombPanel.SetActive (false);
		}
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

	public void SetPlayer(Player player) {
		this.player = player;
	}

	public void Restart () {
		UnityEngine.SceneManagement.Scene scene = 
			UnityEngine.SceneManagement.SceneManager.GetActiveScene ();

		UnityEngine.SceneManagement.SceneManager.LoadScene(scene.name);
	}
}
