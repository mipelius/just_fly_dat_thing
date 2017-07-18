using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {

	public bool levelDevelopmentMode;

	public GameObject debugCanvas;

	void Awake () {
		if (levelDevelopmentMode) {			
			debugCanvas = Instantiate (debugCanvas); DontDestroyOnLoad (debugCanvas);
		} else {
			UnityEngine.SceneManagement.SceneManager.LoadScene ("ScreenMain");
		}
	}
}
