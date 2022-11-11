using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnvironmentalDamage : MonoBehaviour
{
    public float baseDamage = 10f;
    public float damageRate = 1f;
    public float pushBackForce = 10f;
    public float pushBackRate = 1f;
    
    private float _nextDamage;
    private float _nextPushBack;

    // On 3d rigidbody collision with player
    private void OnCollisionEnter(Collision collision)
    {
        // If the collision is with the player
        if (!collision.gameObject.CompareTag("Player")) return;
        // If the time is greater than the next damage time
        if (Time.time > _nextDamage)
        {
            // Set the next damage time
            _nextDamage = Time.time + damageRate;
            // Calculate with what force the player collided with the object
            var force = collision.impulse / Time.fixedDeltaTime;
            // Calculate the damage based on the force
            var damage = force.magnitude * baseDamage * 0.001f;
            // Apply damage to the player
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
        // If the time is greater than the next push back time
        if (!(Time.time > _nextPushBack)) return;
        // Set the next push back time
        _nextPushBack = Time.time + pushBackRate;
        // Get the direction of the collision
        var pushDirection = collision.transform.position - transform.position;
        // Push the player back
        pushDirection.y = 0f;
        collision.gameObject.GetComponent<PlayerController>().KnockBack(pushDirection.normalized, pushBackForce);
    }
}
