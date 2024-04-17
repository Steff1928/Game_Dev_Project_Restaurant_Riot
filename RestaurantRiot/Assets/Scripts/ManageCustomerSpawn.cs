using System.Collections;
using UnityEngine;

// Contains logic for how customers act upon instatiation
public class ManageCustomerSpawn : MonoBehaviour
{
    GameManager gameManager; // Reference the GameManager scripts

    // Reference the Animator and BoxCollider components
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        // Loop through each skinned mesh render on the children game objects of each customer to disable
        // it on start up (avoiding bugs where players can see customers appearing and disappearing)
        foreach (SkinnedMeshRenderer skinnedMesh in GetComponentsInChildren<SkinnedMeshRenderer>()) 
        {
            skinnedMesh.enabled = false;
        }

        animator = GetComponent<Animator>(); // Reference the animator component on the customer
        gameManager = FindAnyObjectByType<GameManager>(); // Find the Game Manager script
    }

    // Check to see if the customer is within the boundaries of the level and isn't touching any other objects
    private void OnCollisionEnter(Collision collision)
    {
        if (!(collision.gameObject.CompareTag("Ground")))
        {
            if (!(collision.gameObject.CompareTag("Food")))
            {
                Destroy(gameObject);
            } 
        } else // If the customer is in contact with the ground, enable each of the child components meshes
        {
            foreach (SkinnedMeshRenderer skinnedMesh in GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                skinnedMesh.enabled = true;
            }
        }
    }

    // If a food item projectile collides with a customer, play an animation and start a coroutine to remove them from
    // the scene
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            gameManager.customersFed++;
            animator.SetTrigger("hasBeenFed");
            StartCoroutine(nameof(DestroyCustomer));
        }
    }

    // Destroy the customer game object after some time when called
    IEnumerator DestroyCustomer()
    {
        yield return new WaitForSeconds(1.3f);
        Destroy(gameObject);
    }
}
