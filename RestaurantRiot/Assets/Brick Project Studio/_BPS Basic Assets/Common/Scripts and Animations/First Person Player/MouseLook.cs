using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SojaExiles

{
    public class MouseLook : MonoBehaviour
    {
        public float mouseXSensitivity = 100f;

        public Transform playerBody;

        float xRotation = 0f;

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            // Get the X and Y axis of mouse input
            float mouseX = Input.GetAxis("Mouse X") * mouseXSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseXSensitivity;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Prevent the player from tilting the camera above or below a certain limit

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}