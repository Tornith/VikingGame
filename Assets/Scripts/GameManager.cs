using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int goldAmount;
    public float timeElapsed;
    public int bossesDefeated;
    
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
        timeElapsed += Time.deltaTime;
    }
    
    public void BossDefeated()
    {
        bossesDefeated++;
    }
}
