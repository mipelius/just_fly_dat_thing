using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
	
	public AudioSource musicSource;

	public static AudioManager instance = null;

	private int countOfEfxPlayers = 12;

	private List<AudioSource> audioSources;

	private int currentAudioSource = 0;

	void Awake () {		
		if (instance == null) {
			instance = this;
			CreateAudioSources ();
		} else if (instance != this)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);
	}

	public void PlaySingle (AudioClip clip, float volume = 1.0f)
	{
		currentAudioSource++;
		if (currentAudioSource >= audioSources.Count) {
			currentAudioSource = 0;
		}

		AudioSource efxSource = audioSources [currentAudioSource];

		efxSource.volume = volume;

		efxSource.clip = clip;
		efxSource.Play ();
	}

	private void CreateAudioSources() {
		audioSources = new List<AudioSource> ();

		for (int i = 0; i < countOfEfxPlayers; i++) {			
			AudioSource audioSource = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;

			audioSource.enabled = true;
			audioSource.loop = false;
			audioSource.playOnAwake = false;
			audioSource.volume = 1.0f;

			audioSources.Add (audioSource);
		}
	}
}

