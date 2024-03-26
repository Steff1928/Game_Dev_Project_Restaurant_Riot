using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowablesController : MonoBehaviour
{
    [SerializeField] float speed;

    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        // Move the projectile forward once instantiated
        transform.Translate(Vector3.forward * -speed * Time.deltaTime);
        StartCoroutine(nameof(DeleteProjectile));
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Object hit!");
        Destroy(gameObject);
    }

    // Destroy food item prefab if existing for too long (failsafe if collison doesn't work to avoid memory loss)
    IEnumerator DeleteProjectile()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Object destroyed for lasting too long");
        Destroy(gameObject);
    }
}
