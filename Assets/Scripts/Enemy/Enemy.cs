using System;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public abstract class Enemy : MonoBehaviour
    {
        public float viewDistance = 10f;
        public float attackDistance = 5f;
        public float wanderDistance = 10f;
        public GameObject player;
        
        protected Health health;
        protected NavMeshAgent agent;

        protected virtual void Start()
        {
            health = GetComponent<Health>();
            agent = GetComponent<NavMeshAgent>();
        }

        // Show gizmos in the editor for the view and attack ranges
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, viewDistance);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackDistance);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, wanderDistance);
        }
    }
}