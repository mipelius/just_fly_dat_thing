using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteDialog : MonoBehaviour {

	public void Show() {
		gameObject.SetActive (true);
	}

	public void YesPressed() {
		User user = UserManager.instance.currentUser;
		UserManager.instance.DeleteUser (user);
		UnityEngine.SceneManagement.SceneManager.LoadScene ("ScreenMain");
	}

	public void NoPressed() {
		gameObject.SetActive (false);
	}
}
