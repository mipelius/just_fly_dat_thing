using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public float rotationVelocity;
	public float accelerationForce;
	public float damageThreshold;
	public float damageFactor;

	public int bombs;

	public float health;

	public GameObject bomb;

	private Rigidbody2D rb;
	private PolygonCollider2D polygonCollider;

	bool isColliding = false;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		polygonCollider = GetComponent<PolygonCollider2D> ();
			
		UILevelManager.instance.SetPlayer (this);
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

	void Update() {
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (bombs > 0) {
				DropBomb ();
				bombs--;
			}
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
		float accAngle = (transform.rotation.eulerAngles.z / 360) * Mathf.PI * 2;

		Vector2 force = new Vector2 (
			               Mathf.Cos (accAngle) * accAmount,
			               Mathf.Sin (accAngle) * accAmount			               
		               );
	
		rb.AddForce (force);
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
		//rotation.eulerAngles += new Vector3 (0, 0, 180);

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
			Destroy (collision.collider.gameObject);
			return;
		}

		float collisionMagnitude = collision.relativeVelocity.magnitude;

		if (collisionMagnitude > damageThreshold) {
			health -= damageFactor * collisionMagnitude;
			if (health <= 0) {
				UILevelManager.instance.Restart ();
			}
		}			
	}

	void OnCollisionStay2D(Collision2D collision) {
		isColliding = true;
	}

	void OnCollisionExit2D(Collision2D collision) {
		isColliding = false;
	}
}
