using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour {
	
	public GameObject fallingBlockExplosion;

	public float pushForce;

	private Vector3 originalPosition;
	private Quaternion originalRotation;

	private Rigidbody2D rb;

	private float restartDelay = 0.5f;

	private bool hasCollided = false;

	private SpriteRenderer spriteRenderer;

	void Start () {
		originalPosition = transform.position;
		originalRotation = transform.rotation;
		rb = GetComponent<Rigidbody2D> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}


	void Explode() {		
		GameObject explosion = Instantiate (fallingBlockExplosion, transform.position, Quaternion.identity);
		explosion.transform.localScale *= spriteRenderer.size.x * 3;
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "fallingBlock" || hasCollided) {
			return;
		}

		Explode ();
		spriteRenderer.enabled = false;
		StartCoroutine ("Restart");
	}

	IEnumerator Restart() {
		yield return new WaitForSeconds(restartDelay);

		transform.position = originalPosition;
		transform.rotation = originalRotation;
		rb.velocity = new Vector2(0, 0);
		rb.angularVelocity = 0;

		spriteRenderer.enabled = true;
	}
}
