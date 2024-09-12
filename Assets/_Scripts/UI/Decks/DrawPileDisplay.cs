using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(DeckDisplay))]
public class DrawPileDisplay : MonoBehaviour
{
    private DeckDisplay deckDisplay;
    public CardManager cardManager;

    private void Awake()
    {
        deckDisplay = GetComponent<DeckDisplay>();
    }

    private void OnEnable()
    {
        var drawCards = cardManager.drawPile?.Select(x => x.cardScriptable).ToList();
        deckDisplay.UpdateDisplay(drawCards);
    }

    private void OnDisable()
    {
        deckDisplay.ClearDisplay();
    }
}