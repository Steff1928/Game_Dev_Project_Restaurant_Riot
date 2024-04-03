using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Create arrays of GameObjects to reference prefabs to spawn
    [SerializeField] GameObject[] customers;
    [SerializeField] GameObject[] enemyCustomers;
    [SerializeField] GameObject foodCollectibles;
    [SerializeField] GameObject timeCollectibles;

    [SerializeField] Vector3 enemySpawnPos; // enemySpawnPos to be determined in inspector

    // Assign the bounds of the level to variables
    float xBoundRight = 21.5f;
    float xBoundLeft = -25.0f;
    float zBoundBack = 4.5f;
    float zBoundForward = 17.0f;

    float collectibleZBoundFoward = 38.0f; // Collectibles can also be instatiated within the back offices of the map

    float randomEnemySpawn;

    // Start is called before the first frame update
    void Start()
    {
        randomEnemySpawn = Random.Range(15.0f, 30.0f);

        InvokeRepeating(nameof(SpawnInCustomers), 5, 2); // Spawn a customer every few seconds
        InvokeRepeating(nameof(SpawnInEnemyCustomers), 30, randomEnemySpawn); // Spawn an enemy customer every few seconds
        InvokeRepeating(nameof(SpawnInFoodCollectibles), 5, 1);
        InvokeRepeating(nameof(SpawnInTimeCollectibles), 5, 3);
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

    void SpawnInEnemyCustomers()
    {
        randomEnemySpawn = Random.Range(15.0f, 30.0f);
        int enemyIndex = Random.Range(0, enemyCustomers.Length); // Assign a random enemy customer to be spawned within a variable

        Instantiate(enemyCustomers[enemyIndex], enemySpawnPos, Quaternion.identity); // Instatiate enemy customers when called
    }

    void SpawnInFoodCollectibles()
    {
        Vector3 spawnPos = new Vector3(Random.Range(xBoundLeft, xBoundRight), 1, Random.Range(zBoundBack, collectibleZBoundFoward));

        Instantiate(foodCollectibles, spawnPos, Quaternion.identity);
    }

    void SpawnInTimeCollectibles()
    {
        Vector3 spawnPos = new Vector3(Random.Range(xBoundLeft, xBoundRight), 1, Random.Range(zBoundBack, collectibleZBoundFoward));

        Instantiate(timeCollectibles, spawnPos, Quaternion.identity);
    }
}
