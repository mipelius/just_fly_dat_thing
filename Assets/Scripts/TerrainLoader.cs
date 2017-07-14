using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainLoader : MonoBehaviour {

	public GameObject blockPrefab;

	private int[,] map = new int[, ] {
		{1, 1, 1, 1, 1, 1, 1, 1, 1},
		{1, 0, 0, 0, 0, 0, 0, 0, 0},
		{1, 0, 0, 0, 0, 0, 0, 0, 0},
		{1, 0, 0, 0, 0, 0, 0, 0, 0},
		{1, 0, 0, 0, 0, 0, 0, 0, 0},
		{1, 0, 0, 0, 0, 0, 0, 0, 0},
		{1, 0, 0, 0, 0, 0, 0, 0, 0},
		{1, 0, 0, 0, 0, 0, 0, 0, 0},
		{1, 0, 0, 0, 0, 0, 0, 0, 0},
		{1, 0, 0, 0, 0, 0, 0, 0, 0},
		{1, 1, 1, 1, 1, 1, 1, 1, 1},
		{1, 1, 1, 1, 1, 1, 1, 1, 1},
		{1, 0, 0, 0, 0, 0, 0, 0, 0},
		{1, 0, 0, 0, 0, 0, 0, 0, 0},
		{1, 0, 0, 0, 0, 0, 0, 0, 0},
		{1, 0, 0, 0, 0, 0, 0, 0, 0},
		{1, 0, 0, 0, 0, 0, 0, 0, 0},
		{1, 0, 0, 0, 0, 0, 0, 0, 0},
		{1, 0, 0, 0, 0, 0, 0, 0, 0},
		{1, 0, 0, 0, 0, 0, 0, 0, 0},
		{1, 0, 0, 0, 0, 0, 0, 0, 0},
		{1, 1, 1, 1, 1, 1, 1, 1, 1}
	};

	// Use this for initialization
	void Start () {
		int h = map.GetUpperBound (0) + 1;
		int w = map.GetUpperBound (1) + 1;

		Debug.Log ("W:" + w + ",H:" + h);

		for (int x = 0; x < 200; x++) {
			for (int y = 0; y < 200; y++) {			
				if (Random.value < 0.5 ) {
					GameObject currentBlock = 
						Instantiate<GameObject> (
							blockPrefab, 
							new Vector3 (x, y, 0), 
							Quaternion.identity
						);


					BoxCollider2D collider = currentBlock.GetComponent<BoxCollider2D>();

					currentBlock.transform.parent = this.transform;
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
