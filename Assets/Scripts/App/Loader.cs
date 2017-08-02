using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {

	void Awake () {
		Cursor.visible = true;

		#if !UNITY_EDITOR
			UnityEngine.SceneManagement.SceneManager.LoadScene ("ScreenMain");
		#endif
		// -- else stays in current scene --
	}
}
