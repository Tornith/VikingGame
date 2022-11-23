using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class StandingEnemy : ShootingEnemy
    {
        private void FixedUpdate()
        {
            if (health.IsDead()) return;
        
            var distance = Vector3.Distance(transform.position, player.transform.position);
            if (!(distance < viewDistance))
            {
                // TODO: Move around randomly
                return;
            }
            // The player is in view range, move towards them
            MoveTowardsPlayer();
            if (!(distance < attackDistance)) return;
            nextFire += Time.deltaTime;
            // The player is in attack range, so fire a projectile
            if (nextFire < fireCooldown) return;
            nextFire = 0f;
            // Use a coroutine to fire the projectile
            StartCoroutine(FireProjectile());
        }

        private void MoveTowardsPlayer()
        {
            // Move towards the player
            agent.SetDestination(player.transform.position);
        }
    }
}
