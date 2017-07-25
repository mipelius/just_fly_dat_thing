﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public float rotationVelocity;
	public float accelerationForce;
	public float damageThreshold;
	public float damageFactor;

	public float customDrag;

	public float velocityLimit;

	public int bombs;

	public float health;

	public GameObject bomb;

	public GameObject explosion;

	private Rigidbody2D rb;
	private PolygonCollider2D polygonCollider;
	private SpriteRenderer spriteRenderer;

	private bool isAlive = true;

	private bool isColliding = false;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		polygonCollider = GetComponent<PolygonCollider2D> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();

		UILevelManager.instance.SetPlayer (this);
	}

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

		//LimitVelocity ();

		AddCustomDrag (customDrag);
	}

	void AddCustomDrag (float drag) {
		rb.AddForce (-rb.velocity.normalized * drag * rb.velocity.sqrMagnitude * rb.velocity.sqrMagnitude);
	}

	void LimitVelocity() {
		float overSpeed = rb.velocity.magnitude - velocityLimit;

		if (overSpeed > 0) {			
			rb.AddForce (-rb.velocity.normalized * accelerationForce);
		}
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (bombs > 0) {
				DropBomb ();
				bombs--;
			}
		}

		if (health <= 0 && isAlive) {
			isAlive = false;

			spriteRenderer.enabled = false;
			rb.simulated = false;

			GameObject exp = Instantiate (explosion, transform.position, Quaternion.identity);
			exp.transform.localScale *= 8;

			StartCoroutine ("Die");
		}
	}

	private void Rotate (float rotationInput) {
		if (rotationInput == 0 && isColliding) {			
			rb.freezeRotation = false;
		}
		else {
			rb.freezeRotation = true;
			Vector3 rotation = new Vector3 (0, 0, -rotationInput) * rotationVelocity * Time.deltaTime;
			transform.Rotate (rotation);
		}			
	}

	private void Accelerate (float accelerationInput) {
		float accAmount = accelerationInput * accelerationForce;

		Vector2 force = NormalizedDirectionVector() * accAmount;

		rb.AddForce (force);
	}

	private Vector2 NormalizedDirectionVector() {
		float angle = (transform.rotation.eulerAngles.z / 360) * Mathf.PI * 2;

		return new Vector2 (
			Mathf.Cos (angle),
			Mathf.Sin (angle)
		);
	}

	private void DropBomb() {
		float angle = (rb.rotation + 180) / 360 * 2 * Mathf.PI;

		Vector2 transition = new Vector2 (
			Mathf.Cos (angle),
			Mathf.Sin (angle)
		);

		transition *= 1.2f;

		this.polygonCollider.enabled = false;
		RaycastHit2D hit = Physics2D.Linecast (rb.position, rb.position + transition);
		this.polygonCollider.enabled = true;

		if (hit.transform != null) {
			rb.position = rb.position - transition / 3;
		}

		Vector2 position = rb.position + transition * 1f;

		Quaternion rotation = transform.rotation;

		GameObject currentBomb = Instantiate (
			                         bomb, 
			                         new Vector3 (position.x, position.y, -2), 
									 rotation
		                         );

		Rigidbody2D bombRb = currentBomb.GetComponent<Rigidbody2D> ();

		if (bombRb != null) {
			bombRb.velocity = rb.velocity + transition * 2;
			bombRb.AddForce (transition * 2000 + new Vector2 (0, -2000));			
		}		
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.tag == "bombbox") {
			bombs++;
			//collision.collider.enabled = false;
			Destroy (collision.gameObject);
			return;
		}
			
		float collisionMagnitude = collision.relativeVelocity.magnitude;

		if (collisionMagnitude > damageThreshold) {
			health -= damageFactor * collisionMagnitude;
			if (health <= 0) {
				health = 0;
			}
		}			
	}

	void OnCollisionStay2D(Collision2D collision) {
		isColliding = true;
	}

	void OnCollisionExit2D(Collision2D collision) {
		isColliding = false;
	}

	IEnumerator Die() {
		yield return new WaitForSeconds(2.2f);

		UILevelManager.instance.Restart ();
	}
}