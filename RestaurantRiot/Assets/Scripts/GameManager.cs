using SojaExiles;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float timeRemaining = 60f;

    [HideInInspector] public int customersFed = 0;
    public int customersToFeed = 50;

    bool gameIsPaused = false;
    [HideInInspector] public bool gameOver = false;
    [HideInInspector] public bool gameWon = false;

    [HideInInspector] public string[] gameOverCauses = { "You were caught", "You ran out of time" };
    [HideInInspector] public int gameOverCausesIndex;

    MouseLook mouseLookScript;
    FireProjectiles fireProjectilesScript;

    GameObject player;
    Camera playerCam;

    [SerializeField] Canvas objectiveInfo;
    [SerializeField] Canvas hud;
    [SerializeField] Canvas pauseMenu;
    [SerializeField] Canvas gameOverMenu;
    [SerializeField] GameObject gameWonMenu;
    [SerializeField] GameObject optionsMenu;

    // Start is called before the first frame update
    void Start()
    {
        objectiveInfo.enabled = true;
        hud.enabled = false;
        pauseMenu.enabled = false;
        gameOverMenu.enabled = false;
        gameWonMenu.SetActive(false);
        optionsMenu.SetActive(false);

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
        if (!gameOver && !gameWon)
        {
            timeRemaining -= Time.deltaTime;
        }

        if (timeRemaining <= 0 && !gameWon && !gameOver) 
        {
            gameOver = true;
            gameOverCausesIndex = 1;
            StopGame();
        }

        if (customersFed == customersToFeed) 
        {
            gameWon = true;
            StopGame();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && objectiveInfo.enabled == false && !gameOver && !gameWon) 
        {
            optionsMenu.SetActive(false);
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

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void BeginGame()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen and hide it once game has begun
        mouseLookScript.enabled = true;
        fireProjectilesScript.enabled = true;
        objectiveInfo.enabled = false;
        hud.enabled = true;
        Time.timeScale = 1;
    }

    void PauseGame()
    {
        gameIsPaused = true;

        Cursor.lockState = CursorLockMode.None;
        mouseLookScript.enabled = false; 
        fireProjectilesScript.enabled = false;
        pauseMenu.enabled = true;
        Time.timeScale = 0;
    }

    public void ShowOptionsMenu()
    {
        optionsMenu.SetActive(true);
    }

    public void HideOptionsMenu()
    {
        optionsMenu.SetActive(false);
    }

    public void ResumeGame()
    {
        gameIsPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        mouseLookScript.enabled = true;
        fireProjectilesScript.enabled = true;
        pauseMenu.enabled = false;
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StopGame()
    {
        // Disable both interactivity scripts so players can't move
        player.GetComponent<PlayerMovement>().enabled = false;
        playerCam.GetComponent<MouseLook>().enabled = false;

        if (gameOver)
        {
            StartCoroutine(AwaitGameOver());
        } 
        else if (gameWon) 
        {
            gameWonMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            StartCoroutine(AwaitGameWon());
        }
    }

    public void ExitGame()
    {
        Debug.Log("Game Closed");
        Application.Quit();
    }

    IEnumerator AwaitGameOver()
    {
        yield return new WaitForSeconds(1);
        gameOverMenu.enabled = true;
        Cursor.lockState = CursorLockMode.None;
        mouseLookScript.enabled = false;
        fireProjectilesScript.enabled = false;
        hud.enabled = false;
        Time.timeScale = 0;
    }

    IEnumerator AwaitGameWon()
    {
        yield return new WaitForSeconds(1);
        mouseLookScript.enabled = false;
        fireProjectilesScript.enabled = false;
        hud.enabled = false;
        Time.timeScale = 0;
    }
}
