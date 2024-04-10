using SojaExiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject controlsPanel;
    [SerializeField] GameObject creditsPanel;
    [SerializeField] GameObject optionsPanel;

    MouseLook mouseLook;

    // Start is called before the first frame update
    void Start()
    {
        mouseLook = FindAnyObjectByType<MouseLook>();

        BackToMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackToMenu()
    {
        mainMenuPanel.SetActive(true);
        creditsPanel.SetActive(false);
        controlsPanel.SetActive(false);
        optionsPanel.SetActive(false);
    }

    public void SwitchToControls()
    {
        controlsPanel.SetActive(true);
        mainMenuPanel.SetActive(false); 
    }

    public void SwitchToCredits()
    {
        creditsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void SwitchToOptions()
    {
        optionsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }
}
