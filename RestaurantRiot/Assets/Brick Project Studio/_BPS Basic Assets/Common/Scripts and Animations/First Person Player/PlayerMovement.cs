using UnityEngine;

/// Script provided by the Fast Food Restaurant Kit asset pack by Brick Project Studios.
/// This script has been modified during project development to account for plagarism

namespace SojaExiles
{
    // Contains logic for basic player movement and some assocatiated gameplay mechanics
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] CharacterController controller; // Reference the CharacterController component

        // Variables that control player movement behaviour
        [SerializeField] float speed = 5f;
        [SerializeField] float gravity = -15f;

        public float timeIncrease = 15; // Determines the increase value from a time collectible

        Vector3 velocity; // Variable to hold a Vector3 position for velocity

        bool isGrounded = false; // Determines whether the player is grounded

        // Script references
        FireProjectiles fireProjectilesScript;
        GameManager gameManager;
        UIManager uiManager;

        // Start is called before the first frame update
        private void Start()
        {
            // Find the FireProjectiles, GameManager and UIManager anywhere in the hierachy and store them here
            fireProjectilesScript = FindAnyObjectByType<FireProjectiles>();
            gameManager = FindAnyObjectByType<GameManager>();
            uiManager = FindAnyObjectByType<UIManager>();
        }

        // Update is called once per frame
        void Update()
        {
            // Get both horizontal and vertical axis for movement and assign to a variable
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            // Store movement from x and z as a local Vector3 variable
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
            // If the player collides with a food collectible, increase the number
            // of food items on FireProjectiles script
            if (other.gameObject.CompareTag("FoodCollectible"))
            {
                fireProjectilesScript.foodItems++;
                Destroy(other.gameObject);
            }
            // If the player collides with a time collectible, increase timeRemaining by timeIncrease
            else if (other.gameObject.CompareTag("TimeCollectible")) 
            {
                gameManager.timeRemaining += timeIncrease;
                uiManager.DisplayTimeCollected();
                Destroy(other.gameObject);
            }
        }
    }
}