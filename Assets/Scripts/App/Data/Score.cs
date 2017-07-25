using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Score : IComparable<Score> {
	public int level_id;
	public int user_id;
	public int score;

	public int CompareTo(Score other) {
		if (score < other.score)
			return -1;

		if (score > other.score)
			return 1;

		return 0;
	}
}
