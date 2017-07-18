using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour {

	public float angularVelocity = 100;

	private Rigidbody2D rb;

	void Awake() {
		rb = GetComponent<Rigidbody2D> ();
	}

	void Start () {
		GoldManager.instance.AddGold (this);
	}

	void FixedUpdate () {
		rb.angularVelocity = angularVelocity;
	}
				
	private void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Player") {
			Destroy (gameObject);
		}			
	}

	private void OnDestroy() {
		GoldManager.instance.RemoveGold (this);
	}

}
