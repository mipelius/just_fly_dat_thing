using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour {

	private Vector3 originalPosition;
	private Quaternion originalRotation;

	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		originalPosition = transform.position;
		originalRotation = transform.rotation;
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "wall") {
			transform.position = originalPosition;
			transform.rotation = originalRotation;
			rb.velocity = new Vector2(0, 0);
			rb.angularVelocity = 0;
		}
	}
}
