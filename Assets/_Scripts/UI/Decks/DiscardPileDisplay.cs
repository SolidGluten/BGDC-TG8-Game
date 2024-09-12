using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(DeckDisplay))]
public class DiscardPileDisplay : MonoBehaviour
{
    private DeckDisplay deckDisplay;
    public CardManager cardManager;

    private void Awake()
    {
        deckDisplay = GetComponent<DeckDisplay>();    
    }

    private void OnEnable()
    {
        var discardCards = cardManager.discardPile?.Select(x => x.cardScriptable).ToList();
        deckDisplay.UpdateDisplay(discardCards);
    }

    private void OnDisable()
    {
        deckDisplay.ClearDisplay();
    }
}
