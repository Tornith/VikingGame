using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class BoatEnemy : ShootingEnemy
    {
        public int projectileAmount = 5;
        public float projectileTimeSpread = 0.5f;
        
        private NavMeshPath _path;
        private Vector3 _agentPosition;
        private Vector3 _agentTarget;
        private int _pathIter;

        protected override void Start()
        {
            base.Start();
            agent.isStopped = true;
            _path = new NavMeshPath();
        }

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
            _agentTarget = player.transform.position;
            MoveTowardsTarget();
            if (!(distance < attackDistance)) return;
            nextFire += Time.deltaTime;
            // The player is in attack range, so fire a projectile
            if (nextFire < fireCooldown) return;
            nextFire = 0f;
            // Use a coroutine to fire the projectile
            StartCoroutine(FireProjectile(projectileAmount, projectileTimeSpread));
        }

        private void MoveTowardsTarget()
        {
            SetAgentPosition();
            _path = new NavMeshPath();
            agent.CalculatePath(_agentTarget, _path);
            _pathIter = 1;
            agent.isStopped = false;

            if (_path.corners == null || _path.corners.Length == 0)
            {
                print("No path found");
                return;
            }
            if (_pathIter >= _path.corners.Length)
            {
                _agentTarget = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
                agent.isStopped = true;
                print("Reached target");
                return;
            }
            _agentTarget = _path.corners[_pathIter];

            if (!(_agentTarget.x < float.PositiveInfinity))
            {
                print("Cannot reach target");
                return;
            }
            
            var direction = _agentTarget - _agentPosition;
            var newDirection = Vector3.RotateTowards(transform.forward, direction, agent.angularSpeed * Time.deltaTime, 0.0f);
            var newRotation = Quaternion.LookRotation(newDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, agent.angularSpeed * Time.deltaTime);
            var distance = Vector3.Distance(_agentPosition, _agentTarget);
            
            if (distance > agent.radius + 0.1) {
                var movement = transform.forward * (agent.speed * Time.deltaTime);
                agent.Move(movement);
            }
            else
            {
                _pathIter++;
                if (_pathIter < _path.corners.Length) return;
                _agentTarget = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
                agent.isStopped = true;
            }
        }
        
        private void SetAgentPosition()
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(transform.position, out hit, agent.stoppingDistance, agent.areaMask))
            {
                _agentPosition = hit.position;
            }
        }
    }
}
