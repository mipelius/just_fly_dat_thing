using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour {

	public static TriggerManager instance = null;

	public enum triggerType { yellow, green, purple };

	private List<TriggerBehaviour> triggerBehaviours;

	void Awake () {
		if (instance == null) {
			instance = this;
			triggerBehaviours = new List<TriggerBehaviour> ();
		}
		else if (instance != this)
			Destroy(gameObject);
	}

	public void addBehaviour(TriggerBehaviour triggerBehaviour) {
		triggerBehaviours.Add (triggerBehaviour);
	}

	public void trigger(triggerType triggerType) {
		foreach (TriggerBehaviour triggerBehaviour in triggerBehaviours) {
			if (triggerBehaviour.triggerType == triggerType)
				triggerBehaviour.trigger = true;
		}
	}

	public void untrigger(triggerType triggerType) {
		foreach (TriggerBehaviour door in triggerBehaviours) {
			if (door.triggerType == triggerType)
				door.trigger = false;
		}
	}
}