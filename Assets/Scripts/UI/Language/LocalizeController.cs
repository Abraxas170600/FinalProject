using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizeController : MonoBehaviour
{
    public static LocalizeController Instance { get; private set; }
    [SerializeField] private LanguageDataScriptable languageData;
    [SerializeField] private SystemLanguage currentLanguage;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetCurrentLanguage(SystemLanguage language)
    {
        currentLanguage = language;
        UpdateAllLocalizedTexts();
    }

    public void SetLanguageSpanish()
    {
        SetCurrentLanguage(SystemLanguage.Spanish);
    }

    public void SetLanguageEnglish()
    {
        SetCurrentLanguage(SystemLanguage.English);
    }

    public string GetTextLocalize(string key)
    {
        var textLocalized = languageData.GetTextFromKey(key, currentLanguage);
        
        return textLocalized;
    }

    public void UpdateAllLocalizedTexts()
    {
        var localizedTextEntities = FindObjectsOfType<LocalizedTextEntity>();
        foreach (var entity in localizedTextEntities)
        {
            entity.SetText();
        }
    }
}
