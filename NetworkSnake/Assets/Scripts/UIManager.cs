using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider slider;
    public Slider slider2;
    public Text gridSizeText;
    public Text maxPlayersNumber;
    
    public void Awake()
    {
        PlayerPrefs.SetString("gridSize", gridSizeText.text);
        PlayerPrefs.SetString("maxPlayersNumber", maxPlayersNumber.text);
    }

    public void SliderControl()
    {
        gridSizeText.text = (Mathf.RoundToInt(slider.value * 40) + 10).ToString();
        PlayerPrefs.SetString("gridSize", gridSizeText.text);
    }
    public void MaxPlayersSliderControl()
    {
        maxPlayersNumber.text = (Mathf.RoundToInt(slider2.value * 3) + 1).ToString();
        PlayerPrefs.SetString("maxPlayersNumber", maxPlayersNumber.text);
    }
    
}
