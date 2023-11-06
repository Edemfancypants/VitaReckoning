using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class SFXLogic : MonoBehaviour {

	public static SFXLogic Instance;

	private void Awake()
	{
		Instance = this;
	}

	public AudioSource[] soundEffects;
	public AudioSource BGM;

	public void PlaySFX(int sfxToPlay)
	{
		soundEffects[sfxToPlay].Stop();
		soundEffects[sfxToPlay].Play();
	}

	public void PlaySFXPitched(int sfxToPlay)
	{
		soundEffects[sfxToPlay].pitch = Random.Range(.8f, 1.2f);

		PlaySFX(sfxToPlay);
	}

	public void PlayBGM(bool Paused)
	{
		if (Paused == false)
		{
			BGM.Pause();
		}
		else
		{
			BGM.UnPause();
		}
	}
}
