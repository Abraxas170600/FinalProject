using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private float speed;
    [SerializeField] private float fallingDownMultiplier = 2.0f;
    public float maxJumpHeight = 2.5f;
    public float maxJumpTine = 1.0f;
    [SerializeField] private LayerMask groundLayerMask;

    [Header("Dependences")]
    private Rigidbody2D rb;
    private CapsuleCollider2D capsuleCollider2D;
    private PlayerAnimator playerAnimator;
    private PlayerPowerStorage playerPowerStorage;
    private TriggerDetector triggerDetector;

    [Header("Powers")]
    private PlayerDash playerDash;
    private PlayerWallJump playerWallJump;
    private PlayerFastFall playerFastFall;
    private PlayerBash playerBash;

    [Header("Events")]
    [SerializeField] private UnityEvent<int> particleEvents;

    private PlayerState _playerState;

    private PlayerSkills _playerSkills;
    private PowerObject _powerObject;

    private Vector2 _inputVelocity;
    private Vector2 _currentVelocity;
    private float _movementInput;
    private float _heightCheckDistance = 0.05f;

    private bool _isJumpingPressed;
    private bool isDashingPressed;
    private bool isFastFallingPressed;
    private bool isFacingRight;

    private float _coyoteTime = 0.06f;
    private float _coyoteTimeCounter;
    private float jumpForce => (maxJumpHeight) / (maxJumpTine / 2f);
    private float gravity;

    public Vector2 InputVelocity { get => _inputVelocity; set => _inputVelocity = value; }
    public Vector2 CurrentVelocity { get => _currentVelocity; set => _currentVelocity = value; }
    public bool IsDashingPressed { get => isDashingPressed; set => isDashingPressed = value; }
    public bool IsFastFallingPressed { get => isFastFallingPressed; set => isFastFallingPressed = value; }
    public bool IsFacingRight { get => isFacingRight; set => isFacingRight = value; }
    public float Gravity { get => gravity; set => gravity = value; }

    private void Awake()
    {
        Initialize();
    }
    public void UpdateController()
    {
        NormalActions();
        SpecialActions();
        Physics();
        Graphics();
    }
    private void FixedUpdate()
    {
        Vector2 position = rb.position;
        position += _currentVelocity * Time.fixedDeltaTime;
        //if (playerBash.IsBashing == false)
        //{
            rb.MovePosition(position);
        //}
        if (_currentVelocity.y > 0) ResetJump();
    }

    #region Interface Methods
    public void Initialize()
    {
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        playerAnimator = GetComponent<PlayerAnimator>();
        playerPowerStorage = GetComponent<PlayerPowerStorage>();
        triggerDetector = GetComponent<TriggerDetector>();

        playerDash = GetComponent<PlayerDash>();
        playerWallJump = GetComponent<PlayerWallJump>();
        playerFastFall = GetComponent<PlayerFastFall>();
        playerBash = GetComponent<PlayerBash>();

        playerPowerStorage.Initialize();
        _playerSkills = new PlayerSkills();
        Gravity = (-2f * maxJumpHeight) / Mathf.Pow(maxJumpTine / 2f, 2);
    }
    public void NormalActions()
    {
        HorizontalMovement();
        TryApplyJump();
        UpdatePlayerState();
    }
    public void SpecialActions()
    {
        playerDash.Dash(this, IsGrounded(), playerWallJump.IsWalled(capsuleCollider2D, _heightCheckDistance, groundLayerMask));
        playerWallJump.WallJump(_isJumpingPressed, this, capsuleCollider2D, _heightCheckDistance, groundLayerMask, IsGrounded());
        playerFastFall.FastFall(this, IsGrounded(), particleEvents);
        playerBash.Bash(this);
    }
    public void Physics()
    {
        ApplyGravity();
        CoyoteTime();
    }
    public void Graphics()
    {
        AnimationState();
        Flip();
    }
    #endregion

    #region Normal Actions
    private void HorizontalMovement()
    {
        if (!playerDash.IsDashing && !playerWallJump.IsWallJumping)
        {
            float horizontalVelocity = Mathf.MoveTowards(_currentVelocity.x, _inputVelocity.x * speed, speed);
            _currentVelocity.x = horizontalVelocity;
        }
    }
    private void TryApplyJump()
    {
        if (IsGrounded()) _currentVelocity.y = Mathf.Max(_currentVelocity.y, 0);
        if (_inputVelocity.y > 0 && _coyoteTimeCounter > 0f)
        {
            _currentVelocity.y = jumpForce;
            particleEvents.Invoke(0);
        }
    }
    private void UpdatePlayerState()
    {
        _playerState = !IsGrounded() ? _playerState = PlayerState.isJumping : _playerState = PlayerState.isGrounded;
    }
    #endregion

    #region Physics
    private void ApplyGravity()
    {
        if (!IsGrounded() && Gravity != 0)
        {
            bool isFalling = _currentVelocity.y < 0.0f;
            float gravityMultiplier = isFalling ? fallingDownMultiplier : 1f;
            float jumpForceMultiplier = _isJumpingPressed ? 2f : 1f;
            _currentVelocity.y += Gravity * Time.deltaTime * gravityMultiplier / jumpForceMultiplier;
            _currentVelocity.y = Mathf.Max(_currentVelocity.y, Gravity / 2);
        }
    }
    private void CoyoteTime()
    {
        if (IsGrounded()) _coyoteTimeCounter = _coyoteTime;
        else if (!IsGrounded() && _coyoteTimeCounter > 0) _coyoteTimeCounter -= Time.deltaTime;
    }
    #endregion

    #region Graphics
    public void AnimationState()
    {
        playerAnimator.SetIsGrounded(IsGrounded());
        playerAnimator.SetIsWalking(_playerState == PlayerState.isGrounded && _currentVelocity.x != 0);
        playerAnimator.SetIsJumping(_playerState == PlayerState.isJumping);
        playerAnimator.SetIsDashing(playerDash.IsDashing || playerBash.IsBashing);
        playerAnimator.SetOnWall(playerWallJump.IsWalled(capsuleCollider2D, _heightCheckDistance, groundLayerMask) && playerWallJump.IsWallSliding);
        playerAnimator.SetIsFastFalling(IsFastFallingPressed && playerFastFall.IsFastFalling && !IsGrounded());
    }
    private void Flip()
    {
        if (!playerWallJump.IsWallJumping)
        {
            if (isFacingRight && _currentVelocity.x > 0f || !isFacingRight && _currentVelocity.x < 0f)
            {
                IsFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }
        }
    }
    #endregion

    #region Power Methods
    private void PowerActiveChecker()
    {
        if (_powerObject == null) return;
        else
        {
            _powerObject.PlayerSkillActive(_playerSkills);
            playerPowerStorage.PlayerPowers[(int)_powerObject.GetSkillType()].Activate(CanUsePower(_powerObject));
        }
    }
    private bool CanUsePower(PowerObject powerObject)
    {
        return _playerSkills.IsSkillUnlocked(powerObject.GetSkillType());
    }
    public void PowerDetected(bool detected)
    {
        if (!detected) return;
        else
        {
            _powerObject = triggerDetector.LastElementDetected.GetComponent<PowerObject>();
        }
    }
    #endregion

    #region Help Methods
    private void ResetJump() => _inputVelocity = new Vector2(_inputVelocity.x, 0f);
    private bool IsGrounded()
    {
        Vector3 raycastOrigin = capsuleCollider2D.bounds.min;
        Vector2 newRaycastOrigin = new(raycastOrigin.x + 0.35f, raycastOrigin.y);

        RaycastHit2D raycastHit2D = Physics2D.Raycast(newRaycastOrigin, Vector2.down, _heightCheckDistance, groundLayerMask);

        Color rayColor;

        if (raycastHit2D.collider != null) rayColor = Color.green;
        else rayColor = Color.red;

        Debug.DrawRay(newRaycastOrigin, Vector3.down * (_heightCheckDistance), rayColor, groundLayerMask);
        return raycastHit2D.collider != null;
    }
    #endregion

    #region Input Methods
    public void OnMovement(InputAction.CallbackContext value)
    {
        if (!value.performed) return;

        _movementInput = value.ReadValue<float>();
        _inputVelocity = new Vector2(_movementInput, _inputVelocity.y);
    }
    public void OnJumping(InputAction.CallbackContext value)
    {
        if (!value.performed)
        {
            _isJumpingPressed = false;
            _inputVelocity = new Vector2(_inputVelocity.x, 0f);
            return;
        }

        if (!playerDash.IsDashing)
        {
            _isJumpingPressed = true;
            _inputVelocity = new Vector2(_inputVelocity.x, 1f);
        }
    }
    public void OnDashing(InputAction.CallbackContext value)
    {
        if (value.started) IsDashingPressed = true;
    }
    public void OnInteract(InputAction.CallbackContext value)
    {
        if (value.started) PowerActiveChecker();
    }
    public void OnFastFalling(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            if (playerWallJump.IsWallSliding || CurrentVelocity.y == 0f) return;
            else IsFastFallingPressed = true;
        }
    }
    #endregion
}
