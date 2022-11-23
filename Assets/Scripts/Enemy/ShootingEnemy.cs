using System.Collections;
using UnityEngine;

namespace Enemy
{
    public abstract class ShootingEnemy : Enemy
    {
        public GameObject projectilePrefab;
        public GameObject projectileHolder;
        public Transform firePoint;
        public float projectileDamage = 1f;
        public float projectileSpeed = 10f;
        public float projectileSpread = 0.1f;
        public float projectileArch = 0.1f;
        public float fireCooldown = 1f;

        protected float nextFire = 0f;
        protected bool isprojectileHolderNotNull;
        
        protected override void Start()
        {
            base.Start();
            isprojectileHolderNotNull = projectileHolder != null;
        }
        
        protected IEnumerator FireProjectile()
        {
            var start = firePoint.position;
            var end = player.transform.position;
            var projectile = Instantiate(projectilePrefab, start, Quaternion.identity);
            if (isprojectileHolderNotNull) projectile.transform.parent = projectileHolder.transform;
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
        
        protected IEnumerator FireProjectile(int amount, float spreadTime)
        {
            for (var i = 0; i < amount; i++)
            {
                StartCoroutine(FireProjectile());
                yield return new WaitForSeconds(spreadTime);
            }
        }
    }
}