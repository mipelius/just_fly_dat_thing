using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialGoldPanel : MonoBehaviour {
	public GameObject goldPanelText;

	public GameObject statGoldArrow;

	private float triggerTimeStamp;

	public void Show() {		
		int count = GoldManager.instance.GoldCount();

		string goldPanelTextStr = "";

		if (count <= 0) {
			goldPanelTextStr = "Great! Now go back and fly to the eye. The Eye is the exit of the level.";
		} else if (count == 1) {
			goldPanelTextStr = "One more gold bar left!";
		} else if (count == 3) {
			goldPanelTextStr = "You can see the amount of gold bars left on the top left corner of the screen.";
			statGoldArrow.SetActive (true);			
		} else {
			return;
		}
			
		Text text = goldPanelText.GetComponent<Text> ();
		text.text = goldPanelTextStr;

		gameObject.SetActive (true);

		StopAllCoroutines ();
		StartCoroutine ("ShowPanel");
	}

	public IEnumerator ShowPanel() {	
		yield return new WaitForSeconds(3);

		statGoldArrow.SetActive (false);
		gameObject.SetActive (false);
	}
}
