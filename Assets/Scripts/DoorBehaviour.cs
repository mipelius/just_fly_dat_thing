using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : TriggerBehaviour {

	public float doorForce;
	public bool isVertical;
	public bool hasPositiveOpeningDirection;

	private Rigidbody2D rb;

	protected override void Start () {
		rb = GetComponent<Rigidbody2D> ();
		base.Start ();
	}

	protected override void OnTriggerEnter() {
		Vector2 force = PrepareForce ();
		rb.AddForce (force);
	}

	protected override void OnTriggerExit() {
		Vector2 force = PrepareForce ();
		rb.AddForce (-force);
	}

	private Vector2 PrepareForce() {
		Vector2 force = isVertical ? new Vector2 (0, doorForce) : new Vector2 (doorForce, 0);
		if(!hasPositiveOpeningDirection) 
			force = -force;

		return force;
	}
}