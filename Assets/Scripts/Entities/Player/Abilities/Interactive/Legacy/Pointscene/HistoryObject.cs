using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryObject : MonoBehaviour
{
    [SerializeField] private GameObject historyText;
    public void PlayerDetected(bool detected)
    {
        historyText.SetActive(detected);
    }
}