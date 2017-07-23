using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {

	public bool levelDevelopmentMode;

	public GameObject debugCanvas;

	void Awake () {
		if (levelDevelopmentMode) {			
			// -- stays in current scene --
		} else {
			UnityEngine.SceneManagement.SceneManager.LoadScene ("ScreenMain");
		}
	}
}
