using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRadius = 25f;
    public float spawnDelay = 1f;
    public int maxEnemies = 10;
    public float maxEnemyRadius = 100f;
    
    private float spawnTimer = 0f;
    private GameObject _enemyHolder;
    private Transform _playerTransform;

    private void Start()
    {
        _enemyHolder = GameObject.FindWithTag("Enemy Holder");
        _playerTransform = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;
        if (!(spawnTimer >= spawnDelay)) return;
        spawnTimer = 0f;
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        // If player is in the max enemy radius, don't spawn
        if (Vector3.Distance(transform.position, _playerTransform.position) < maxEnemyRadius)
            return;
        // Find all enemies within maxEnemyRadius
        var enemies = Physics.OverlapSphere(transform.position, maxEnemyRadius, 1 << LayerMask.NameToLayer("Enemies"));
        var enemySameTagCount = enemies.Count(enemy => enemy.CompareTag(enemyPrefab.tag));
        if (enemySameTagCount >= maxEnemies) return;
        
        // Spawn a new enemy
        var spawnPosition = Random.insideUnitSphere * spawnRadius + transform.position;
        spawnPosition.y = transform.position.y;
        var enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        enemy.transform.parent = _enemyHolder.transform;
    }

    // Draw gizmos in the scene view
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 1);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, maxEnemyRadius);
    }
}
