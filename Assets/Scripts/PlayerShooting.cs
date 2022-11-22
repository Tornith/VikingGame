using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform arrowSpawn;
    public GameObject cursor;
    
    public float arrowSpeed = 10f;
    public int arrowCount = 10;
    public float arrowSpread = 0.1f;
    public float arrowArcHeight = 1f;
    public float arrowDespawnTime = 5f;
    
    public float shootingCooldown = 0.5f;
    
    private float _shootingCooldownTimer = 0.0f;

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && _shootingCooldownTimer <= 0.0f)
        {
            _shootingCooldownTimer = shootingCooldown;
            Shoot();
        }
        _shootingCooldownTimer -= Time.deltaTime;
        if (_shootingCooldownTimer < 0.0f)
        {
            _shootingCooldownTimer = 0.0f;
        }
    }
    
    private void Shoot()
    {
        var start = arrowSpawn.position;
        var end = cursor.transform.position;
        for (var i = 0; i < arrowCount; i++)
        {
            var arrow = Instantiate(arrowPrefab, start, Quaternion.identity);
            var spreadX = Random.Range(-arrowSpread, arrowSpread);
            var spreadZ = Random.Range(-arrowSpread, arrowSpread);
            var endSpread = new Vector3(end.x + spreadX, end.y, end.z + spreadZ);
            arrow.GetComponent<ArrowProjectile>().start = start;
            arrow.GetComponent<ArrowProjectile>().end = endSpread;
            arrow.GetComponent<ArrowProjectile>().speed = arrowSpeed;
            arrow.GetComponent<ArrowProjectile>().archHeight = arrowArcHeight;
            arrow.GetComponent<ArrowProjectile>().despawnTime = arrowDespawnTime;
        }
    }
}
