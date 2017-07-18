using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour {

	public bool canUse = false;

	public Sprite exitOpenSprite;
	public Sprite exitClosedSprite;

	private SpriteRenderer spriteRenderer;

	void Start () {
		GoldManager.instance.AddExit (this);
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	void Update() {
		if (canUse) {
			spriteRenderer.sprite = exitOpenSprite;
		} else {
			spriteRenderer.sprite = exitClosedSprite;
		}

	}
	private void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Player" && canUse) {
			UnityEngine.SceneManagement.Scene scene = 
				UnityEngine.SceneManagement.SceneManager.GetActiveScene ();

			UnityEngine.SceneManagement.SceneManager.LoadScene(scene.name);
		}			
	}
}
