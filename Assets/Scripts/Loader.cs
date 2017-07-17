using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {

	public bool levelDevelopmentMode;

	public GameObject debugCanvas;
	public GameObject eventSystem;

	void Awake () {
		if (levelDevelopmentMode) {			
			debugCanvas = Instantiate (debugCanvas); DontDestroyOnLoad (debugCanvas);
			eventSystem = Instantiate (eventSystem); DontDestroyOnLoad (eventSystem);
		} else {
			UnityEngine.SceneManagement.SceneManager.LoadScene ("ScreenMain");
		}
	}
}
