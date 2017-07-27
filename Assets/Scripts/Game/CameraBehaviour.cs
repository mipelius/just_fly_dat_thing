using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

	public GameObject playerToFollow;

	public float zoomInSize;
	public float zoomOutSize;

	public float zoomingRate;

	private bool zoomingIn = true;

	private Camera camera;

	void Awake() {
		camera = GetComponent<Camera> ();
	}

	// Update is called once per frame
	void Update () {
		if (playerToFollow != null) {
			transform.position = new Vector3 (
				playerToFollow.transform.position.x,
				playerToFollow.transform.position.y,
				-10
			);
		}

		if (Input.GetKeyDown (KeyCode.Tab)) {
			zoomingIn = !zoomingIn;
		}

		HandleZooming ();
	}

	private void HandleZooming() {
		if (zoomingIn) {
			if (camera.orthographicSize	> zoomInSize) {
				camera.orthographicSize -= zoomingRate * Time.deltaTime;
			}
			if (camera.orthographicSize < zoomInSize) {
				camera.orthographicSize = zoomInSize;
			}
		} else {
			if (camera.orthographicSize	< zoomOutSize) {
				camera.orthographicSize += zoomingRate * Time.deltaTime;
			}
			if (camera.orthographicSize > zoomOutSize) {
				camera.orthographicSize = zoomOutSize;
			}			
		}
	}
}
