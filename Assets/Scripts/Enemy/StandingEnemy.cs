using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Enemy
{
    public class StaticEnemy : MonoBehaviour
    {
        public float viewDistance = 10f;
        public float attackDistance = 5f;
        public GameObject player;
        public GameObject projectilePrefab;
        public GameObject projectileHolder;
        public Transform firePoint;
        public float projectileDamage = 1f;
        public float projectileSpeed = 10f;
        public float projectileSpread = 0.1f;
        public float projectileArch = 0.1f;
        public float fireCooldown = 1f;
        
        private float _nextFire = 0f;
        private Health _health;
        private bool _isprojectileHolderNotNull;

        private void Start()
        {
            _health = GetComponent<Health>();
            _isprojectileHolderNotNull = projectileHolder != null;
        }
        
        private void FixedUpdate()
        {
            if (_health.IsDead()) return;
            
            var distance = Vector3.Distance(transform.position, player.transform.position);
            if (!(distance < viewDistance)) return;
            // The player is in view range, so rotate towards them
            transform.LookAt(player.transform);
            if (!(distance < attackDistance)) return;
            _nextFire += Time.deltaTime;
            // The player is in attack range, so fire a projectile
            if (_nextFire < fireCooldown) return;
            _nextFire = 0f;
            // Use a coroutine to fire the projectile
            StartCoroutine(FireProjectile());
        }
        
        private IEnumerator FireProjectile()
        {
            var start = firePoint.position;
            var end = player.transform.position;
            var projectile = Instantiate(projectilePrefab, start, Quaternion.identity);
            if (_isprojectileHolderNotNull) projectile.transform.parent = projectileHolder.transform;
            var spreadX = Random.Range(-projectileSpread, projectileSpread);
            var spreadZ = Random.Range(-projectileSpread, projectileSpread);
            var endSpread = new Vector3(end.x + spreadX, end.y, end.z + spreadZ);
            projectile.GetComponent<Projectile>().start = start;
            projectile.GetComponent<Projectile>().end = endSpread;
            projectile.GetComponent<Projectile>().speed = projectileSpeed;
            projectile.GetComponent<Projectile>().archHeight = projectileArch;
            projectile.GetComponent<Projectile>().damage = projectileDamage;
            projectile.GetComponent<Projectile>().despawnTime = 0.5f;
            yield return null;
        }
        
        // Show gizmos in the editor for the view and attack ranges
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, viewDistance);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackDistance);
        }
    }
}
