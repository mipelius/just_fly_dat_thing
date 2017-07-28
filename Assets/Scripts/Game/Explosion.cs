using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {
	public AudioClip explosionAudioClip;

	void Start () {
		AudioManager.instance.PlaySingle (explosionAudioClip);

		ParticleSystem exp = GetComponent<ParticleSystem>();
		exp.Play();
		Destroy(gameObject, exp.main.duration);	
	}
}
