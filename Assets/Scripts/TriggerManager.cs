using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour {

	public static TriggerManager instance = null;

	public enum triggerType { yellow, green, purple };

	private List<Door> doors;

	void Awake () {
		if (instance == null) {
			instance = this;
			doors = new List<Door> ();
		}
		else if (instance != this)
			Destroy(gameObject);
	}

	public void addDoor(Door door) {
		doors.Add (door);
	}

	public void trigger(triggerType triggerType) {
		foreach (Door door in doors) {
			if (door.triggerType == triggerType)
				door.trigger = true;
		}
	}

	public void untrigger(triggerType triggerType) {
		foreach (Door door in doors) {
			if (door.triggerType == triggerType)
				door.trigger = false;
		}
	}
}