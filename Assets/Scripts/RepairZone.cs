using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairZone : MonoBehaviour
{
    public GameObject player;
    
    public float repairRate = 0.1f;
    
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            player.GetComponent<Health>().Heal(repairRate * Time.deltaTime);
        }
    }
}
