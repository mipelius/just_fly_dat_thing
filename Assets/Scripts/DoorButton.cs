using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour {

	public TriggerManager.triggerType triggerType;

	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		TriggerManager.instance.trigger(triggerType);
	}

	void OnTriggerExit2D(Collider2D other) {
		TriggerManager.instance.untrigger(triggerType);
	}
}
