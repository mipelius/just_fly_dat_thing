using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour {

	public bool canUse = false;

	public GameObject redEyeParticle;
	public GameObject blueEyeParticle;
	public GameObject exitShieldParticle;

	private SpriteRenderer spriteRenderer;

	void Start () {
		GoldManager.instance.AddExit (this);
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	void Update() {
		Collider2D collider = GetComponent<Collider2D> ();

		if (canUse) {
			exitShieldParticle.SetActive (false);
			redEyeParticle.SetActive (false);
			blueEyeParticle.SetActive (true);
			collider.isTrigger = true;
		} else {
			exitShieldParticle.SetActive (true);
			redEyeParticle.SetActive (true);
			blueEyeParticle.SetActive (false);
			collider.isTrigger = false;
		}
	}

	private void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Player" && canUse) {
			UILevelManager.instance.LevelFinished ();
		}			
	}
}
