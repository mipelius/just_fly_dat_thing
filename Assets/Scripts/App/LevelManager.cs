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

		new Level(1, "Dummy 1", "Dummy", "dummylevel1"),
		new Level(2, "Dummy 2", "Dummy", "dummylevel2"),
		new Level(3, "Dummy 3", "Dummy", "dummylevel3"),
		new Level(4, "Dummy 4", "Dummy", "dummylevel4"),
		new Level(5, "Dummy 5", "Dummy", "dummylevel5"),
		new Level(6, "Dummy 6", "Dummy", "dummylevel6"),
		new Level(7, "Dummy 7", "Dummy", "dummylevel7"),
		new Level(8, "Dummy 8", "Dummy", "dummylevel8"),
		new Level(9, "Dummy 9", "Dummy", "dummylevel9"),
		new Level(10, "Dummy 10", "Dummy", "dummylevel10"),
		new Level(11, "Dummy 11", "Dummy", "dummylevel11"),
		new Level(12, "Dummy 12", "Dummy", "dummylevel12"),
		new Level(13, "Dummy 13", "Dummy", "dummylevel13"),
		new Level(14, "Dummy 14", "Dummy", "dummylevel14"),
		new Level(15, "Dummy 15", "Dummy", "dummylevel15"),
		new Level(16, "Dummy 16", "Dummy", "dummylevel16"),
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
		string filePath = GetFilePath ();

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
		} 
		// else nothing because there is no scores yet!
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

		_currentLevel = level;

		UnityEngine.SceneManagement.SceneManager.LoadScene (level.sceneName);
	}

	public int lastLevelNumber {
		get {
			return 16;	
		}
	}

	public void SaveScores() {
		List<Score> allScores = new List<Score> ();

		foreach (Level level in levels) {
			List<Score> levelScores = level.scores;
			foreach (Score score in levelScores) {
				allScores.Add (score);
			}
		}

		RawScoreData scoreData = new RawScoreData ();
		scoreData.scores = allScores.ToArray ();
		string jsonString = JsonUtility.ToJson (scoreData);
		string filePath = GetFilePath ();

		File.WriteAllText(filePath, jsonString);
	}

	private string GetFilePath() {
		return Path.Combine (Application.streamingAssetsPath, scoreDataFilePath);
	}
}
