using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDInterfaceUI : MonoBehaviour
{
    [SerializeField] private GameObject lifeContainer;
    [SerializeField] private GameObject lifePointPrefab;
    private List<GameObject> lifePoints = new();
    public void ChangeLifeAmount(int amount)
    {
        while (lifePoints.Count < amount)
        {
            GameObject newLifePoint = Instantiate(lifePointPrefab, lifeContainer.transform);
            lifePoints.Add(newLifePoint);
        }
        while (lifePoints.Count > amount)
        {
            GameObject lastLifePoint = lifePoints[lifePoints.Count - 1];
            lifePoints.RemoveAt(lifePoints.Count - 1);
            Destroy(lastLifePoint);
        }
    }
}
