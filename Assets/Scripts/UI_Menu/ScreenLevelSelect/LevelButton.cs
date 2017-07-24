using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour {

	private Button button;

	public GameObject disabledPanel;

	public int levelNumber;

	public string sceneToLoad;

	void Start () {
		button = GetComponent<Button> ();
		LevelPanel.instance.AddLevelButton (this); 
	}

	public void LoadScene() { 
		UnityEngine.SceneManagement.SceneManager.LoadScene (sceneToLoad);
	}

	public void SetActive(bool enabled) {
		button.interactable = enabled;
		disabledPanel.SetActive (!enabled);
	}
}
