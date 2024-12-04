using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISoundController : MonoBehaviour
{
    public Slider musicSlider, sfxSlider;

    void Start()
    {
        musicSlider.minValue = 0.0001f;    
        musicSlider.maxValue = 1;    
        //musicSlider.value = 0.5f;

        sfxSlider.minValue = 0.0001f;
        sfxSlider.maxValue = 1;
        //sfxSlider.value = 0.5f;
    }

    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
    }

    public void ToggleSFX()
    {
        AudioManager.Instance.ToggleSFX();
    }

    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(musicSlider.value);
    }

    public void SFXVolume()
    {
        AudioManager.Instance.SFXVolume(sfxSlider.value);
    }
}
