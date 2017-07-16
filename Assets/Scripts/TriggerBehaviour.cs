using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TriggerBehaviour : MonoBehaviour {

	public bool trigger;
	public TriggerManager.triggerType triggerType;

	protected virtual void Start () {
		TriggerManager.instance.addBehaviour (this);
	}

	void Update () {
		if (trigger)
			OnTriggerEnter ();
		else
			OnTriggerExit ();
	}

	protected abstract void OnTriggerEnter ();
	protected abstract void OnTriggerExit ();
}
