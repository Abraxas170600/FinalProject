using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerStorage : MonoBehaviour
{
    private PlayerPower[] playerPowers;
    public PlayerPower[] PlayerPowers { get => playerPowers; set => playerPowers = value; }

    public void Initialize()
    {
        PlayerPowers = GetComponents<PlayerPower>();
    }
}
