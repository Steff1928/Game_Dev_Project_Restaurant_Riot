using SojaExiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    GameObject player;
    Camera playerCamera;

    Animator animator;
    NavMeshAgent agent;

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

        agent.speed = 100.0f * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasCaughtPlayer && !isStunned) 
        {
            ChasePlayer();
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
            // Disable both interactivity scripts so players can't move
            player.GetComponent<PlayerMovement>().enabled = false;
            playerCamera.GetComponent<MouseLook>().enabled = false;
            //playerCamera.transform.LookAt(transform.position); // Look at the enemy to show that the player has been caught

            animator.SetTrigger("hasCaughtPlayer");
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
