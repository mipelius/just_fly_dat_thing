using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class LevelButton : MonoBehaviour {

	private Button button;

	public GameObject disabledPanel;

	public int levelNumber;

	public GameObject levelNameText;
	public GameObject levelImage;

	void Start () {
		button = GetComponent<Button> ();
		LevelPanel.instance.AddLevelButton (this);

		Level level = LevelManager.instance.GetLevel (levelNumber);

		if (level != null) {
			Text text = levelNameText.GetComponent<Text> ();
			text.text = level.name;

			RawImage image = levelImage.GetComponent<RawImage> ();
			string imagePath = Path.Combine ("LevelImages", level.levelImage);
			image.texture = Resources.Load<Texture2D> (imagePath);
		}
	}

	public void LoadScene() {
		LevelManager.instance.LoadLevel(levelNumber);
	}

	public void SetActive(bool enabled) {
		button.interactable = enabled;
		disabledPanel.SetActive (!enabled);
	}

	public void OnMouseEnter() {
		Debug.Log ("Nappiiii");
	}
}
