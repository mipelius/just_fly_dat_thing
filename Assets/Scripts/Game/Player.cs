using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public AudioClip collisionClip;
	public AudioClip softcollisionClip;
	public AudioClip bombDrop;

	public AudioSource accelerationAudioSource;

	public GameObject rocketFireParticleSystem1;
	public GameObject rocketFireParticleSystem2;

	public float rotationVelocity;
	public float accelerationForce;
	public float damageThreshold;
	public float damageFactor;

	public float customDrag;

	public int bombs;

	public float health;

	public GameObject bomb;

	public GameObject explosion;

	public GameObject bloodBurst;

	private Rigidbody2D rb;
	private PolygonCollider2D polygonCollider;
	private SpriteRenderer spriteRenderer;

	private bool isAlive = true;

	private bool isAccelerating = false;
	private bool stoppedAccelerating = true;

	private bool isColliding = false;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		polygonCollider = GetComponent<PolygonCollider2D> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();

		UILevelManager.instance.SetPlayer (this);
	}

	void FixedUpdate  () {
		if (UpdatesDisabled ()) {
			return;
		}

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
		AddCustomDrag (customDrag);
	}

	void AddCustomDrag (float drag) {
		rb.AddForce (-rb.velocity.normalized * drag * rb.velocity.sqrMagnitude * rb.velocity.sqrMagnitude);
	}

	void Update() {
		if (UpdatesDisabled ()) {
			return;
		}

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

			rocketFireParticleSystem1.SetActive (false);
			rocketFireParticleSystem2.SetActive (false);

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
		ParticleSystem system1 = rocketFireParticleSystem1.GetComponent<ParticleSystem> ();
		ParticleSystem system2 = rocketFireParticleSystem2.GetComponent<ParticleSystem> ();

		if (accelerationInput == 0) {
			system1.Stop ();
			system2.Stop ();

			accelerationAudioSource.Stop ();

			stoppedAccelerating = true;

		} else {
			if (stoppedAccelerating) {
				system1.Play ();
				system2.Play ();

				accelerationAudioSource.Play ();

				stoppedAccelerating = false;
			}
		}

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
		AudioManager.instance.PlaySingle (bombDrop);

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
		if (collision.gameObject.tag == "bombbox") {
			bombs++;
			Destroy (collision.gameObject);
			return;
		}

		if (collision.gameObject.tag == "fallingBlock") {
			return;
		}

		float collisionMagnitude = collision.relativeVelocity.magnitude;

		if (collisionMagnitude > damageThreshold) {
			AudioManager.instance.PlaySingle (
				collisionClip, 
				Mathf.Min(
					0.1f + (collisionMagnitude - damageThreshold) / 20, 
					1.0f
				)
			);

			ApplyDamage (damageFactor * collisionMagnitude);
		} else {
			AudioManager.instance.PlaySingle (softcollisionClip, 0.1f);
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

	public void ApplyDamage(float amount) {
		health -= amount;

		GameObject currentBloodBurst = Instantiate (bloodBurst, transform.position, Quaternion.identity);
		currentBloodBurst.transform.localScale *= 1 + (amount / 20);

		if (health <= 0) {
			health = 0;
		}
	}

	private bool UpdatesDisabled() {		
		if (Time.timeScale == 0 || !isAlive) {
			accelerationAudioSource.Stop ();
			return true;
		} else {
			return false;
		}
	}
}
