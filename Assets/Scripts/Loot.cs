using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public int value = 1;
    public GameObject gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("Game Manager");
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        gameManager.GetComponent<GameManager>().AddGold(value);
        Destroy(gameObject);
    }
}
