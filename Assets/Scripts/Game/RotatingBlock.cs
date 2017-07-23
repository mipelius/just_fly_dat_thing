using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingBlock : MonoBehaviour {

	public float torque = 10;
	public float angularVelocityAtTheBeginning = 100;
	public float maxAngularVelocity;

	private Rigidbody2D rb;

	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		rb.angularVelocity = angularVelocityAtTheBeginning;
	}
	
	// Update is called once per frame
	void FixedUpdate () {		
		if (Mathf.Abs(rb.angularVelocity) < maxAngularVelocity) 
			rb.AddTorque (torque);
	}
}
