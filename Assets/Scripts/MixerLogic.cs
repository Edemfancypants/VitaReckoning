using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MixerLogic : MonoBehaviour {

    [SerializeField] private AudioMixer mainMixer;
    public Slider masterVolumeSlider, musicVolumeSlider, sfxVolumeSlider;
    private float masterVolume, musicVolume, sfxVolume;

    public void Start()
    {
        if (PlayerPrefs.HasKey("Master") == false)
        {
            PlayerPrefs.SetFloat("Master", 1);
            PlayerPrefs.SetFloat("Music", 1);
            PlayerPrefs.SetFloat("SFX", 1);
            LoadVolume();
        }
        else
        {
            LoadVolume();
        }
    }

    public void SetVolume()
    {
        mainMixer.SetFloat("Master", Mathf.Log10(masterVolumeSlider.value) * 20);
        masterVolume = masterVolumeSlider.value;

        mainMixer.SetFloat("Music", Mathf.Log10(musicVolumeSlider.value) * 20);
        musicVolume = musicVolumeSlider.value;

        mainMixer.SetFloat("SFX", Mathf.Log10(sfxVolumeSlider.value) * 20);
        sfxVolume = sfxVolumeSlider.value;
    }

    public void LoadVolume()
    {
        masterVolumeSlider.value = PlayerPrefs.GetFloat("Master");
        musicVolumeSlider.value = PlayerPrefs.GetFloat("Music");
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFX");
    }

    public void SaveVolume()
    {
        PlayerPrefs.SetFloat("Master", masterVolume);
        PlayerPrefs.SetFloat("Music", musicVolume);
        PlayerPrefs.SetFloat("SFX", sfxVolume);
    }
}