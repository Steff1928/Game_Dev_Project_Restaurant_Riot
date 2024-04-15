using SojaExiles;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

// Contains general gameplay logic, ranging from UI navigation to determining objectives
public class GameManager : MonoBehaviour
{
    public float timeRemaining = 120f; // Store the time remaining as a float variable 

    [HideInInspector] public int customersFed = 0; // Store the customers fed as a public variable so it can be accessed else where
    public int customersToFeed = 50; // Number of customers to feed

    // Bools determining the current game state (public for gameOver and gameWon for wider access)
    bool gameIsPaused = false;
    [HideInInspector] public bool gameOver = false;
    [HideInInspector] public bool gameWon = false;

    // String array determing possible causes for how the player could have lost the game
    [HideInInspector] public string[] gameOverCauses = { "You were caught", "You ran out of time" };
    [HideInInspector] public int gameOverCausesIndex; // Public variable to select cause

    // Script references
    MouseLook mouseLookScript;
    FireProjectiles fireProjectilesScript;

    // Player and main camera references
    GameObject player;
    Camera playerCam;

    // UI Screen references
    [SerializeField] GameObject objectiveInfo;
    [SerializeField] GameObject hud;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject gameWonMenu;
    [SerializeField] GameObject optionsMenu;

    // Start is called before the first frame update
    void Start()
    {
        // Determine which screen should show on startup
        objectiveInfo.SetActive(true);
        hud.SetActive(false);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        gameWonMenu.SetActive(false);
        optionsMenu.SetActive(false);

        // Remove any cursor lockstate and set the timeScale to 0 to initially stop the game from running
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;

        // Find the MouseLook and FireProjectiles scripts anywhere in the hierachy and disable them
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
        // Countdown from the initial time remaining as long as the game has not been won or lost
        if (!gameOver && !gameWon)
        {
            timeRemaining -= Time.deltaTime;
        }

        // If timeRemaining reaches 0 and the game has not already been won or lost, trigger a game over state
        if (timeRemaining <= 0 && !gameWon && !gameOver) 
        {
            gameOver = true;
            gameOverCausesIndex = 1;
            StopGame();
        }

        // If the player successfully feeds the required amount of customers, trigger a game won state
        if (customersFed == customersToFeed) 
        {
            gameWon = true;
            StopGame();
        }

        // If the Escape key is pressed, the Objective Info screen is not
        // displaying and the game has not already been won or lost, run following code
        if (Input.GetKeyDown(KeyCode.Escape) && objectiveInfo.activeSelf == false && !gameOver && !gameWon) 
        {
            optionsMenu.SetActive(false); // Ensure the Options Menu is not displaying
            // Toggle the Pause Menu based on whether it is active or not
            if (!gameIsPaused) 
            { 
                PauseGame();
            } 
            else
            {
                ResumeGame();
            }
        }
    }

    // Move to the next scene in the build index when run
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Move to the previous scene in the build index when run
    public void ReturnToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    // Set a bunch of option back to true in which they were intially set to false upon start up so
    // the game can properly start(hiding the Objective Info Screen)
    public void BeginGame()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen and hide it once game has begun
        mouseLookScript.enabled = true;
        fireProjectilesScript.enabled = true;
        objectiveInfo.SetActive(false);
        hud.SetActive(true);
        Time.timeScale = 1;
    }

    // Essentialy invert the "BeginGame" function so the player can browse the Pause Menu without the game
    // progressing once the game is paused
    void PauseGame()
    {
        gameIsPaused = true;

        Cursor.lockState = CursorLockMode.None;
        mouseLookScript.enabled = false; 
        fireProjectilesScript.enabled = false;
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    // Show the Options Menu when called
    public void ShowOptionsMenu()
    {
        optionsMenu.SetActive(true);
    }

    // Hide the Options Menu when called
    public void HideOptionsMenu()
    {
        optionsMenu.SetActive(false);
    }

    // Invert the "PauseGame" function so the player can resume playing once the game is no longer paused
    public void ResumeGame()
    {
        gameIsPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        mouseLookScript.enabled = true;
        fireProjectilesScript.enabled = true;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    // Reload the currently active scene when called
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // When called, prevent the player from moving or looking around while the game process the outcome
    public void StopGame()
    {
        // Disable both interactivity scripts so players can't move
        player.GetComponent<PlayerMovement>().enabled = false;
        playerCam.GetComponent<MouseLook>().enabled = false;

        // If gameOver, start a coroutine responsible for managing a Game Over state
        if (gameOver)
        {
            StartCoroutine(AwaitGameOver());
        } 
        else if (gameWon) // If gameWon, display the Game Won menu and await a coroutine responsible for managing a Game Won state
        {
            gameWonMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            StartCoroutine(AwaitGameWon());
        }
    }

    // Exit the application when called (only applicable once the project is built)
    public void ExitGame()
    {
        Debug.Log("Game Closed");
        Application.Quit();
    }

    // After some time, display the game over menu while triggering some options which prevent the game from progressing
    IEnumerator AwaitGameOver()
    {
        yield return new WaitForSeconds(1);
        gameOverMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        mouseLookScript.enabled = false;
        fireProjectilesScript.enabled = false;
        hud.SetActive(false);
        Time.timeScale = 0;
    }

    // After some time, prevent the game from progressing when the Game Won Menu is fully show
    // (it is animated through transition)
    IEnumerator AwaitGameWon()
    {
        yield return new WaitForSeconds(1);
        mouseLookScript.enabled = false;
        fireProjectilesScript.enabled = false;
        hud.SetActive(false);
        Time.timeScale = 0;
    }
}
