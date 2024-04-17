using System.Collections;
using UnityEngine;

// Contains logic for how collectible objects act in game 
public class CollectiblesController : MonoBehaviour
{
    // Variables that modify the behaviour of the collectible (editable in inspector)
    [SerializeField] float rotateSpeed = 50.0f;
    [SerializeField] float ySpeed = 50.0f;
    [SerializeField] float height = 0.2f;

    [SerializeField] float ySize;
    
    // Determines how far off the ground the collectible should be
    [SerializeField] float heightOffset = 0.5f;

    Vector3 pos; // Stores a Vector3 of the collectibles original position

    [SerializeField] LayerMask exceptionMask; // Used within "CheckBox" to ignore anything with this mask

    // Component references
    BoxCollider boxCollider;
    MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        // Get the objects current position and put it in a variable so we can access it later with less code
        pos = transform.position;

        meshRenderer = GetComponent<MeshRenderer>(); // Get the MeshRenderer component

        // Get the BoxCollider component and disable it on instantiation of collectible object
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;

        // Disable the MeshRenderer component on startup for a food item collectible and for each child 
        // component of a time collectible
        if (gameObject.CompareTag("FoodCollectible"))
        {
            meshRenderer.enabled = false;
        } 
        
        if (gameObject.CompareTag("TimeCollectible"))
        {
            foreach (MeshRenderer mesh in GetComponentsInChildren<MeshRenderer>())
            {
                mesh.enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime); // Rotate the collectible object at a certain speed

        // Calculate what the new Y position will be
        float newY = Mathf.Sin(Time.time * ySpeed);
        // Set the object's Y to the new calculated Y
        transform.position = new Vector3(pos.x, (newY * height) + heightOffset, pos.z);

        // Checks to see if any objects with the layer "exceptionMask" are within the box's collider
        // (with exagerrated y to detect ground), if not, destroy the object
        if (!Physics.CheckBox(transform.position, new Vector3(boxCollider.size.x, boxCollider.size.y * ySize, boxCollider.size.z), Quaternion.identity, exceptionMask))
        {
            Destroy(gameObject);
        } 
        else
        {
            // Show the collectibles if they have not been destroyed (using a coroutine to ensure the object does not get
            // destroyed after a few seconds)
            StartCoroutine(ShowCollectibles());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Ensure that the collectible is not colliding with any of the following objects with tags and destroy it
        
        bool food = other.gameObject.CompareTag("Food");
        bool ground = other.gameObject.CompareTag("Ground");
        bool player = other.gameObject.CompareTag("Player");
        bool foodCollectible = other.gameObject.CompareTag("FoodCollectible");
        bool timeCollectible = other.gameObject.CompareTag("TimeCollectible");
        bool enemy = other.gameObject.CompareTag("Enemy");
        bool customers = other.gameObject.CompareTag("Customers");

        if (!food && !ground && !player && !foodCollectible && !timeCollectible && !enemy && !customers)
        {
            Destroy(gameObject);
        } 
        else
        {
            StartCoroutine(ShowCollectibles());
        }
    }

    // Clearly draw a cube around each collectible to show what it will be colliding with (in relation to "CheckCube")
    private void OnDrawGizmos()
    {
        boxCollider = GetComponent<BoxCollider>();

        Gizmos.DrawWireCube(transform.position, new Vector3(boxCollider.size.x, boxCollider.size.y * ySize, boxCollider.size.z));
    }

    // After some time, enable both the BoxCollider and MeshRenderer components for the collectible object when called
    IEnumerator ShowCollectibles()
    {
        yield return new WaitForSeconds(3);

        boxCollider.enabled = true;

        if (gameObject.CompareTag("FoodCollectible"))
        {
            meshRenderer.enabled = true;
        }

        if (gameObject.CompareTag("TimeCollectible"))
        {
            foreach (MeshRenderer mesh in GetComponentsInChildren<MeshRenderer>())
            {
                mesh.enabled = true;
            }
        }
    }
}
