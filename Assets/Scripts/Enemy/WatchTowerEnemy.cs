using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class WatchTowerEnemy : ShootingEnemy
    {
        public int projectileAmount = 5;
        public float projectileTimeSpread = 0.5f;
        private void FixedUpdate()
        {
            if (health.IsDead()) return;
        
            var distance = Vector3.Distance(transform.position, player.transform.position);
            if (!(distance < viewDistance)) return;
            if (!(distance < attackDistance)) return;
            nextFire += Time.deltaTime;
            // The player is in attack range, so fire a projectile
            if (nextFire < fireCooldown) return;
            nextFire = 0f;
            // Use a coroutine to fire the projectile
            StartCoroutine(FireProjectile(projectileAmount, projectileTimeSpread));
        }
    }
}
