using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Script provided by the Fast Food Restaurant Kit asset pack by Brick Project Studios (copied over to a new C# file to correctly abide to naming conventions).
/// This script has been modified during project development to account for plagarism

public class OpenCloseDoor : MonoBehaviour
{
    Animator openandclose;
    bool open;
    Transform player;

    float interactionDist = 5f;

    void Start()
    {
        openandclose = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        open = false;
    }

    void OnMouseOver()
    {
        {
            if (player)
            {
                float dist = Vector3.Distance(player.position, transform.position);
                if (dist < interactionDist)
                {
                    if (open == false)
                    {
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            StartCoroutine(Opening());
                        }
                    }
                    else
                    {
                        if (open == true)
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && !open)
        {
            Debug.Log("Enemy opening door");
            StartCoroutine(Opening());
        }
    }

    IEnumerator Opening()
    {
        print("you are opening the door");
        openandclose.Play("Opening");
        open = true;
        yield return new WaitForSeconds(.5f);
    }

    IEnumerator Closing()
    {
        print("you are closing the door");
        openandclose.Play("Closing");
        open = false;
        yield return new WaitForSeconds(.5f);
    }
}
