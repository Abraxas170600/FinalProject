using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private TMP_Text tutorialText;
    [SerializeField] private GameObject tutorialObject;
    public void ChangeTutorialSkillText(string text)
    {
        tutorialText.text = text;
        tutorialObject.SetActive(true);

        Invoke(nameof(DesactiveTutorialObject), 5f);
    }
    private void DesactiveTutorialObject()
    {
        tutorialObject.SetActive(false);
    }
}
