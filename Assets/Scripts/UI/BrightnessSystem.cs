using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrightnessSystem : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Image panelBrightness;

    private const string BRIGHTNESS_KEY = "brightness";
    private const float DEFAULT_BRIGHTNESS = 0.7f;

    private void Start()
    {
        InitializeBrightness();
    }

    public void InitializeBrightness()
    {
        slider.value = PlayerPrefs.GetFloat(BRIGHTNESS_KEY, DEFAULT_BRIGHTNESS);
        UpdateBrightness(slider.value);
    }

    public void UpdateBrightness(float value)
    {
        float invertedValue = 0.9f - value; 
        
        PlayerPrefs.SetFloat(BRIGHTNESS_KEY, value);
        panelBrightness.color = new Color(panelBrightness.color.r, panelBrightness.color.g, panelBrightness.color.b, invertedValue);
    }
}
