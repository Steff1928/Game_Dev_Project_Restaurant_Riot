using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageSpawn : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            animator.SetTrigger("hasBeenFed");
            StartCoroutine(nameof(DestroyCustomer));
        }
    }

    IEnumerator DestroyCustomer()
    {
        yield return new WaitForSeconds(1.3f);
        Destroy(gameObject);
    }
}
