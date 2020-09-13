using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider slider;
    public Text gridSizeText;

    public void Awake()
    {
        PlayerPrefs.SetString("gridSize", gridSizeText.text);
    }

    public void SliderControl()
    {
        gridSizeText.text = (Mathf.RoundToInt(slider.value * 40) + 5).ToString();
        PlayerPrefs.SetString("gridSize", gridSizeText.text);
    }
}
