using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public void Respawn()
    {
        Vector3 respawnPosition = GameManager.Instance.GetLastCheckpoint();
        transform.position = respawnPosition;
        Debug.Log("Jugador reapareció en: " + respawnPosition);
    }

    // acto de la muricion
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("Jugador murió.");
            Respawn();
        }
    }
}
