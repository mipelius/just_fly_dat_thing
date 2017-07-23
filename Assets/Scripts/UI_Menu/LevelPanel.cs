using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPanel : MonoBehaviour {

	public static LevelPanel instance; 

	private List<LevelButton> levelButtons; 

	void Awake () {
		if (instance == null) {
			instance = this;
			levelButtons = new List<LevelButton> ();
		}
		else if (instance != this)
			Destroy(gameObject);		
	}

	void Start() {
		 //JUST FOR TESTING:
		List<User> users = UserManager.instance.GetUsers ();
		UserManager.instance.currentUser = users [1];
	}

	void Update () {
		foreach (LevelButton button in levelButtons) {
			if (button.levelNumber <= UserManager.instance.currentUser.level) {
				button.SetActive (true);
			} else {
				button.SetActive (false);
			}
		}	

		if (Input.GetKeyDown (KeyCode.Space)) {
			UserManager.instance.currentUser.level++;
		}
	}

	public void AddLevelButton(LevelButton button) {
		levelButtons.Add (button);
	}
}
