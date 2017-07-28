using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour {

	public bool canUse;

	public GameObject redEyeParticle;
	public GameObject blueEyeParticle;
	public GameObject exitShieldParticle;

	void Start () {
		GoldManager.instance.AddExit (this);

		blueEyeParticle.GetComponent<ParticleSystem> ().Stop ();

		canUse = false;
	}

	private void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Player" && canUse) {
			UILevelManager.instance.LevelFinished ();
		}			
	}

	public void SetOpen() {
		if (canUse)
			return;

		exitShieldParticle.GetComponent<ParticleSystem> ().Stop ();
		redEyeParticle.GetComponent<ParticleSystem> ().Stop ();
		blueEyeParticle.GetComponent<ParticleSystem> ().Play ();

		exitShieldParticle.GetComponent<Collider2D> ().enabled = false;

		GetComponent<Collider2D> ().isTrigger = true;

		canUse = true;
	}
}
