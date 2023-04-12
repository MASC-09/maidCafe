using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player;  // The player's transform
    public float cameraDistance = 5f;  // Distance of the camera from the player
    public float cameraHeight = 2f;  // Height of the camera from the player
    public float cameraSmoothTime = 0.1f;  // Time it takes for the camera to smoothly follow the player
    public float cameraRotationSpeed = 5f;  // Speed at which the camera rotates

    private Vector3 cameraVelocity = Vector3.zero;  // Velocity of the camera's movement

    void Update()
    {
        // Get the mouse movement
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Rotate the player based on the mouse movement
        player.Rotate(Vector3.up, mouseX * cameraRotationSpeed);

        // Rotate the camera based on the mouse movement
        transform.RotateAround(player.position, Vector3.up, mouseX * cameraRotationSpeed);
        transform.RotateAround(player.position, transform.right, -mouseY * cameraRotationSpeed);

        // Calculate the desired camera position
        Vector3 cameraPosition = player.position - transform.forward * cameraDistance + Vector3.up * cameraHeight;

        // Smoothly move the camera to the desired position
        transform.position = Vector3.SmoothDamp(transform.position, cameraPosition, ref cameraVelocity, cameraSmoothTime);
    }
}
