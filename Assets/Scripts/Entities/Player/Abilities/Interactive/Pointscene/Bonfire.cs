using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : MonoBehaviour
{
    private bool isPlayerNearby = false;
    public KeyCode interactKey = KeyCode.F;

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(interactKey))
        {
            InteractWithBonfire();
        }
    }

    private void InteractWithBonfire()
    {
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            player.FullHealth();
            Debug.Log("Player interacted with the bonfire!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            Debug.Log("Player is near the bonfire.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            Debug.Log("Player left the bonfire.");
        }
    }
}

