using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {

	public bool developmentMode;

	public GameObject debugCanvas;
	public GameObject eventSystem;

	void Awake () {
		if (developmentMode) {			
			debugCanvas = Instantiate (debugCanvas); DontDestroyOnLoad (debugCanvas);
			eventSystem = Instantiate (eventSystem); DontDestroyOnLoad (eventSystem);
		} else {
			
		}
	}
}
