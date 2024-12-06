using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableLocalizationData", menuName = "ScriptableObjects/New ScriptableLanguage", order = 1)]

public class LanguageDataScriptable : ScriptableObject
{
    [SerializeField] private List<LanguageModel> languageModelList = new List<LanguageModel>();

    public string GetTextFromKey(string key, SystemLanguage language)
    {
        LanguageModel languageModel = languageModelList.Find(x => x.key == key);
        if (languageModel != null)
        {
            LanguageKeyModel localizedLanguage = languageModel.languages.Find(x => x.language == language);
            return localizedLanguage != null ? localizedLanguage.localizedText : "Translation not found";
        }
        return "Key not found";
    }
    
}

[System.Serializable]
public class LanguageModel
{
    public string key;
    public List<LanguageKeyModel> languages = new List<LanguageKeyModel>();
}

[System.Serializable]
public class LanguageKeyModel
{
    public SystemLanguage language;
    public string localizedText;
}
