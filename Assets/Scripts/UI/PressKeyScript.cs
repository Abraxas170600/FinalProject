using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressKeyScript : MonoBehaviour
{
    [SerializeField] private GameObject pressKeyPanel;
    [SerializeField] private GameObject mainMenuPanel;

    void Start()
    {
        pressKeyPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.anyKeyDown || Input.GetMouseButtonDown(0))
        {
            pressKeyPanel.SetActive(false);
            mainMenuPanel.SetActive(true);
        }
    }
}
