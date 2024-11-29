using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerFastFall : PlayerPower
{
    private bool isActive;
    private bool isFastFalling;
    private float FastFallVelocity = 30f;

    [SerializeField] private CameraShake cameraShake;
    [SerializeField] private float shakeIntensity = default;
    [SerializeField] private float shakeTime = default;

    public bool IsFastFalling { get => isFastFalling; set => isFastFalling = value; }

    public override void Activate(bool State)
    {
        isActive = State;
    }
    public void FastFall(PlayerController playerController, bool isGrounded, UnityEvent<int> particleEvent)
    {
        if (!isActive) return;
        else
        {
            if (!playerController.IsFastFallingPressed) return;
            else FastFallActive(playerController, isGrounded, particleEvent);
        }
    }
    private void FastFallActive(PlayerController playerController, bool isGrounded, UnityEvent<int> particleEvent)
    {
        if (!isGrounded)
        {
            IsFastFalling = true;
            if (IsFastFalling) playerController.CurrentVelocity = new Vector2(0, -FastFallVelocity);
        }
        else
        {
            IsFastFalling = false;
            playerController.IsFastFallingPressed = false;
            particleEvent.Invoke(3);
            cameraShake.ShakeCamera(shakeIntensity, shakeTime);
        }
    }
}
