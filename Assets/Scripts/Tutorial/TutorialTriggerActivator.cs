using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTriggerActivator : MonoBehaviour {
	public GameObject triggerOther;

	private bool hasActivatedOnce = false;

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.tag == "Player" && !hasActivatedOnce) {
			hasActivatedOnce = true;

			triggerOther.SetActive (true);			
		}
	}
}
