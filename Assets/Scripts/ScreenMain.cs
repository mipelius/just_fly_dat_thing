﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenMain : MonoBehaviour {
	public void NewGame() {
		UnityEngine.SceneManagement.SceneManager.LoadScene (2);
	}

	public void Quit() {
		Application.Quit ();
	}
}
