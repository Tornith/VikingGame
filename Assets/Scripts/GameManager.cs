using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int goldAmount;
    public float timeElapsed;
    public int bossesDefeated;
    public bool gameWon;

    public GameObject winTrigger;
    public GameObject winNote;

    public TextMeshProUGUI bossesDefeatedText;

    public void AddGold(int amount)
    {
        goldAmount += amount;
    }
    
    public void RemoveGold(int amount)
    {
        goldAmount -= amount;
    }

    private void FixedUpdate()
    {
        if (!gameWon) timeElapsed += Time.deltaTime;
    }
    
    public void BossDefeated()
    {
        bossesDefeated++;
        bossesDefeatedText.text = $"{bossesDefeated}/4 Artifacts Recovered";

        if (bossesDefeated != 4) return;
        winTrigger.SetActive(true);
        winNote.SetActive(true);
    }
}
