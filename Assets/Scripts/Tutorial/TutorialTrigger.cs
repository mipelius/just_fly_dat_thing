using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour {

	public GameObject[] panels;

	private bool hasActivatedOnce = false;

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.tag == "Player" && !hasActivatedOnce) {
			hasActivatedOnce = true;

			foreach (GameObject panel in panels) {
				panel.SetActive (true);
			}
		}
	}

	void OnTriggerExit2D(Collider2D collider) {
		if (collider.tag == "Player") {
			foreach (GameObject panel in panels) {
				panel.SetActive (false);
			}
		}
	}
}
