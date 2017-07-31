using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialGoldTrigger : MonoBehaviour {
	public GameObject goldPanel;

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.tag == "Player") {
			TutorialGoldPanel panel = goldPanel.GetComponent<TutorialGoldPanel> ();
			panel.Show ();
		}
	}
}
