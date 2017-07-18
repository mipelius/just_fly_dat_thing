using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour {

	public GameObject playerToInstantiate;

	void Awake () {
		GameObject player = Instantiate (playerToInstantiate);

		player.transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + 90);

		float scale = transform.lossyScale.y / 20;
		float angle = (transform.eulerAngles.z + 90 )/ 360 * 2 * Mathf.PI;

		Vector3 transmission = new Vector3(
			Mathf.Cos(angle) * scale,
			Mathf.Sin(angle) * scale,
			0
		);

		player.transform.position = transform.position + transmission;

		GameObject camera = GameObject.Find ("Main Camera");
		CameraBehaviour cameraBehaviour = camera.GetComponent<CameraBehaviour>();
		cameraBehaviour.playerToFollow = player;
	}
}
