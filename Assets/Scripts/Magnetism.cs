using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetism : MonoBehaviour
{
    public GameObject magnet;
    
    public float magnetForce = 10f;
    public float magnetRange = 10f;
    
    private Rigidbody _rb;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    
    private void FixedUpdate()
    {
        var direction = magnet.transform.position - transform.position;
        var distance = direction.magnitude;
        if (distance < magnetRange)
        {
            _rb.AddForce(direction.normalized * (magnetForce * (1 - distance / magnetRange)));
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, magnetRange);
    }
}
