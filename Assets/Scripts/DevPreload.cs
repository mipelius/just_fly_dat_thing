using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevPreload : MonoBehaviour {
	
	void Awake()
	{
		GameObject check = GameObject.Find("__app");

		if (check == null) {
			UnityEngine.SceneManagement.Scene scene = 
				UnityEngine.SceneManagement.SceneManager.GetActiveScene ();

			UnityEngine.SceneManagement.SceneManager.LoadScene ("_preload");
			UnityEngine.SceneManagement.SceneManager.LoadScene (scene.name);
		}
	}
}
