using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform arrowSpawn;
    public GameObject cursor;
    public GameObject projectileHolder;
    
    public float arrowSpeed = 10f;
    public int arrowCount = 10;
    public float arrowSpread = 0.1f;
    public float arrowSpreadDistanceIncrease = 0.1f;
    public float arrowSpreadTime = 0.1f;
    public float arrowArcHeight = 1f;
    public float arrowDespawnTime = 5f;
    
    public float shootingCooldown = 0.5f;
    
    private float _shootingCooldownTimer = 0.0f;
    private bool _isprojectileHolderNotNull;

    private void Start()
    {
        _isprojectileHolderNotNull = projectileHolder != null;
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && _shootingCooldownTimer <= 0.0f)
        {
            _shootingCooldownTimer = shootingCooldown;
            StartCoroutine(ShootArrows());
        }
        _shootingCooldownTimer -= Time.deltaTime;
        if (_shootingCooldownTimer < 0.0f)
        {
            _shootingCooldownTimer = 0.0f;
        }
    }

    private IEnumerator ShootArrows()
    {
        var end = cursor.transform.position;
        // Spawn arrowCount arrows and wait for arrowSpreadTime seconds between each arrow
        for (var i = 0; i < arrowCount; i++)
        {
            StartCoroutine(SpawnArrow(end));
            yield return new WaitForSeconds(arrowSpreadTime);
        }
    }
    
    private IEnumerator SpawnArrow(Vector3 end)
    {
        var start = arrowSpawn.position;
        var distance = Vector3.Distance(start, end);
        var arrow = Instantiate(arrowPrefab, start, Quaternion.identity);
        if (_isprojectileHolderNotNull) arrow.transform.parent = projectileHolder.transform;
        var spreadX = Random.Range(-arrowSpread, arrowSpread) * distance * arrowSpreadDistanceIncrease;
        var spreadZ = Random.Range(-arrowSpread, arrowSpread) * distance * arrowSpreadDistanceIncrease;
        var endSpread = new Vector3(end.x + spreadX, end.y, end.z + spreadZ);
        arrow.GetComponent<Projectile>().start = start;
        arrow.GetComponent<Projectile>().end = endSpread;
        arrow.GetComponent<Projectile>().speed = arrowSpeed;
        arrow.GetComponent<Projectile>().archHeight = arrowArcHeight;
        arrow.GetComponent<Projectile>().despawnTime = arrowDespawnTime;
        yield return null;
    }
}
