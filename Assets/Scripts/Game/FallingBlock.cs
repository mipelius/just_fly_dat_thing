using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour {

	public AudioClip fallingBlockExplosionAudio;
	public AudioClip playerDamageAudio;

	public GameObject fallingBlockExplosion;

	public float damage;

	private Vector3 originalPosition;
	private Quaternion originalRotation;

	private Rigidbody2D rb;

	private float restartDelay = 0.5f;

	private bool hasCollided;

	private SpriteRenderer spriteRenderer;
	private Collider2D collider;

	void Start () {
		originalPosition = transform.position;
		originalRotation = transform.rotation;
		rb = GetComponent<Rigidbody2D> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
		collider = GetComponent<Collider2D> ();

		hasCollided = false;
	}

	void Explode() {		
		GameObject explosion = Instantiate (fallingBlockExplosion, transform.position, Quaternion.identity);
		explosion.transform.localScale *= spriteRenderer.size.x * 3;
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "fallingBlock" || hasCollided) {
			return;
		}

		hasCollided = true;

		if (collision.gameObject.tag == "Player") {
			collision.gameObject.SendMessage ("ApplyDamage", damage);
			AudioManager.instance.PlaySingle (playerDamageAudio);
		}

		GameObject camera = GameObject.Find("Main Camera");
		Vector2 distanceVector = new Vector2 (
			                         transform.position.x - camera.transform.position.x,
			                         transform.position.y - camera.transform.position.y
		                         );
		if (distanceVector.magnitude < 20) {
			AudioManager.instance.PlaySingle (fallingBlockExplosionAudio);
		}

		Explode ();
		spriteRenderer.enabled = false;
		collider.enabled = false;

		StartCoroutine ("Restart");
	}

	IEnumerator Restart() {
		yield return new WaitForSeconds(restartDelay);

		transform.position = originalPosition;
		transform.rotation = originalRotation;
		rb.velocity = new Vector2(0, 0);
		rb.angularVelocity = 0;

		spriteRenderer.enabled = true;
		collider.enabled = true;

		hasCollided = false;
	}
}
