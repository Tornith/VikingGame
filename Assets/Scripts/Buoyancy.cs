using System;
using UnityEngine;

public class Buoyancy : MonoBehaviour
{
    public Transform waterSurface;
    
    public float underWaterDrag = 3f;
    public float underWaterAngularDrag = 1f;
    public float airDrag = 0.1f;
    public float airAngularDrag = 0.05f;
    public float waterDensity = 1f;

    private Rigidbody _rigidbody;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        var difference = waterSurface.position.y - transform.position.y;
        
        if (difference > 0)
        {
            _rigidbody.drag = underWaterDrag;
            _rigidbody.angularDrag = underWaterAngularDrag;
            
            _rigidbody.AddForceAtPosition(Vector3.up * (difference * waterDensity), transform.position);
        }
        else
        {
            _rigidbody.drag = airDrag;
            _rigidbody.angularDrag = airAngularDrag;
        }
    }
}