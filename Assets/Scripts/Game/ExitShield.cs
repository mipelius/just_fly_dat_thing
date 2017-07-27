using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitShield : MonoBehaviour {	
	public float damageAmount;

	void OnTriggerStay2D(Collider2D collider) {		
		if (collider.tag == "Player") {
			collider.gameObject.SendMessage ("ApplyDamage", damageAmount * Time.deltaTime);
		}
	}
}
