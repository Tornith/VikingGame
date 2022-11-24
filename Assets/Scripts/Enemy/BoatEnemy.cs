using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class BoatEnemy : ShootingEnemy
    {
        public int projectileAmount = 5;
        public float projectileTimeSpread = 0.5f;
        
        [HideInInspector] public float updateDistance = 2f;
        
        private Rigidbody _rigidbody;
        private NavMeshPath _path;
        private Vector3 _agentPosition;
        private Vector3 _agentVelocity;
        private Vector3 _agentTarget;
        private Vector3 _agentNext;
        private bool _isMoving;
        private int _pathIter = 1;


        protected override void Start()
        {
            base.Start();
            agent.isStopped = true;
            _path = new NavMeshPath();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (health.IsDead()) return;

            // If there is currently a path and the agent has moved far enough, update the path
            if (_path?.corners?.Length > 0 && Vector3.Distance(_path.corners[0], _agentPosition) > updateDistance)
            {
                SetPath();
            }

            // If there currently isn't a path and the agent has some residual velocity, continue moving it
            if (!_isMoving && _agentVelocity.magnitude > 0.001f)
            {
                // Calculate the decaying velocity with the drag coefficient
                var decayedVelocity = _agentVelocity * Mathf.Exp(-_rigidbody.drag * Time.fixedDeltaTime);
                agent.Move(_agentVelocity);
                _agentVelocity = decayedVelocity;
            }

            var distance = Vector3.Distance(transform.position, player.transform.position);
            if (!(distance < viewDistance))
            {
                // If there is already a path, continue following it
                if (_path != null && _pathIter < _path.corners.Length)
                {
                    MoveTowardsTarget();
                    return;
                }
                // Otherwise pick a random point on the NavMesh within the wanderDistance and set it as the destination
                var randomDirection = Random.insideUnitSphere * wanderDistance;
                _agentTarget = transform.position + randomDirection;
                SetPath();
                return;
            }
            // The player is in view range, move towards them
            var playerPos = player.transform.position;
            // If the player position has changed a bit or if the agent has moved away from the first corner of the path, recalculate the path
            if (Vector3.Distance(playerPos, _agentTarget) > updateDistance)
            {
                _agentTarget = playerPos;
                SetPath();
            }
            MoveTowardsTarget();
            if (!(distance < attackDistance)) return;
            nextFire += Time.deltaTime;
            // The player is in attack range, so fire a projectile
            if (nextFire < fireCooldown) return;
            nextFire = 0f;
            // Use a coroutine to fire the projectile
            StartCoroutine(FireProjectile(projectileAmount, projectileTimeSpread));
        }

        private void SetPath()
        {
            SetAgentPosition();
            _path = new NavMeshPath();
            agent.CalculatePath(_agentTarget, _path);
            _pathIter = 1;
            agent.isStopped = false;
        }

        private void MoveTowardsTarget()
        {
            SetAgentPosition();

            var distanceToTarget = Vector3.Distance(_agentPosition, _agentTarget);
            // If the distance to the final target is less than the agent's stopping distance, then the agent has reached the target
            if (distanceToTarget < agent.stoppingDistance)
            {
                _isMoving = false;
                _pathIter++;
                return;
            }
            
            // If the path has no corners, then the agent is already at the target
            if (_path.corners == null || _path.corners.Length == 0)
            {
                _isMoving = false;
                return;
            }
            // If the iteration is greater than the number of corners, then the agent has reached the target
            if (_pathIter >= _path.corners.Length)
            {
                _agentNext = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
                agent.isStopped = true;
                _isMoving = false;
                return;
            }
            
            // Set the current agent target to the next corner
            _agentNext = _path.corners[_pathIter];

            // Stop if the next corner is invalid
            if (!(_agentNext.x < float.PositiveInfinity))
            {
                _isMoving = false;
                return;
            }

            // Rotate the enemy towards the target
            var direction = _agentNext - _agentPosition;
            var newDirection = Vector3.RotateTowards(transform.forward, direction, agent.angularSpeed * Time.deltaTime, 0.0f);
            var newRotation = Quaternion.LookRotation(newDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, agent.angularSpeed * Time.deltaTime);

            // Move the enemy towards the next point
            var distance = Vector3.Distance(_agentPosition, _agentNext);
            
            
            // If the next point is currently further than the agent's radius, move towards it
            if (distance > agent.radius + 0.1f) {
                _isMoving = true;
                // Accelerate the agent
                var desiredMovement = transform.forward * (agent.speed * Time.deltaTime);
                _agentVelocity = Vector3.Lerp(_agentVelocity, desiredMovement, agent.acceleration * Time.deltaTime);
                agent.Move(_agentVelocity);
            }
            // Otherwise we have reached the next point
            else
            {
                _isMoving = false;
                // Increment the path iterator
                _pathIter++;
                // Check if we still have points to go to
                if (_pathIter < _path.corners.Length) return;
                // If we have reached the end of the path, stop the agent
                _agentNext = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
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
                Gizmos.DrawWireSphere(_agentNext, 2f);
                Gizmos.DrawWireSphere(_agentTarget, 5f);
            }
        }
    }
}
