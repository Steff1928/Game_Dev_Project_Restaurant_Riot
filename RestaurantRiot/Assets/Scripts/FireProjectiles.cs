using UnityEngine;

// Contains logic for firing a food item projectile
public class FireProjectiles : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab; // Reference a food item to throw from the inspector

    MeshRenderer mesh; // Reference the MeshRenderer component

    public float foodItems = 10; // Number of food items the player can throw before running out


    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>(); // Get the MeshRenderer component
    }

    // Update is called once per frame
    void Update()
    {
        // Disable the game object to show the player has no food items left
        if (foodItems == 0)
        {
            mesh.enabled = false;
        } 
        else
        {
            mesh.enabled = true;
        }

        // Instatiate a clone of the food item projectile when the space key is pressed and the player has food items remaining
        if (Input.GetMouseButtonDown(0) && foodItems > 0)
        {
            Instantiate(projectilePrefab, transform.position, transform.rotation);
            foodItems--;
        } 
    }
}
