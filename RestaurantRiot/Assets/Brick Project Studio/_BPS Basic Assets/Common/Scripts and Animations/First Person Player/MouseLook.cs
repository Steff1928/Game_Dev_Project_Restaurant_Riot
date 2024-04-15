using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Script provided by the Fast Food Restaurant Kit asset pack by Brick Project Studios.
/// Since this a very basic way to achieve mouse look movement, this script has not been greatly modified 
/// (although, I still do not claim ownership of the contents of this script)

namespace SojaExiles
{
    public class MouseLook : MonoBehaviour
    {
        public Transform playerBody;

        float xRotation = 0f;

        // Update is called once per frame
        void Update()
        {
            // Get the X and Y axis of mouse input
            float mouseX = Input.GetAxis("Mouse X") * SettingsManager.mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * SettingsManager.mouseSensitivity;

            xRotation -= mouseY; // Allow the player to look up and down
            xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Prevent the player from tilting the camera above or below a certain limit

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}