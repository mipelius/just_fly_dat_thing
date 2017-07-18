using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public float rotationVelocity;
	public float accelerationForce;
	public float damageThreshold;

	private Rigidbody2D rb;

	private float shockTimeStamp = 0;
	private float shockPower = 0;

	private bool isColliding = false;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void FixedUpdate  () {
		bool inputLeft = Input.GetKey (KeyCode.LeftArrow);
		bool inputRight = Input.GetKey (KeyCode.RightArrow);

		float rotationInput = 0;

		if (inputLeft)
			rotationInput -= 1;
		if (inputRight)
			rotationInput += 1;
		
		Rotate (rotationInput);

		float accelerationInput = Input.GetKey(KeyCode.UpArrow) ? 1 : 0;

		Accelerate (accelerationInput);
	}

	private void Rotate (float rotationInput) {
		if (rotationInput == 0) {
			rb.angularVelocity = 0;
		} else if (Mathf.Abs(rb.angularVelocity) < rotationVelocity) {
			rb.angularVelocity = -rotationInput * rotationVelocity;
		}
	}

	private void Accelerate (float accelerationInput) {
		float accAmount = accelerationInput * accelerationForce;
		float accAngle = (transform.rotation.eulerAngles.z / 360) * Mathf.PI * 2;

		Vector2 force = new Vector2 (
			               Mathf.Cos (accAngle) * accAmount,
			               Mathf.Sin (accAngle) * accAmount			               
		               );
	
		rb.AddForce (force);
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.relativeVelocity.magnitude > damageThreshold) {
			// damage
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
