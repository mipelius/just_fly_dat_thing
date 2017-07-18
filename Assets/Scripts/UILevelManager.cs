using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILevelManager : MonoBehaviour {

	public static UILevelManager instance = null;

	public RectTransform healthBar;

	private Player player;

	void Awake () {
		if (instance == null) {
			instance = this;
		}
		else if (instance != this)
			Destroy(gameObject);
	}
	
	void Update () {
		if (player != null) {
			healthBar.offsetMax = new Vector2(player.health + healthBar.offsetMin.x + 1, healthBar.offsetMax.y);
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
