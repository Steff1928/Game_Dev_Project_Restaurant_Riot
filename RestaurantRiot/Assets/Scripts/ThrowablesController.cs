using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowablesController : MonoBehaviour
{
    [SerializeField] float speed;

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
            Debug.Log("Object hit!");
            Destroy(gameObject);
        }
    }

    // Destroy food item prefab if existing for too long (failsafe if collison doesn't work to avoid memory loss)
    IEnumerator DeleteProjectile()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Object destroyed for lasting too long");
        Destroy(gameObject);
    }
}
