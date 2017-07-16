using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

	public bool trigger;
	public float doorForce;
	public TriggerManager.triggerType triggerType;

	private Rigidbody2D rb;

	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		TriggerManager.instance.addDoor (this);
	}
	
	// Update is called once per frame
	void Update () {
		if (trigger)
			rb.AddForce (new Vector2 (doorForce, 0));
		else
			rb.AddForce (new Vector2 (-doorForce, 0));
	}
}
