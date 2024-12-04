using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJump : PlayerPower
{
    private bool isActive;
    private PlayerController playerController;

    private bool isWallSliding;
    private float wallSlidingSpeed = 2f;
    private bool isWallJumping;
    private bool _wasJumpingPressed;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.18f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.22f;
    private Vector2 wallJumpingPower = new (5f, 18f);
    public bool IsWallJumping { get => isWallJumping; set => isWallJumping = value; }
    public bool IsWallSliding { get => isWallSliding; set => isWallSliding = value; }

    public override void Activate(bool State, PlayerController playerController)
    {
        isActive = State;
        this.playerController = playerController;
    }

    public void WallJump(bool isJumpingPressed, CapsuleCollider2D capsuleCollider2D, float heightCheckDistance, LayerMask groundLayerMask, bool isGrounded)
    {
        if (!isActive) return;
        else
        {
            WallJumpActive(isJumpingPressed);
            WallSlide(capsuleCollider2D, heightCheckDistance, groundLayerMask, isGrounded);
        }
    }
    private void WallJumpActive(bool isJumpingPressed)
    {
        if (IsWallSliding)
        {
            IsWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
            wallJumpingCounter -= Time.deltaTime;

        // Cambio en esta parte: Solo permitir el salto si el botón se acaba de presionar
        if (isJumpingPressed && wallJumpingCounter > 0f && !_wasJumpingPressed)
        {
            _wasJumpingPressed = true; // Marcar que el botón se ha presionado
            IsWallJumping = true;
            playerController.CurrentVelocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                playerController.IsFacingRight = !playerController.IsFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
        else if (!isJumpingPressed)
        {
            _wasJumpingPressed = false; // Restablecer el estado del botón
        }
    }
    private void WallSlide(CapsuleCollider2D capsuleCollider2D, float heightCheckDistance, LayerMask groundLayerMask, bool isGrounded)
    {
        if (IsWalled(capsuleCollider2D, heightCheckDistance, groundLayerMask) && !isGrounded && playerController.CurrentVelocity.x != 0)
        {
            IsWallSliding = true;
            playerController.CurrentVelocity = new Vector2(playerController.CurrentVelocity.x, Mathf.Clamp(playerController.CurrentVelocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
            IsWallSliding = false;
    }
    private void StopWallJumping() => IsWallJumping = false;
    public bool IsWalled(CapsuleCollider2D capsuleCollider2D, float heightCheckDistance, LayerMask groundLayerMask)
    {
        Vector3 raycastOrigin = capsuleCollider2D.bounds.center; // Cambia a la posición del jugador
        Vector2 newRaycastOrigin = transform.localScale.x > 0 ? new(raycastOrigin.x + 0.4f, raycastOrigin.y) : new(raycastOrigin.x - 0.4f, raycastOrigin.y);

        Vector2 raycastDirection = transform.localScale.x > 0 ? Vector2.right : Vector2.left; // Dirección del rayo según la escala del jugador

        RaycastHit2D raycastHit2D = Physics2D.Raycast(newRaycastOrigin, raycastDirection, heightCheckDistance, groundLayerMask);

        Color rayColor = raycastHit2D.collider != null ? Color.green : Color.red;

        Debug.DrawRay(newRaycastOrigin, raycastDirection * (heightCheckDistance), rayColor, groundLayerMask);
        return raycastHit2D.collider != null;
    }
}
