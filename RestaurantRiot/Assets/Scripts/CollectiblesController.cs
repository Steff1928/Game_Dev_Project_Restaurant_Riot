using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesController : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 50.0f;
    [SerializeField] float speed = 50.0f;
    [SerializeField] float height = 0.2f;
    
    public float heightOffset = 0.5f;

    Vector3 pos;

    [SerializeField] LayerMask exceptionMask;

    BoxCollider boxCollider;
    MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        // Get the objects current position and put it in a variable so we can access it later with less code
        pos = transform.position;
        meshRenderer = GetComponent<MeshRenderer>();

        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;

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

        StartCoroutine(ShowCollectibles());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);

        // Calculate what the new Y position will be
        float newY = Mathf.Sin(Time.time * speed);
        // Set the object's Y to the new calculated Y
        transform.position = new Vector3(pos.x, (newY * height) + heightOffset, pos.z);

        // Checks to see if any objects with the layer "exceptionMask" are within the box's collider
        // (with exagerrated y to detect ground), if not, destroy the object
        if (!Physics.CheckBox(transform.position, new Vector3(boxCollider.size.x, boxCollider.size.y * 4, boxCollider.size.z), Quaternion.identity, exceptionMask))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // TODO: Clean up this code
        if (!other.gameObject.CompareTag("Food")
            && !other.gameObject.CompareTag("Ground")
            && !other.gameObject.CompareTag("Player")
            && !other.gameObject.CompareTag("FoodCollectible")
            && !other.gameObject.CompareTag("TimeCollectible")
            && !other.gameObject.CompareTag("Enemy")
            && !other.gameObject.CompareTag("Customers"))
        {
            Debug.Log("Collectible is colliding with a structural object!");
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        boxCollider = GetComponent<BoxCollider>();

        Gizmos.DrawWireCube(transform.position, new Vector3(boxCollider.size.x, boxCollider.size.y * 4, boxCollider.size.z));
    }

    IEnumerator ShowCollectibles()
    {
        yield return new WaitForSeconds(1);

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
