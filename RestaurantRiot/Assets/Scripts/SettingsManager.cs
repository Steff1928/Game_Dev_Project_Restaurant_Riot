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
    static int resolutionIndex;

    static bool isFullscreen = true;

    [SerializeField] Slider sensitivitySlider;
    [SerializeField] TextMeshProUGUI sensitivityValue;
    [SerializeField] TMP_Dropdown graphicsDropdown;
    [SerializeField] TMP_Dropdown resolutionDropdown;
    [SerializeField] Toggle fullscreenToggle;

    Resolution[] resolutions;

    private void Start()
    {
        qualityIndex = QualitySettings.GetQualityLevel();

        resolutions = resolutions = Screen.resolutions.Where(resolution => resolution.refreshRateRatio.value == Screen.currentResolution.refreshRateRatio.value).ToArray();
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolution = 0;
        for (int i = 0; i < resolutions.Length; i++) 
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolution = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolution;
        resolutionDropdown.RefreshShownValue();
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

    public void SetResolution(int resolutionIndexValue)
    {
        resolutionIndex = resolutionIndexValue;
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
