﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

	public float explosionRange;
	public float explosionForce;

	public float activationDelay;

	private float awakeTimeStamp;

	private Rigidbody2D rb;

	void Awake () {
		awakeTimeStamp = Time.time;
		rb = GetComponent<Rigidbody2D> ();
	}

	void Update() {
		Vector2 direction = rb.velocity.normalized;

		float targetAngle = Mathf.Acos (direction.x) / (2 * Mathf.PI) * 360 + 180;

		float rotationSpeed = 100f;
		float angle = Mathf.MoveTowardsAngle(rb.rotation, targetAngle, rotationSpeed * Time.deltaTime);

		rb.rotation = angle;
	}

	public void Explode () {
		if (!enabled)
			return;

		enabled = false;

		Vector2 pos = transform.position;
		Collider2D[] colliders = Physics2D.OverlapCircleAll (pos, explosionRange);

		foreach (Collider2D collider in colliders) {
			Rigidbody2D rb = collider.gameObject.GetComponent<Rigidbody2D> ();

			if (rb != null) {
				Vector2 direction = rb.position - pos;
				float rangeFactor = (explosionRange - direction.magnitude) / explosionRange;
				direction.Normalize ();
				rb.AddForce (explosionForce * rangeFactor * direction);
			}

			if (collider.tag == "explosive") {
				Bomb otherBomb = collider.GetComponent<Bomb> ();
				if (otherBomb != null) {
					otherBomb.Explode ();
				}
			}
		}

		Destroy (this.gameObject);
	}

	void OnCollisionStay2D(Collision2D collision) {
		if (collision.collider.tag == "explosive")
			return;
		
		if (Time.time - awakeTimeStamp > activationDelay) {
			Explode ();
		}
	}
}
