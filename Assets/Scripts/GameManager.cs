using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int goldAmount;
    
    public void AddGold(int amount)
    {
        goldAmount += amount;
    }
    
    public void RemoveGold(int amount)
    {
        goldAmount -= amount;
    }
}
