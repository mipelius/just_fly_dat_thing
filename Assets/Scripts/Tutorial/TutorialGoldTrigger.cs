using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialGoldTrigger : MonoBehaviour {
	public GameObject goldPanel;
	public GameObject goldPanelText;

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.tag == "Player") {
			StartCoroutine ("ShowGoldPanel");
		}
	}

	public IEnumerator ShowGoldPanel() {
		int count = GoldManager.instance.GoldCount();

		string goldPanelTextStr;

		if (count <= 0) {
			goldPanelTextStr = "Great! Now go back and fly to the exit!";
		} else {
			if (count == 1) 
				goldPanelTextStr = "One more gold bar left!";
			else
				goldPanelTextStr = count.ToString() + " gold bars left!";
		}

		Text text = goldPanelText.GetComponent<Text> ();
		text.text = goldPanelTextStr;

		goldPanel.SetActive (true);

		yield return new WaitForSeconds(2);

		goldPanel.SetActive (false);
	}
}
