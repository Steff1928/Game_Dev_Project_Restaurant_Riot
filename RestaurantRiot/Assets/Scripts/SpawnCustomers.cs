using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCustomers : MonoBehaviour
{
    // Create an array of GameObjects to reference certain customers
    [SerializeField] GameObject[] customers;
    [SerializeField] GameObject[] enemyCustomers;

    // Assign the bounds of the level to variables
    float xBoundRight = 21.5f;
    float xBoundLeft = -25.0f;
    float zBoundBack = 4.5f;
    float zBoundForward = 17.0f;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawnInCustomers), 5, 3); // Spawn a customer every few seconds
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnInCustomers()
    {
        int customersIndex = Random.Range(0, customers.Length); // Assign a random customer to be spawned within a variable
        // Assign a random spawn position within the valid confines
        Vector3 spawnPos = new Vector3(Random.Range(xBoundLeft, xBoundRight), 0.1f, Random.Range(zBoundBack, zBoundForward));

        Instantiate(customers[customersIndex], spawnPos, Quaternion.identity); // Instatiate customers when called
    }
}
