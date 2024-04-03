using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace SojaExiles
{
    public class PlayerMovement : MonoBehaviour
    {
        public CharacterController controller;

        public float speed = 5f;
        public float gravity = -15f;

        Vector3 velocity;

        bool isGrounded = false;

        FireProjectiles fireProjectilesScript;

        // Start is called before the first frame update
        private void Start()
        {
            // Find the FireProjectiles script on a child component of the Player to access number of food items
            fireProjectilesScript = FindAnyObjectByType<FireProjectiles>();
        }

        // Update is called once per frame
        void Update()
        {
            // Get both horizontal and vertical axis for movement and assign to a variable
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right.normalized * x + transform.forward.normalized * z;

            controller.Move(move * speed * Time.deltaTime); // Move the player when WASD or Arrow Keys are pressed

            // Manage gravity for the player
            if (isGrounded)
            {
                velocity = Vector3.zero;
            } 
            else
            {
                velocity.y += gravity * Time.deltaTime; // Only increase velocity if the player is not touching the ground (avoiding bugs)
            }

            controller.Move(velocity * Time.deltaTime); // Move the player in relation to their velocity on the y axis

        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            // Set isGrounded to true if the CharacterController Collider is touching the ground
            if (hit.gameObject.CompareTag("Ground"))
            {
                isGrounded = true;
            } else
            {
                isGrounded = false;
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("FoodCollectible"))
            {
                fireProjectilesScript.foodItems++; // Increase number of food items on FireProjectiles script
                Destroy(other.gameObject);
            }
        }
    }
}