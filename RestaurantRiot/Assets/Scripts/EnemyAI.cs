using SojaExiles;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    GameObject player;
    Camera playerCamera;

    Animator animator;
    NavMeshAgent agent;

    BoxCollider boxCollider;

    GameManager gameManager;

    bool hasCaughtPlayer = false;
    bool isStunned = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player"); // Find the player active within the hierachy
        playerCamera = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Camera>(); // Find the camera attached to the player as a child component
        boxCollider = GetComponent<BoxCollider>();

        gameManager = FindAnyObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!hasCaughtPlayer && !isStunned) 
        {
            ChasePlayer();
            agent.speed = 2.0f;
            boxCollider.enabled = true;
        } else
        {
            agent.speed = 0;
            boxCollider.enabled = false;
        }

        float dist = agent.remainingDistance;
        if (dist != Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance == 0)
        {
            Debug.Log("Arrived at Destination");
            animator.SetBool("isRunning", false);
        } else
        {
            animator.SetBool("isRunning", true);
        }
    }

    void ChasePlayer()
    {
        agent.SetDestination(player.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !isStunned)
        {
            // TODO: Stop Game
            hasCaughtPlayer = true;
            animator.SetTrigger("hasCaughtPlayer");
            gameManager.gameOver = true;
            gameManager.gameOverCausesIndex = 0;
            gameManager.StopGame();
        }

        if (other.gameObject.CompareTag("Food") && !isStunned)
        {
            animator.SetBool("isStunned", true);
            isStunned = true;
            StartCoroutine(StunTime());
        }
    }

    IEnumerator StunTime()
    {
        yield return new WaitForSeconds(5.0f);
        animator.SetBool("isStunned", false);
        isStunned = false;
    }
}
