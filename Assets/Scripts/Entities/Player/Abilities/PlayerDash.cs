using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerDash : PlayerPower
{
    private bool isActive;
    private PlayerController playerController;

    private float dashingVelocity = 23f;
    private float dashingTime = 0.23f;
    private float waitDashingTime;
    private bool isDashing;
    private bool canDash = true;

    public bool IsDashing { get => isDashing; set => isDashing = value; }
    public override void Activate(bool State, PlayerController playerController)
    {
        isActive = State;
        this.playerController = playerController;
    }

    public void Dash(bool isGrounded, bool isWalled)
    {
        if (!isActive) return;
        else
        {
            DashUpdate(isGrounded, isWalled);

            if (!playerController.IsDashingPressed) return;
            else DashActive(isWalled);
        }
    }
    public void DashActive(bool isWalled)
    {
        playerController.IsDashingPressed = false;

        if (waitDashingTime >= dashingTime * 2.2f && playerController.InputVelocity.x != 0 && canDash)
        {
            waitDashingTime = 0;
            canDash = false;
            IsDashing = true;

            float previousGravity = playerController.Gravity;
            float horizontalVelocity = playerController.CurrentVelocity.x;

            playerController.Gravity = 0;

            if (!isWalled) playerController.CurrentVelocity = new Vector2(horizontalVelocity > 0 ? dashingVelocity : -dashingVelocity, 0f);
            else playerController.CurrentVelocity = new Vector2(horizontalVelocity < 0 ? dashingVelocity : -dashingVelocity, 0f);

            StartCoroutine(StopDash(previousGravity));
        }
    }
    private void DashUpdate(bool isGrounded, bool isWalled)
    {
        if (waitDashingTime < 3f) waitDashingTime += Time.deltaTime;
        if ((isGrounded || isWalled) && !IsDashing) canDash = true;
    }

    private IEnumerator StopDash(float previousGravity)
    {
        yield return new WaitForSeconds(dashingTime);

        IsDashing = false;
        playerController.Gravity = previousGravity;
    }
}
