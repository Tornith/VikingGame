using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public GameObject deathMenuUI;
    public GameObject player;
    private LevelLoader _levelLoader;
    
    private void Start()
    {
        deathMenuUI.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        _levelLoader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
    }

    public void Update()
    {
        if (!(player.GetComponent<Health>().health <= 0)) return;
        deathMenuUI.SetActive(true);
        
        Time.timeScale = 0.25f;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        _levelLoader.LoadLevelByName("MainMenu");
    }
}
