using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableAudio", menuName = "ScriptableObjects/New ScriptableAudio", order = 1)]

public class ScriptableAudio : ScriptableObject
{
    public SoundData[] claseAudioObj;
}

public enum EnumSounds
{
    Sound_Menu,
    Sound_Gameplay,
    Sound_Gameover,
    Sfx_Crash
}

[System.Serializable]
public class SoundData
{
    public AudioClip soundClip;
    public EnumSounds soundKey;
}
