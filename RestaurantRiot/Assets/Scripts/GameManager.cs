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

    GameObject player;
    Camera playerCam;

    // Start is called before the first frame update
    void Start()
    {
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
