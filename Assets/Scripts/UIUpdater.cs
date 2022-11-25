using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class UIUpdater : MonoBehaviour
{
    public Health playerHealth;
    public GameManager gameManager;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI goldText;
    
    private void Update()
    {
        healthText.text = $"Health: {math.ceil(playerHealth.health).ToString(CultureInfo.InvariantCulture)}";
        goldText.text = $"Gold: {gameManager.goldAmount.ToString(CultureInfo.InvariantCulture)}";
    }
}
