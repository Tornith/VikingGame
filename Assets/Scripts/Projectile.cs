using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // The starting point of the arrow
    public Vector3 start;
    // The ending point of the arrow
    public Vector3 end;
    // The peak height of the arrow, happens at the midpoint
    public float archHeight = 1f;
    // The Projectile damage
    public float damage = 1f;
    // The speed of the arrow
    public float speed = 0.01f;
    // The time the arrow takes to despawn after hitting the target
    public float despawnTime = 0.5f;

    // Percentage of the distance traveled from start to end
    private float _distanceTravelled;
    private bool _isDespawning;
    private bool _hasCollided;
    private void Start()
    {
        transform.position = start;
    }
    
    private void Update()
    {
        if (_isDespawning) return;
        if (_distanceTravelled >= 10f)
        {
            Destroy(gameObject, despawnTime);
            _isDespawning = true;
            return;
        }
        var distance = Vector3.Distance(start, end);
        // Move the arrow
        _distanceTravelled += Time.deltaTime * (speed / distance);
        transform.position = CalculatePosition(_distanceTravelled);
        // Rotate the arrow
        transform.LookAt(CalculatePosition(_distanceTravelled + 0.1f));
    }
    
    private Vector3 CalculatePosition(float t)
    {
        var distance = Vector3.Distance(start, end);
        var actualArchHeight = archHeight * distance * 0.1f;
        var midpoint = ((start + end) / 2f) + (Vector3.up * actualArchHeight);
        var a = LerpWithoutClamp(start, midpoint, t);
        var b = LerpWithoutClamp(midpoint, end, t);
        return LerpWithoutClamp(a, b, t);
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the collision is in the first frame, ignore it
        if (_hasCollided) return;
        _hasCollided = true;
        // If the arrow hits a target that has a health component, deal damage and destroy immediately
        var health = other.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(damage);
            Destroy(gameObject);
        }
        // If the projectile hits water destroy it immediately
        else if (other.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            Destroy(gameObject);
        }
        Destroy(gameObject, despawnTime);
        _isDespawning = true;
    }

    private Vector3 LerpWithoutClamp(Vector3 a, Vector3 b, float t)
    {
        return a + (b-a)*t;
    }
}