using SojaExiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float timeRemaining = 60f;

    public int customersFed = 0;
    public int customersToFeed = 50;

    MouseLook mouseLookScript;
    FireProjectiles fireProjectilesScript;

    GameObject player;
    Camera playerCam;

    [SerializeField] Canvas objectiveInfo;
    [SerializeField] Canvas hud;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;

        mouseLookScript = FindAnyObjectByType<MouseLook>();
        mouseLookScript.enabled = false;

        fireProjectilesScript = FindAnyObjectByType<FireProjectiles>();
        fireProjectilesScript.enabled = false;

        player = GameObject.FindGameObjectWithTag("Player"); // Find the player active within the hierachy
        playerCam = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Camera>(); // Find the camera attached to the player as a child component
    }

    // Update is called once per frame
    void Update()
    {
        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0) 
        {
            StopGame();
        }

        if (customersFed == customersToFeed) 
        {
            StopGame();
        }
    }
    public void BeginGame()
    {
        Debug.Log("BUTTON CLICKED");
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen and hide it once game has begun
        mouseLookScript.enabled = true;
        fireProjectilesScript.enabled = true;
        objectiveInfo.enabled = false;
        hud.enabled = true;
        Time.timeScale = 1;

    }

    public void StopGame()
    {
        // Disable both interactivity scripts so players can't move
        player.GetComponent<PlayerMovement>().enabled = false;
        playerCam.GetComponent<MouseLook>().enabled = false;
        //playerCamera.transform.LookAt(transform.position); // Look at the enemy to show that the player has been caught

        // TEMP: Restart
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
