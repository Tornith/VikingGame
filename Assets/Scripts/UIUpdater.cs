using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class UIUpdater : MonoBehaviour
{
    public Health playerHealth;
    public TextMeshProUGUI healthText;
    
    private void Update()
    {
        healthText.text = $"Health: {math.ceil(playerHealth.health).ToString(CultureInfo.InvariantCulture)}";
    }
}
