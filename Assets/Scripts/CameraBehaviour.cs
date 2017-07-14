using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

	public GameObject playerToFollow;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.position = new Vector3 (
			playerToFollow.transform.position.x,
			playerToFollow.transform.position.y,
			-10
		);
	}
}
