using SojaExiles;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    // This value is static and public because it needs to be accessed from another script in a different scene
    public static float mouseSensitivity = 1f;

    [SerializeField] Slider sensitivitySlider;
    [SerializeField] TextMeshProUGUI sensitivityValue;

    private void Start()
    {
        Debug.Log(mouseSensitivity);
    }

    private void Update()
    {
        sensitivitySlider.value = mouseSensitivity;

        float roundedSensitivity = Mathf.Round(mouseSensitivity * 100);
        sensitivityValue.text = roundedSensitivity.ToString();
    }

    public void MouseSensitivityChange(float sensitivityValue)
    {
        mouseSensitivity = sensitivityValue;
        Debug.Log(mouseSensitivity);
    }
}
