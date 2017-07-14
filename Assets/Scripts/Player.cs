using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	
	public float rotationSpeed;
	public float acceleration;
	public float maxRotation;
	public float rotationBrakeFactor;
	public float shockDuration;
	public float shockTriggerVelocity;
	public float shockFactor;

	private Text text1;
	private Text text2;
	private Text text3;

	private Rigidbody2D rb;

	private float shockTimeStamp = 0;
	private float shockPower = 0;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		text1 = GameObject.Find("DebugText1").GetComponent<Text>();
		text2 = GameObject.Find("DebugText2").GetComponent<Text>();
		text3 = GameObject.Find("DebugText3").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void FixedUpdate  () {
		float rotationInput = Input.GetAxisRaw ("Horizontal");
		Rotate (rotationInput);

		float accelerationInput = Input.GetAxisRaw ("Vertical");
		Accelerate (accelerationInput);
	}

	private void Rotate (float rotationInput) {
		float torque = 0;

		if (rotationInput == 0) {			
			torque = -Mathf.Sign (rb.angularVelocity) * rotationBrakeFactor;

			if (Mathf.Abs (rb.angularVelocity) < Mathf.Abs (torque)) {
				rb.angularVelocity = 0;
				torque = 0;
			}
		} else if (			
			Mathf.Abs (rb.angularVelocity) < maxRotation ||
			Mathf.Sign (rb.angularVelocity) != Mathf.Sign (-rotationInput)
		) {
			torque = -rotationInput * rotationSpeed;		
		}

		float torqueShockFactor = Mathf.Abs (shockTimeStamp - Time.time) / shockDuration;
		torqueShockFactor = Mathf.Pow (torqueShockFactor, shockPower);

		if (torqueShockFactor > 1) {
			torqueShockFactor = 1;
			text1.text = "NO SHOCK";
		}

		float actualTorque = torque * torqueShockFactor;

		rb.AddTorque (actualTorque);

		text2.text = "AngularVelocity: " 	+ rb.angularVelocity;
		text3.text = "Torgue: " 			+ torque;				
	}

	private void Accelerate (float accelerationInput) {
		
		float accAmount = accelerationInput * acceleration;
		float accAngle = (transform.rotation.eulerAngles.z / 360) * Mathf.PI * 2;

		Vector3 force = new Vector3 (
			Mathf.Cos (accAngle) * accAmount,
			Mathf.Sin (accAngle) * accAmount,
			0
		);

		rb.AddForce (force);
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.relativeVelocity.magnitude > shockTriggerVelocity) {
			shockTimeStamp = Time.time;
			shockPower = collision.relativeVelocity.magnitude * shockFactor;

			text1.text = "SHOCK, POWER: " + shockPower;
		}
	}

	private void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "exit") {
			UnityEngine.SceneManagement.Scene scene = 
				UnityEngine.SceneManagement.SceneManager.GetActiveScene ();
			
			UnityEngine.SceneManagement.SceneManager.LoadScene(scene.name);
		} // else if (other.tag == "gold") { ... }			
	}
}
