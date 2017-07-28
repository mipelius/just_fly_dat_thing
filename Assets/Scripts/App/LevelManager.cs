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
		new Level (0, "Tutorial", "Tutorial", "AnotherBrick"),
		new Level (1, "Chillin\'", "ChillinFinal", "Chillin"),
		new Level (2, "Hypnosis", "HypnosisFinal", "Hypnosis"),
		new Level (3, "Another Brick", "AnotherBrickFinal", "AnotherBrick"),
		new Level (4, "Falling In Love", "FallingInLoveFinal", "FallingInLove"),
		new Level (5, "Run F. Run", "RunFRunFinal", "RunFRun"),
		new Level (6, "Home Alone", "HomeAloneFinal", "HomeAlone"),
		new Level (7, "Asthma Pipe", "AsthmaPipeFinal", "AsthmaPipe"),
		new Level (8, "Crazy Day", "CrazyDayFinal", "CrazyDay"),
		new Level (9, "Secret Buttons", "SecretButtonsFinal", "SecretButtons"),
		new Level (10, "Rock'N'Buttons", "RockNButtonsFinal", "RockNButtons")
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
			return 10;	
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
