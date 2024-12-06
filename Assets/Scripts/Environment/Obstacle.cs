using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public void PlayerDetected(bool detected)
    {
        if (!detected) return;
        else
        {
            Player player = GetComponent<TriggerDetector>().LastElementDetected.GetComponent<Player>();
            player.InstaKill();
        }
    }
}
