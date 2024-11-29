using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoubleJump : PlayerPower
{
    private bool isActive;

    private int maxJumps = 2;
    private int jumpsRemaining;
    public override void Activate(bool State)
    {
        isActive = State;
    }
    public void DoubleJump(PlayerController playerController)
    {
        if (!isActive) return;
        else
        {
            //if (!playerController.IsFastFallingPressed) return;
            //else 
        }
    }
}
