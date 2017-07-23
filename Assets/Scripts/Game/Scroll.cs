using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour {

	public float speed = 0.5f;

	private MeshRenderer meshRenderer;

	void Start () {
		meshRenderer = GetComponent<MeshRenderer> ();
//		transform.localScale = new Vector3 (
//			Screen.currentResolution.height,
//			Screen.currentResolution.width,
//			0
//		);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 cameraPosition = Camera.main.transform.position;
		Vector2 cameraPosition2D = new Vector2(cameraPosition.x, cameraPosition.y);

		Vector2 offset = cameraPosition2D * speed;

		meshRenderer.material.mainTextureOffset = offset;
	}
}
