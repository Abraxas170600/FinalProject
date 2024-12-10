using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    private bool isPlayerNearby = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = true;
            Debug.Log("Jugador cerca del bonfire. Presiona 'F' para activar.");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F))
        {
            ActivateCheckpoint();
        }
    }

    private void ActivateCheckpoint()
    {
        GameManager.Instance.UpdateCheckpoint(transform.position);
        Debug.Log("Checkpoint activado en el bonfire.");
    }
}

