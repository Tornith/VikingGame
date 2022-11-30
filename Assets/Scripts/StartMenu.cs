using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject startMenuUI;

    private void Start()
    {
        startMenuUI.SetActive(true);
        Time.timeScale = 0.25f;
    }
    
    void Update()
    {
        if (!startMenuUI.activeSelf) return;
        if (!Input.anyKey) return;
        Play();
    }

    public void Play()
    {
        startMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }
}
