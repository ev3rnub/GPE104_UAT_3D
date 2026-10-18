using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsGUI : MonoBehaviour
{

    public AudioMixer mainAudioMixer;
    public Slider mainVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;

    //define some strings for playerprefs and to save the sliders values. 
    private const string MasterKey = "MasterVolume";
    private const string MusicKey = "MusicVolume";
    private const string SfxKey = "SFXVolume";


    private void Start()
    {
        if (mainAudioMixer == null ||
            mainVolumeSlider == null ||
            musicVolumeSlider == null ||
            sfxVolumeSlider == null)
        {
            Debug.LogError(
                "OptionsGUI: Assign the Audio Mixer and all three Sliders in the Inspector.",
                this
            );
            enabled = true;
            return;
        }

        mainVolumeSlider.value = PlayerPrefs.GetFloat(MasterKey, 1f);
        musicVolumeSlider.value = PlayerPrefs.GetFloat(MusicKey, 1f);
        sfxVolumeSlider.value = PlayerPrefs.GetFloat(SfxKey, 1f);

        OnMainVolumeChange();
        OnMusicVolumeChange();
        OnSFXVolumeChange();
    }


    public void OnMainVolumeChange()
    {
        PlayerPrefs.SetFloat(MasterKey, mainVolumeSlider.value);
        PlayerPrefs.Save();

        float newVolume = mainVolumeSlider.value;
        if (newVolume <= 0)
        {
            newVolume = -80;
        }
        else
        {
            newVolume = Mathf.Log10(newVolume);
            newVolume = newVolume * 20;  
        }
        mainAudioMixer.SetFloat("MasterVolume", newVolume);
    }

    public void OnMusicVolumeChange()
    {
        PlayerPrefs.SetFloat(MusicKey, musicVolumeSlider.value);
        PlayerPrefs.Save();
        float newVolume = musicVolumeSlider.value;
        if (newVolume <= 0)
        {
            newVolume = -80;
        }
        else
        {
            newVolume = Mathf.Log10(newVolume);
            newVolume = newVolume * 20;  
        }
        mainAudioMixer.SetFloat("MusicVolume", newVolume);
    }

    public void OnSFXVolumeChange()
    {
        PlayerPrefs.SetFloat(SfxKey, sfxVolumeSlider.value);
        PlayerPrefs.Save();
        
        float newVolume = sfxVolumeSlider.value;
        if (newVolume <= 0)
        {
            newVolume = -80;
        }
        else
        {
            newVolume = Mathf.Log10(newVolume);
            newVolume = newVolume * 20;  
        }
        mainAudioMixer.SetFloat("SFXVolume", newVolume);
    }
}
