using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldManager : MonoBehaviour {

	public static GoldManager instance = null;

	private List<Gold> golds;
	private List<Exit> exits;

	void Awake () {
		if (instance == null) {
			instance = this;
			golds = new List<Gold> ();
			exits = new List<Exit> ();
		}
		else if (instance != this)
			Destroy(gameObject);
	}

	void Update() {
		if (golds.Count <= 0) {
			foreach (Exit exit in exits) {
				exit.SetOpen ();
			}
		}
	}

	public void AddGold(Gold gold) {
		golds.Add (gold);
	}

	public void RemoveGold(Gold gold) {
		golds.Remove (gold);
	}

	public void AddExit(Exit exit) {
		exits.Add (exit);
	}

	public int GoldCount() {
		return golds.Count;
	}
}
