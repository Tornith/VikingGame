using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBobbing : MonoBehaviour
{
    public float bobbingSpeed = 0.18f;
    public float bobbingAmount = 0.2f;
    
    private float _timer;
    private Rigidbody _rb;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    
    private void FixedUpdate()
    {
        // Apply the bobbing effect to the rigidbody as a force
        _rb.AddForce(new Vector3(0, Mathf.Sin(_timer) * bobbingAmount, 0), ForceMode.Acceleration);
        
        // Increase the timer
        _timer += bobbingSpeed * Time.deltaTime;
    }
}
