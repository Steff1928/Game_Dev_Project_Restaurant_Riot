using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCustomers : MonoBehaviour
{
    [SerializeField] GameObject[] customers;
    [SerializeField] GameObject[] enemyCustomers;


    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating(nameof(SpawnInCustomers), 5, 5);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnInCustomers()
    {
        //int customersIndex = Random.Range(0, customers.Length);

        //Instantiate(customers[customersIndex], new Vector3(Random.Range(-8.0f, 8.0f), 0, 8.0f), Quaternion.identity);
    }
}
