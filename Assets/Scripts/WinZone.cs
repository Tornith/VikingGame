using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinZone : MonoBehaviour
{
    public GameObject player;
    public GameObject winUI;
    public GameObject[] playerUI;
    public Camera winCamera;
    public Camera playerCamera;

    public TextMeshProUGUI finalGold;
    public TextMeshProUGUI finalTime;
    
    private GameManager _gameManager;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        winUI.SetActive(false);
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            winUI.SetActive(true);
            _gameManager.gameWon = true;
            foreach (var obj in playerUI)
            {
                obj.SetActive(false);
            }
            winCamera.enabled = true;
            playerCamera.enabled = false;
            
            finalGold.text = "Gold collected: " + _gameManager.goldAmount;
            finalTime.text = "Time taken: " + FormatTime(_gameManager.timeElapsed);
            
            Time.timeScale = 0;
        }
    }

    private string FormatTime(float seconds)
    {
        // Format the time to a string
        var minutes = Mathf.FloorToInt(seconds / 60);
        var secondsInt = Mathf.FloorToInt(seconds % 60);
        return string.Format("{0:00}:{1:00}", minutes, secondsInt);
    }
}
