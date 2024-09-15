using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DrawPileCounter : MonoBehaviour
{
    public TextMeshProUGUI text_counter;
    public CardManager cardManager;

    private void Awake()
    {
        text_counter = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        text_counter.text = cardManager.DrawPileCount.ToString();
    }
}
