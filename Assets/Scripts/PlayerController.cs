using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    private Vector3 _thrust;
    private float _steerVelocity;
    
    public GameObject centerOfMass;
    public Transform water;
    
    public float waterLevelLeniency = 0.1f;

    public float maxSpeed = 5f;
    public float acceleration = 10f;
    public float deceleration = 10f;

    public float maxSteerSpeed = 5f;
    public float steerSpeed = 10f;
    public float steerDecay = 0.1f;
    
    public float shipRockingSpeed = 10f;
    public float shipRockingAmount = 10f;
    
    public float shipRollingSpeed = 10f;
    public float shipRollingAmount = 10f;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.centerOfMass = centerOfMass.transform.localPosition;
    }

    private void FixedUpdate()
    {
        // If the boat is not touching the water, don't allow movement
        if (transform.position.y > water.position.y + waterLevelLeniency)
        {
            return;
        }
        
        // Get the input
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");
        
        // If we're moving forward
        if (verticalInput > 0)
        {
            // Slowly approach the max speed, with acceleration affecting how fast we get there. The closer we get to max speed, the slower we accelerate.
            // Use rigidbody forward vector to determine which direction we're moving in
            _thrust = _rb.transform.forward * (verticalInput * acceleration * (1 - _rb.velocity.magnitude / maxSpeed));
        } else if (verticalInput < 0)
        {
            // If we're moving backwards, we decelerate at a different rate
            _thrust = _rb.transform.forward * (verticalInput * deceleration * (1 - _rb.velocity.magnitude / maxSpeed));
        }

        // If the player is rotating the ship we change the rotation velocity of the ship
        if (horizontalInput != 0)
        {
            _steerVelocity += horizontalInput * steerSpeed;
        }
        // Slowly decay the rotation velocity
        _steerVelocity *= 1 - steerDecay * Time.fixedDeltaTime;
        
        // Add the velocity force to the rigidbody
        _rb.AddForce(_thrust, ForceMode.Acceleration);
        // Add a rotation force to the rigidbody to rotate the ship, limit the max rotation speed to prevent the ship from spinning out of control
        _rb.AddTorque(Vector3.up * Mathf.Clamp(_steerVelocity, -maxSteerSpeed, maxSteerSpeed));
        
        // Add a rocking force to the ship to make it look more realistic
        _rb.AddRelativeTorque(Vector3.right * (Mathf.Sin(Time.time * shipRockingSpeed) * shipRockingAmount));
        
        // Add a rolling force to the ship to make it look more realistic
        _rb.AddRelativeTorque(Vector3.forward * (Mathf.Sin(Time.time * shipRollingSpeed) * shipRollingAmount));
    }
}
