using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairZoneSpawner : MonoBehaviour
{
    public GameObject repairZone;
    public Health health;

    private void LateUpdate()
    {
        repairZone.SetActive(health.health < 100);
    }
}
