using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.EventSystems;

public class HighScoreLevelButton : MonoBehaviour {
	
	public int levelNumber;

	public GameObject levelNameText;
	public GameObject levelImage;

	void Start () {
		Level level = LevelManager.instance.GetLevel (levelNumber);

		if (level != null) {
			Text text = levelNameText.GetComponent<Text> ();
			text.text = level.name;

			RawImage image = levelImage.GetComponent<RawImage> ();
			string imagePath = Path.Combine ("LevelImages", level.levelImage);
			image.texture = Resources.Load<Texture2D> (imagePath);
		}	
	}

	public void MouseClick() {
		HighscorePanel.instance.SetCurrentLevel (levelNumber);
	}
}