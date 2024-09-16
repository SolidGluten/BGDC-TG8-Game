using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PileCounter : MonoBehaviour
{
    public TextMeshProUGUI text_counter;
    public CardManager cardManager;

    public PileType type = PileType.All;

    //private void Awake()
    //{
    //    text_counter = GetComponent<TextMeshProUGUI>();
    //}

    private void Update()
    {
        if (text_counter)
        {
            switch (type) { 
                case PileType.Discard:
                    {
                        text_counter.text = cardManager.DiscardPileCount.ToString();
                        break;
                    }
                case PileType.Draw:
                    {
                        text_counter.text = cardManager.DrawPileCount.ToString();
                        break;
                    }
                case PileType.All:
                case PileType.Deck:
                default:
                    {
                        text_counter.text = "0";
                        break;
                    }
            }
        } else
        {
            Debug.LogAssertion("Text is not assigned to the counter yet.");
        }
    }
}
