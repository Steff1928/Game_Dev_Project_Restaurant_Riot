using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Behavioural logic for how the throwable food item acts upon instatiation
public class ThrowablesController : MonoBehaviour
{
    [SerializeField] float speed;

    GameManager gameManager;

    // Update is called once per frame
    void Update()
    {
        // Move the projectile forward once instantiated and start a timer to delete it if not deleted before hand
        transform.Translate(Vector3.forward * -speed * Time.deltaTime);
        StartCoroutine(nameof(DeleteProjectile));
    }

    // Destroy the food item projectile if it hits any object
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    // Destroy food item prefab if existing for too long (failsafe if collison doesn't work to avoid memory loss)
    IEnumerator DeleteProjectile()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
