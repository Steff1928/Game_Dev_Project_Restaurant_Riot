using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectiles : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;

    MeshRenderer mesh;

    public float foodItems = 10;


    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Disable the game object to show the player has no food items left
        if (foodItems == 0)
        {
            mesh.enabled = false;
        } else
        {
            mesh.enabled = true;
        }

        // Instatiate a clone of the food item projectile when the space key is pressed
        if (Input.GetMouseButtonDown(0) && foodItems > 0)
        {
            Instantiate(projectilePrefab, transform.position, transform.rotation);
            foodItems--;
        } 
    }
}
