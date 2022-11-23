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
            
            // Set up the NavMesh path and start the agent
            _path = new NavMeshPath();
            agent.CalculatePath(_agentTarget, _path);
            _pathIter = 1;
            agent.isStopped = false;

            // If the path has no corners, then the agent is already at the target
            if (_path.corners == null || _path.corners.Length == 0)
            {
                return;
            }
            // If the iteration is greater than the number of corners, then the agent has reached the target
            if (_pathIter >= _path.corners.Length)
            {
                _agentTarget = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
                agent.isStopped = true;
                return;
            }
            
            // Set the current agent target to the next corner
            _agentTarget = _path.corners[_pathIter];

            // Stop if the next corner is invalid
            if (!(_agentTarget.x < float.PositiveInfinity))
            {
                return;
            }
            
            // Rotate the enemy towards the target
            var direction = _agentTarget - _agentPosition;
            var newDirection = Vector3.RotateTowards(transform.forward, direction, agent.angularSpeed * Time.deltaTime, 0.0f);
            var newRotation = Quaternion.LookRotation(newDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, agent.angularSpeed * Time.deltaTime);
            
            // Move the enemy towards the target
            var distance = Vector3.Distance(_agentPosition, _agentTarget);
            
            // If the next point is currently further than the agent's radius, move towards it
            if (distance > agent.radius + 0.1f) {
                // TODO: Acceleration and collision avoidance
                var movement = transform.forward * (agent.speed * Time.deltaTime);
                agent.Move(movement);
            }
            // Otherwise we have reached the next point
            else
            {
                // Increment the path iterator
                _pathIter++;
                // If we have reached the end of the path, stop the agent
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
        
        private void OnDrawGizmos()
        {
            if (_path == null || _path.corners == null || _path.corners.Length == 0) return;
            for (var i = 0; i < _path.corners.Length - 1; i++)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawLine(_path.corners[i], _path.corners[i + 1]);
                Gizmos.DrawSphere(_path.corners[i], 1f);
            }
        }
    }
}
