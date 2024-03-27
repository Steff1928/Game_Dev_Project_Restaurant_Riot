using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCustomers : MonoBehaviour
{
    [SerializeField] GameObject[] customers;
    [SerializeField] GameObject[] enemyCustomers;
    [SerializeField] GameObject player;

    float xBoundRight = 21.5f;
    float xBoundLeft = -25.0f;
    float zBoundBack = 4.5f;
    float zBoundForward = 17.0f;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawnInCustomers), 3, 5);

        // TODO: Spawn in customers between random intervals
        // TODO: Ensure customers are spawned within the bounds of the level (booleans, colliders and tags)
        // TODO: Remove a customer that is touching an object
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnInCustomers()
    {
        int customersIndex = Random.Range(0, customers.Length);
        Vector3 spawnPos = new Vector3(Random.Range(xBoundLeft, xBoundRight), 0.1f, Random.Range(zBoundBack, zBoundForward));

        Instantiate(customers[customersIndex], spawnPos, Quaternion.identity);
    }
}
