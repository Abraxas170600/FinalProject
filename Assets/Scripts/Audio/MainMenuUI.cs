using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.PlayMusic(EnumSounds.Sound_Menu);
    }
}
