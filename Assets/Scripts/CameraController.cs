using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _player;

    [SerializeField] private float cameraOffsetDistance = 2f; // Distance the camera should slide in the player's direction
    [SerializeField] private float smoothSpeed = 0.125f; // Smoothing speed for camera movement

    private Vector3 _velocity = Vector3.zero;

    private void LateUpdate()
    {
        // Get the player's current position
        Vector3 playerPosition = _player.position;

        // Get the player's facing direction
        Vector2 playerFacingDirection = _player.up; // Assuming the player is rotating its 'up' vector to face the mouse

        // Calculate the camera target position (with an offset in the player's facing direction)
        Vector3 targetPosition = playerPosition + (Vector3)playerFacingDirection * cameraOffsetDistance;

        // Smoothly move the camera towards the target position
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, smoothSpeed);

        // Update the camera's position
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
    }

}
