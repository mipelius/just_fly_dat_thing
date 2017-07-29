using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour {

	public GameObject panel;

	private bool hasActivatedOnce = false;

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.tag == "Player" && !hasActivatedOnce) {
			hasActivatedOnce = true;
			panel.SetActive (true);
		}
	}	

	void OnTriggerExit2D(Collider2D collider) {
		if (collider.tag == "Player") {
			panel.SetActive (false);
		}
	}
}
