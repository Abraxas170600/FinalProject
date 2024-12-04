using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoubleJump : PlayerPower
{
    private bool isActive;
    private PlayerController playerController;

    private int maxJumps = 2;
    private int jumpsRemaining;
    public override void Activate(bool State, PlayerController playerController)
    {
        isActive = State;
        this.playerController = playerController;
    }
    public void DoubleJump()
    {
        if (!isActive) return;
        else
        {
            //if (!playerController.IsFastFallingPressed) return;
            //else 
        }
    }
}
