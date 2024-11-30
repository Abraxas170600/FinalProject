using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBash : PlayerPower
{
    private bool isActive;

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
    private bool isBashing;

    public bool IsBashing { get => isBashing; set => isBashing = value; }

    private void Start()
    {
        bashTimeRemaining = bashTime;
    }
    public override void Activate(bool state)
    {
        isActive = state;
    }
    public void Bash(PlayerController playerController)
    {
        //if (/*!isActive || */isBashing) return;

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
                PerformBash(playerController);
            }
        }
        else if (bashableObject != null)
        {
            HighlightBashableObject(false);
        }

        if (isBashing)
        {
            ContinueBash(playerController);
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

    private void PerformBash(PlayerController playerController)
    {
        Time.timeScale = 1f;
        bashableObject.transform.localScale = Vector2.one;
        isChoosingDirection = false;
        isBashing = true;

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

    private void ContinueBash(PlayerController playerController)
    {
        if (bashTimeRemaining > 0)
        {
            bashTimeRemaining -= Time.unscaledDeltaTime;
            playerController.CurrentVelocity = bashDirection * bashPower * Time.deltaTime;
        }
        else
        {
            EndBash(playerController);
        }
    }

    private void EndBash(PlayerController playerController)
    {
        isBashing = false;
        bashTimeRemaining = bashTime;
        playerController.Gravity = (-2f * playerController.maxJumpHeight) / Mathf.Pow(playerController.maxJumpTine / 2f, 2);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
