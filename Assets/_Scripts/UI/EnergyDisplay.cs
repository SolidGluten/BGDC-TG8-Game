using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnergyDisplay : MonoBehaviour
{
    public TextMeshProUGUI energyText;

    private void Awake()
    {
        energyText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        energyText.text = CardManager.instance.currentEnergy.ToString();
    }
}
