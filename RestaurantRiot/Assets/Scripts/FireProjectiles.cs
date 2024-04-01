using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectiles : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Instatiate a clone of the food item projectile when the space key is pressed
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(projectilePrefab, transform.position, transform.rotation);
        }
    }
}
