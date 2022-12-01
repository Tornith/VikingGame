using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RepairZoneSpawner : MonoBehaviour
{
    public GameObject repairZone;
    public Health health;
    public TextMeshProUGUI lowHealthText;
    
    private GameManager _gameManager;
    
    private void Start()
    {
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    private void LateUpdate()
    {
        repairZone.SetActive(health.health < health.maxHealth && !_gameManager.gameWon);
        
        lowHealthText.gameObject.SetActive(health.health < health.maxHealth / 2.5f && !_gameManager.gameWon);
    }
}
