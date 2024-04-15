using SojaExiles;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

// Contains logic for enemy behaviour
public class EnemyAI : MonoBehaviour
{
    GameObject player; // Reference the player object

    // Reference essential components
    Animator animator;
    NavMeshAgent agent;
    BoxCollider boxCollider;

    // Reference the GameManager script
    GameManager gameManager;

    // Bools to check if the player is caught or if an enemy is stunned
    bool hasCaughtPlayer = false;
    bool isStunned = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component as soon as the game starts up
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Find the player active within the hierachy

        // Get the Animator and BoxCollider components
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();

        gameManager = FindAnyObjectByType<GameManager>(); // Find the GameManager script which could be attached to any object in the hierachy
    }

    // Update is called once per frame
    void Update()
    {
        // If the player has not yet been caught or the enemy object has not been stunned, contiously chase the player
        if (!hasCaughtPlayer && !isStunned) 
        {
            ChasePlayer();
            agent.speed = 2.0f; // Set a fixed speed for each enemy to operate at
            boxCollider.enabled = true;
        } 
        else // Set the enemy object's NavMeshAgent to 0 and BoxCollider to false if the above statement couldn't run
        {
            agent.speed = 0;
            boxCollider.enabled = false;
        }

        float dist = agent.remainingDistance; // Store the enemy objects remaining distance in a local variable
        // Determine whether the enemy has arrived at their destination and switch an animation trigger accordingly
        if (dist != Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance == 0)
        {
            animator.SetBool("isRunning", false);
        } 
        else
        {
            animator.SetBool("isRunning", true);
        }
    }

    // Contiously chase the player when called
    void ChasePlayer()
    {
        agent.SetDestination(player.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the enemy object has collided with the player (provided a few conditions are false) trigger a game over state from GameManager
        if (other.gameObject.CompareTag("Player") && !isStunned && !gameManager.gameWon && !gameManager.gameOver)
        {
            hasCaughtPlayer = true;
            animator.SetTrigger("hasCaughtPlayer");
            gameManager.gameOver = true;
            gameManager.gameOverCausesIndex = 0;
            gameManager.StopGame();
        }
        // If the enemy object has collided with a food item, run the following code to trigger a "stunned" state for the enemy
        if (other.gameObject.CompareTag("Food") && !isStunned)
        {
            animator.SetBool("isStunned", true);
            isStunned = true;
            StartCoroutine(StunTime()); // Start a coroutine to reset the enemy state
        }
    }

    // After a few seconds, reset the enemys state back to running as the enemy is no longer stunned
    IEnumerator StunTime()
    {
        yield return new WaitForSeconds(5.0f);
        animator.SetBool("isStunned", false);
        isStunned = false;
    }
}
