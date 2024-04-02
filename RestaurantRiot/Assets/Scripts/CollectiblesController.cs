using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesController : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 50.0f;
    [SerializeField] float speed = 50.0f;
    [SerializeField] float height = 0.2f;

    Vector3 pos;

    [SerializeField] float sphereRadius = 5f;
    [SerializeField] LayerMask exceptionMask;

    // Start is called before the first frame update
    void Start()
    {
        // Get the objects current position and put it in a variable so we can access it later with less code
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);

        // Calculate what the new Y position will be
        float newY = Mathf.Sin(Time.time * speed);
        // Set the object's Y to the new calculated Y
        transform.position = new Vector3(pos.x, (newY * height) + 0.5f, pos.z);

        if (!Physics.CheckSphere(transform.position, sphereRadius, exceptionMask))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Food") && !other.gameObject.CompareTag("Ground") && !other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collectible is colliding with an object!");
            Destroy(gameObject);
        }
    }
}
