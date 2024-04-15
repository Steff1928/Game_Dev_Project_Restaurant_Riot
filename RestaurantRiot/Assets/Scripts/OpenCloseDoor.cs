using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// Script provided by the Fast Food Restaurant Kit asset pack by Brick Project Studios (copied over to a new C# file to correctly abide to naming conventions).
/// This script has been modified during project development to account for plagarism

// Contains player and enemy interaction logic with doors around the in-game area
public class OpenCloseDoor : MonoBehaviour
{
    Animator openAndCloseAnim; // Reference the Animator component
    bool isOpen; // Bool to determine if the door is open
    Transform player; // Reference the Transform component on the player 

    NavMeshObstacle obstacle; // Reference the NavMeshObstacle component

    float interactionDist = 5f; // The distance at which the player has to be within to open a door

    void Start()
    {
        openAndCloseAnim = GetComponent<Animator>(); // Get the Animator component
        // Find the Game Object tagged as "Player" within the hierachy and get the Transform on it
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        obstacle = GetComponent<NavMeshObstacle>(); // Get the NavMeshObstacle component

        isOpen = false; // Initialise isOpen to be false
    }

    // The following code is only run once the player mouses over a door game object
    void OnMouseOver()
    {
        {
            // Only run the following if the game can process that is it the player that moused over the door
            if (player)
            {
                // Store the distance from the door object to the player in a local float
                float dist = Vector3.Distance(player.position, transform.position);
                // If the current distance of the player is less then the required distance for interaction
                // run the following
                if (dist < interactionDist)
                {
                    // If the player presses E and the door is not open, start a coroutine to open the door
                    if (isOpen == false)
                    {
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            StartCoroutine(Opening());
                        }
                    }
                    else // If the door is already open, start a coroutine to close the door
                    {
                        if (isOpen == true)
                        {
                            if (Input.GetKeyDown(KeyCode.E))
                            {
                                StartCoroutine(Closing());
                            }
                        }

                    }

                }
            }

        }

    }

    // If an enemy collides with a door game object and the door is closed, start a coroutine to open the door
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && !isOpen)
        {
            Debug.Log("Enemy opening door");
            StartCoroutine(Opening());
        }
    }

    // After some time, play an animation and register the door as open
    IEnumerator Opening()
    {
        print("you are opening the door");
        openAndCloseAnim.Play("Opening");
        isOpen = true;
        obstacle.enabled = false; // Prevent enemies from getting stuck on doors when they are opened
        yield return new WaitForSeconds(.5f);
    }

    // After some time, play an animation and register the door as closed
    IEnumerator Closing()
    {
        print("you are closing the door");
        openAndCloseAnim.Play("Closing");
        isOpen = false;
        obstacle.enabled = true; // Prevent enemies from getting through doors when they are closed
        yield return new WaitForSeconds(.5f);
    }
}
