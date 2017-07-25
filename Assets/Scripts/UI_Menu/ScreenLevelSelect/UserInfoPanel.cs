using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInfoPanel : MonoBehaviour {
	
	public GameObject userInfoText;

	void Update () {
		if (UserManager.instance.currentUser != null) {
			Text text = userInfoText.GetComponent<Text> ();
			text.text = "Player: " + UserManager.instance.currentUser.name;	
		}
	}
}
