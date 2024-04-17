using UnityEngine;

/// Script provided by the Fast Food Restaurant Kit asset pack by Brick Project Studios.
/// Since this a very basic way to achieve mouse look movement, this script has not been greatly modified 
/// (although, I still do not claim ownership of the contents of this script)

namespace SojaExiles
{
    // Contains logic for Player Mouse Movement
    public class MouseLook : MonoBehaviour
    {
        [SerializeField] Transform playerBody; // Reference the Transform of the Player Body, to be assigned in inspector

        float xRotation = 0f; // Variable for xRotation

        // Update is called once per frame
        void Update()
        {
            // Get the X and Y axis of mouse input
            float mouseX = Input.GetAxis("Mouse X") * SettingsManager.mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * SettingsManager.mouseSensitivity;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Prevent the player from tilting the camera above or below 90 degrees

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Allow the player to tilt left and right on the X Axis
            playerBody.Rotate(Vector3.up * mouseX); // Allow the player to look up and down
        }
    }
}