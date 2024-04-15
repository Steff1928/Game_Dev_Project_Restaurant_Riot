using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Contains logic specifically for the Settings Menu
public class SettingsManager : MonoBehaviour
{
    // Static variables which are accessed across both scenes
    public static float mouseSensitivity = 1f;
    static int qualityIndex;
    static bool isFullScreen = true;

    // Reference UI objects that will be assigned within the inspector
    [SerializeField] Slider sensitivitySlider;
    [SerializeField] TextMeshProUGUI sensitivityValue;
    [SerializeField] TMP_Dropdown graphicsDropdown;
    [SerializeField] Toggle fullscreenToggle;

    private void Start()
    {
        qualityIndex = QualitySettings.GetQualityLevel(); // Set qualityIndex to the current Quality Level Unity is using
    }

    private void Update()
    {
        sensitivitySlider.value = mouseSensitivity; // Assign the value of mouseSensitivity to the slider

        // Round mouseSensitivity and multiply it by 100 to give an exagerrated effect in-game
        float roundedSensitivity = Mathf.Round(mouseSensitivity * 100);
        sensitivityValue.text = roundedSensitivity.ToString(); // Assign the rounded sensitivity value to the associated text box

        graphicsDropdown.value = qualityIndex; // Assign the associated dropdown box to the value of qualityIndex
        graphicsDropdown.RefreshShownValue();

        fullscreenToggle.isOn = isFullScreen; // Assign the value of isFullScreen to an associated toggle
    }

    // Update the value of the Mouse Sensitivity slider accordingly
    // This function is called each time the value of the slider is changed
    public void MouseSensitivityChange(float sensitivityValue)
    {
        mouseSensitivity = sensitivityValue; 
    }

    // Update the value of qualityIndex each time this function is called
    // This function is called each time the value of the dropdown is changed
    public void SetQuality(int qualityIndexValue)
    {
        qualityIndex = qualityIndexValue;
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    // Update the value of isFullScreen each time this function is called
    // This function is called each time the value of the toggle is changed
    public void SetFullScreen(bool isFullScreenValue)
    {
        isFullScreen = isFullScreenValue;
        Screen.fullScreen = isFullScreen;
    }
}
