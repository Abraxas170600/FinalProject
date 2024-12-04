using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBash : PlayerPower
{
    private bool isActive;
    private PlayerController playerController;

    [Header("Bash")]
    [SerializeField] private float radius;
    [SerializeField] private float bashPower;
    [SerializeField] private float bashTime;
    [SerializeField] private GameObject arrow;
    private GameObject bashableObject;

    private Vector3 bashDirection;
    private float bashTimeRemaining;

    private bool nearToBashAbleObj;
    private bool isChoosingDirection;

    private void Start()
    {
        bashTimeRemaining = bashTime;
    }
    public override void Activate(bool State, PlayerController playerController)
    {
        isActive = State;
        this.playerController = playerController;
    }
    public void Bash()
    {
        if (!isActive) return;

        CheckForBashableObjects();

        if (nearToBashAbleObj == true)
        {
            HighlightBashableObject(true);

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                EnterBashState();
            }
            else if (isChoosingDirection && Input.GetKeyUp(KeyCode.Mouse1))
            {
                PerformBash();
            }
        }
        else if (bashableObject != null)
        {
            HighlightBashableObject(false);
        }

        if (playerController.IsBashingPressed)
        {
            ContinueBash();
        }
    }

    private void CheckForBashableObjects()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, radius, Vector2.zero);

        nearToBashAbleObj = false;

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag("BashAble"))
            {
                nearToBashAbleObj = true;
                bashableObject = hit.collider.transform.gameObject;
                break;
            }
        }
    }

    private void HighlightBashableObject(bool highlight)
    {
        bashableObject.GetComponent<SpriteRenderer>().color = highlight ? Color.yellow : Color.white;
    }

    private void EnterBashState()
    {
        Time.timeScale = 0;
        bashableObject.transform.localScale = Vector2.one * 1.4f;
        arrow.SetActive(true);
        arrow.transform.position = bashableObject.transform.position;
        isChoosingDirection = true;
    }

    private void PerformBash()
    {
        Time.timeScale = 1f;
        bashableObject.transform.localScale = Vector2.one;
        isChoosingDirection = false;
        playerController.IsBashingPressed = true;

        playerController.CurrentVelocity = Vector2.zero;
        playerController.Gravity = 0;

        //transform.position = bashableObject.transform.position;

        bashDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        bashDirection.z = 0;

        //if (bashDirection.x > 0)
        //    transform.eulerAngles = new Vector3(0, 180, 0);
        //else
        //    transform.eulerAngles = Vector3.zero;

        bashDirection = bashDirection.normalized;
        bashableObject.GetComponent<Rigidbody2D>().AddForce(-bashDirection * 50, ForceMode2D.Impulse);

        arrow.SetActive(false);
    }

    private void ContinueBash()
    {
        if (bashTimeRemaining > 0)
        {
            bashTimeRemaining -= Time.unscaledDeltaTime;
            playerController.CurrentVelocity = bashDirection * bashPower * Time.deltaTime;
        }
        else
        {
            EndBash();
        }
    }

    private void EndBash()
    {
        playerController.IsBashingPressed = false;
        bashTimeRemaining = bashTime;
        playerController.Gravity = (-2f * playerController.maxJumpHeight) / Mathf.Pow(playerController.maxJumpTine / 2f, 2);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
