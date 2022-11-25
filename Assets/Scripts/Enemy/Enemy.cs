using System;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    
    [Serializable]
    public class Loot
    {
        public GameObject prefab;
        public float weight;
        public int amount;
    }
    
    public abstract class Enemy : MonoBehaviour
    {
        public float viewDistance = 10f;
        public float attackDistance = 5f;
        public float wanderDistance = 10f;
        public GameObject player;
        
        public Loot[] lootTable;
        public GameObject lootHolder;

        [HideInInspector] public float lootSpawnSpeed = 1f;

        protected Health health;
        protected NavMeshAgent agent;

        protected virtual void Start()
        {
            health = GetComponent<Health>();
            agent = GetComponent<NavMeshAgent>();
            
            // If player is not defined, find it
            if (player == null)
            {
                player = GameObject.FindWithTag("Player");
            }
            // If loot holder is not defined, find it
            if (lootHolder == null)
            {
                lootHolder = GameObject.FindWithTag("Loot Holder");
            }
        }
        
        protected virtual void Update()
        {
            if (!health.IsDead()) return;
            DropLoot();
            Destroy(gameObject);
        }
        
        protected virtual void DropLoot()
        {
            var totalWeight = lootTable.Sum(loot => loot.weight);
            var random = UnityEngine.Random.Range(0, totalWeight);
            var currentWeight = 0f;
            foreach (var loot in lootTable)
            {
                currentWeight += loot.weight;
                if (random > currentWeight) continue;
                for (var i = 0; i < loot.amount; i++)
                {
                    var l = Instantiate(loot.prefab, transform.position, Quaternion.identity);
                        
                    l.transform.SetParent(lootHolder.transform);
                    l.GetComponent<Magnetism>().magnet = player;
                        
                    // Give the loot a random velocity and direction if there is a rigidbody
                    var rb = l.GetComponent<Rigidbody>();
                    if (rb == null) continue;
                    var randomDirection = UnityEngine.Random.insideUnitSphere * lootSpawnSpeed;
                    rb.AddForce(randomDirection, ForceMode.Impulse);
                }
                break;
            }
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