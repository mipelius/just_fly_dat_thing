using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour {
	
	public GameObject fallingBlockExplosion;

	public float pushForce;

	private Vector3 originalPosition;
	private Quaternion originalRotation;

	private Rigidbody2D rb;


	void Start () {
		originalPosition = transform.position;
		originalRotation = transform.rotation;
		rb = GetComponent<Rigidbody2D> ();
		Explode ();
	}


	void Explode() {
		Instantiate (fallingBlockExplosion, this.transform.position, Quaternion.identity);

		transform.position = originalPosition;
		transform.rotation = originalRotation;
		rb.velocity = new Vector2(0, 0);
		rb.angularVelocity = 0;
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.collider.gameObject.tag == "Player") {
			collision.rigidbody.AddForce (rb.velocity.normalized * pushForce);
		}
		Explode ();
	}
}
