using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LevelManager : MonoBehaviour {

	public static LevelManager instance = null;

	[System.Serializable]
	class RawScoreData {
		public Score[] scores;
	}

	private string scoreDataFilePath = "scores.json";

	private Level[] levels = {
		new Level(0, "Tutorial", "Tutorial", "levelY"),
		new Level(1, "Kakki", "ExampleFull", "levelX"),
		new Level(2, "Nakki", "Example1", "levelY"),
		new Level(3, "HIHII", "Example2", "levelX"),
		new Level(4, "HOHOO", "Example3", "levelY")
	};

	private Level _currentLevel = null;

	public void Awake() {
		if (instance == null) {
			instance = this;

			LoadScores();
		}
		else if (instance != this)
			Destroy(gameObject);
	}

	public void LoadScores() {		
		string filePath = Path.Combine (Application.streamingAssetsPath, scoreDataFilePath);

		if (File.Exists (filePath)) {
			string jsonString = File.ReadAllText (filePath);				
			RawScoreData scoreData = JsonUtility.FromJson<RawScoreData> (jsonString);			

			foreach (Score score in scoreData.scores) {
				foreach (Level level in levels) {
					if (level.id == score.level_id) {
						level.AddScore (score);
					}
				}
			}
		} else {
			Debug.LogError ("Cannot find game data!");
		}
	}

	public List<Level> GetLevels() {
		List<Level> levelsToReturn = new List<Level> ();

		foreach (Level level in levels) {
			levelsToReturn.Add (level);
		}

		levelsToReturn.Sort ();

		return levelsToReturn;
	}

	public Level currentLevel {
		get { 
			return _currentLevel; 
		}
	}

	public Level GetLevel(int levelNumber) {
		foreach (Level level in levels) {
			if (level.id == levelNumber) {
				return level;
			}
		}
		return null;
	}

	public void LoadLevel(int levelNumber) {
		Level level = GetLevel (levelNumber);

		if (level == null) {
			throw new UnityException ("No such level, level = " + levelNumber);
		}

		UnityEngine.SceneManagement.SceneManager.LoadScene (level.sceneName);

	}
}
