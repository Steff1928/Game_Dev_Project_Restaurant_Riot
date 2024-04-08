using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject controlsPanel;
    [SerializeField] GameObject creditsPanel;

    // Start is called before the first frame update
    void Start()
    {
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
        controlsPanel.SetActive(false);
    }
}
