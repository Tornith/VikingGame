using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothMoveSpeed = 0.125f;
    public float smoothRotationSpeed = 0.125f;
    public float rotationThreshold = 0.1f;

    private Vector3 _initialOffset;
    private Quaternion _initialRotation;
    
    private bool _isRotating = false;

    private void Start()
    {
        // Get the immediate offset and rotation of the camera from the target at the start of the game
        _initialOffset = transform.position - target.position;
        _initialRotation = transform.rotation;
    }

    private void LateUpdate()
    {
        // Rotate the y-component of the camera if the angle between the y-rotation of camera and the y-rotation of player
        // is greater than the threshold
        if (_isRotating || Mathf.Abs(transform.rotation.eulerAngles.y - target.rotation.eulerAngles.y) > rotationThreshold)
        {
            _isRotating = true;
            var desired = Quaternion.Euler(_initialRotation.eulerAngles.x, target.rotation.eulerAngles.y, _initialRotation.eulerAngles.z);
            transform.rotation = Quaternion.Slerp(transform.rotation, desired, smoothRotationSpeed / 1000);
            if (Quaternion.Angle(transform.rotation, desired) < 0.1f)
            {
                _isRotating = false;
            }
        }

        // Move the camera to the target's position with the offset with respect to rotation
        var rotationOffset = Quaternion.Euler(0, 90, 0) * target.rotation;
        // Lock the y-position of the camera
        Vector3 desiredPosition = target.position + (rotationOffset * _initialOffset);
        desiredPosition.y = transform.position.y;
        Vector3 smoothedPosition = Vector3.Slerp(transform.position, desiredPosition, smoothMoveSpeed / 1000);
        transform.position = smoothedPosition;
    }
}
