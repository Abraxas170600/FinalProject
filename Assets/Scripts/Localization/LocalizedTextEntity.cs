using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LocalizedTextEntity : MonoBehaviour
{
    [SerializeField] private List<TextKeyReference> texts;
    //public LocalizeController localizeController;
 
    /*[ContextMenu("GetLocalization")]
    public string GetLocalization()
    {
        //var textLocalized = LocalizeController.Instance.GetTextLocalize(key);
        var textLocalized = localizeController.GetTextLocalize(texts.key);
        Debug.Log("Localized Entity" + textLocalized + " " + key);
        return textLocalized;
    }*/

    [ContextMenu("SetText")]
    public void SetText()
    {
        //textPro.text = GetLocalization();
        foreach (var textKeyReference in texts)
        {
            string localizedText = LocalizeController.Instance.GetTextLocalize(textKeyReference.key);
            foreach (var textReference in textKeyReference.textsReference)
            {
                textReference.textPro.text = localizedText;
            }
        }
    }
}

[System.Serializable]
public class TextKeyReference
{
    public string key;
    public List<TextReference> textsReference;
}

[System.Serializable]
public class TextReference
{
    public TMP_Text textPro;
}
