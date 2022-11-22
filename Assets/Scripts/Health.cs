using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health = 100;
    public float maxHealth = 100;
    
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0.0f)
        {
            Die();
        }
    }
    
    public void Die()
    {
        Destroy(gameObject);
    }
    
    public void Heal(float healAmount)
    {
        health += healAmount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }
    
    public void SetHealth(float newHealth)
    {
        health = newHealth;
    }
    
    public void SetMaxHealth(float newMaxHealth)
    {
        maxHealth = newMaxHealth;
    }
    
    public float GetHealth()
    {
        return health;
    }
    
    public bool IsDead()
    {
        return health <= 0.0f;
    }
}
