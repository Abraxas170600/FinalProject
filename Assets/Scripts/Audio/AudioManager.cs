using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] ScriptableAudio scriptableAudio;
    [SerializeField] AudioMixer myMixer;

    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(EnumSounds sonidoName)
    {
        foreach(SoundData soundData in scriptableAudio.claseAudioObj)
        {
            if (soundData.soundKey == sonidoName)
            {
                musicSource.Stop();
                musicSource.clip = soundData.soundClip;
                musicSource.Play();
            }
        }
    }

    public void PlaySFX(EnumSounds nameSFX)
    {
        foreach (SoundData soundData in scriptableAudio.claseAudioObj)
        {
            if (soundData.soundKey == nameSFX)
            {
                sfxSource.PlayOneShot(soundData.soundClip);
            }
        }
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }
    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void MusicVolume(float volume)
    {
        //musicSource.volume = volume;
        myMixer.SetFloat("music", Mathf.Log10(volume) * 20);
    }

    public void SFXVolume(float volume)
    {
        //sfxSource.volume = volume;
        myMixer.SetFloat("sfx", Mathf.Log10(volume) * 20);
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
    public void StopSFX()
    {
        sfxSource.Stop();
    } 
}
