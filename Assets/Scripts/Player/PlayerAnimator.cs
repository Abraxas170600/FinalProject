using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _playerAnimator;
    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private static readonly int IsJumping = Animator.StringToHash("isJumping");
    private static readonly int IsGrounded = Animator.StringToHash("isGrounded");
    private static readonly int IsDashing = Animator.StringToHash("isDashing");
    private static readonly int OnWall = Animator.StringToHash("onWall");
    private static readonly int IsFastFalling = Animator.StringToHash("isFastFalling");
    public void SetIsWalking(bool isWalking) => _playerAnimator.SetBool(IsWalking, isWalking);
    public void SetIsJumping(bool isJumping) => _playerAnimator.SetBool(IsJumping, isJumping);
    public void SetIsGrounded(bool isGrounded) => _playerAnimator.SetBool(IsGrounded, isGrounded);
    public void SetIsDashing(bool isDashing) => _playerAnimator.SetBool(IsDashing, isDashing);
    public void SetOnWall(bool onWall) => _playerAnimator.SetBool(OnWall, onWall);
    public void SetIsFastFalling(bool isFastFalling) => _playerAnimator.SetBool(IsFastFalling, isFastFalling);
}
