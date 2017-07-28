using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitShield : MonoBehaviour {	
	public float damageAmount;

	private AudioSource audioSource;

	void Awake() {
		audioSource = GetComponent<AudioSource> ();
	}

	void OnTriggerStay2D(Collider2D collider) {		
		if (collider.tag == "Player") {
			collider.gameObject.SendMessage ("ApplyDamage", damageAmount * Time.deltaTime);
			if (!audioSource.isPlaying) {
				audioSource.Play ();
			}
		}
	}

	void OnTriggerExit2D(Collider2D collider) {
		if (collider.tag == "Player") {
			audioSource.Stop ();
		}
	}
}
