using SojaExiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Contains UI logic, specifically regarding the Main Menu
public class MainMenuUI : MonoBehaviour
{
    // Reference UI objects that will be assigned within the inspector
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject controlsPanel;
    [SerializeField] GameObject creditsPanel;
    [SerializeField] GameObject optionsPanel;

    // Start is called before the first frame update
    void Start()
    {
        BackToMenu(); // Run "BackToMenu" on startup
    }

    // Disable any panels apart from mainMenuPanel
    public void BackToMenu()
    {
        mainMenuPanel.SetActive(true);
        creditsPanel.SetActive(false);
        controlsPanel.SetActive(false);
        optionsPanel.SetActive(false);
    }

    // Enable controlsPanel and disable mainMenuPanel when called
    public void SwitchToControls()
    {
        controlsPanel.SetActive(true);
        mainMenuPanel.SetActive(false); 
    }

    // Enable creditsPanel and disable mainMenuPanel when called
    public void SwitchToCredits()
    {
        creditsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    // Enable optionsPanel and disable mainMenuPanel when called
    public void SwitchToOptions()
    {
        optionsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }
}
