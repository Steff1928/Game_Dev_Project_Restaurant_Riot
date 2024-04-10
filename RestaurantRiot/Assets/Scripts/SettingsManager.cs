using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    // This value is static and public because it needs to be accessed from another script in a different scene
    public static float mouseSensitivity = 1f;

    static int qualityIndex;

    static bool isFullscreen = true;

    [SerializeField] Slider sensitivitySlider;
    [SerializeField] TextMeshProUGUI sensitivityValue;
    [SerializeField] TMP_Dropdown graphicsDropdown;
    [SerializeField] Toggle fullscreenToggle;

    private void Start()
    {
        qualityIndex = QualitySettings.GetQualityLevel();
    }

    private void Update()
    {
        sensitivitySlider.value = mouseSensitivity;

        float roundedSensitivity = Mathf.Round(mouseSensitivity * 100);
        sensitivityValue.text = roundedSensitivity.ToString();

        graphicsDropdown.value = qualityIndex;
        graphicsDropdown.RefreshShownValue();

        fullscreenToggle.isOn = isFullscreen;
    }

    public void MouseSensitivityChange(float sensitivityValue)
    {
        mouseSensitivity = sensitivityValue;
    }

    public void SetQuality(int qualityIndexValue)
    {
        qualityIndex = qualityIndexValue;
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool isFullscreenValue)
    {
        isFullscreen = isFullscreenValue;
        Screen.fullScreen = isFullscreen;
    }
}
