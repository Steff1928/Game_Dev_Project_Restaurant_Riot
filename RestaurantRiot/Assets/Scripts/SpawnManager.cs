using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Logic for the instatiation of multiple objects
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

    // Assign variables to keep track of enemies spawned
    int enemiesSpawned = 0;
    int enemiesSpawnLimit = 3;

    // Spawn Rate variables for collectibles and customers
    float spawnRateTime = 4.5f;
    float spawnRateFood = 1f;
    float spawnRateCustomer = 1.5f;
    float randomEnemySpawn; // Variable to determine a random enemy spawn

    // Wait time for customers and enemies
    float waitTime = 5f;
    float enemyWaitTime = 30f;

    // Start is called before the first frame update
    void Start()
    {
        randomEnemySpawn = Random.Range(15.0f, 30.0f); // Assign a random value to the random enemy spawn timer upon start up

        InvokeRepeating(nameof(SpawnInCustomers), waitTime, spawnRateCustomer); // Spawn a customer every few seconds
        InvokeRepeating(nameof(SpawnInEnemyCustomers), enemyWaitTime, randomEnemySpawn); // Spawn an enemy customer every few seconds
        
        // Spawn collectibles every few seconds
        InvokeRepeating(nameof(SpawnInFoodCollectibles), waitTime, spawnRateFood);
        InvokeRepeating(nameof(SpawnInTimeCollectibles), waitTime, spawnRateTime);
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
        randomEnemySpawn = Random.Range(15.0f, 30.0f); // Update the randomEnemySpawn time with a random number
        int enemyIndex = Random.Range(0, enemyCustomers.Length); // Assign a random enemy customer to be spawned within a variable

        if (!(enemiesSpawned >= enemiesSpawnLimit)) // Prevent the game from spawning more enemies then the suggested limit
        {
            enemiesSpawned++;
            Instantiate(enemyCustomers[enemyIndex], enemySpawnPos, Quaternion.identity); // Instatiate enemy customers when called
        }
    }

    void SpawnInFoodCollectibles()
    {
        // Assign a random spawn position within the valid confines
        Vector3 spawnPos = new Vector3(Random.Range(xBoundLeft, xBoundRight), 1, Random.Range(zBoundBack, collectibleZBoundFoward));

        Instantiate(foodCollectibles, spawnPos, Quaternion.identity); // Instatiate food collectibles when called
    }

    void SpawnInTimeCollectibles()
    {
        // Assign a random spawn position within the valid confines
        Vector3 spawnPos = new Vector3(Random.Range(xBoundLeft, xBoundRight), 1, Random.Range(zBoundBack, collectibleZBoundFoward));

        Instantiate(timeCollectibles, spawnPos, Quaternion.identity); // Instatiate time collectibles when called
    }
}
