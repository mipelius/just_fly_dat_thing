using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Level : IComparable<Level> {
	private int _id;
	private string _name;
	private string _sceneName;
	private string _levelImage;

	private List<Score> _scores;

	public int CompareTo(Level other) {
		if (_id < other.id) {
			return -1;
		}

		if (_id > other.id) {
			return 1;
		}

		return 0;
	}

	public Level(int id, string name, string sceneName, string levelImage) {
		_id = id;
		_name = name;
		_sceneName = sceneName;
		_levelImage = levelImage;

		_scores = new List<Score> ();
	}

	public int id {
		get {
			return _id;
		}
	}

	public string name {
		get {
			return _name;
		}
	}

	public string sceneName {
		get {
			return _sceneName;
		}
	}

	public string levelImage {
		get {
			return _levelImage;
		}
	}

	public void AddScore(Score score) {	
		_scores.Add (score);
	}

	public List<Score> scores {
		get {
			List<Score> scoresToReturn = new List<Score> ();

			foreach (Score score in _scores) {
				scoresToReturn.Add (score);			
			}

			scoresToReturn.Sort ();

			return scoresToReturn;
		}
	}

	public Score highscore {
		get {
			_scores.Sort ();
			return _scores[0];
		}
	}

	public int bestScoreForUser(User user) {
		int bestScore = 0;

		foreach (Score score in _scores) {
			if (score.user_id == user.id) {
				if (bestScore < score.score) {
					bestScore = score.score;
				}
			}
		}

		return bestScore;
	}
}
