using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataUI : MonoBehaviour
{
    [SerializeField] private float timeToDesactivated;
    [SerializeField] private GameObject savePanel;
    public void SavePanelActive()
    {
        StartCoroutine(SaveRoutine());
    }
    private IEnumerator SaveRoutine()
    {
        savePanel.SetActive(true);
        yield return new WaitForSeconds(timeToDesactivated);
        savePanel.SetActive(false);
    }
}
