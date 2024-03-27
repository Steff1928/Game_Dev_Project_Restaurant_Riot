using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageSpawn : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); // Reference the animator component on the customer
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Check to see if the customer is within the boundaries of the level and isn't touching any other objects
    private void OnCollisionEnter(Collision collision)
    {
        if (!(collision.gameObject.CompareTag("Ground")))
        {
            if (!(collision.gameObject.CompareTag("Food")))
            {
                Debug.Log("Customer isn't within confines!");
                Destroy(gameObject);
            } 
        }
    }

    // If a food item projectile collides with a customer, play an animation and start a coroutine to remove them from
    // the scene
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
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
