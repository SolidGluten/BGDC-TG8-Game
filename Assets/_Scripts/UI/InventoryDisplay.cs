using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    public List<Card> deckDisplay = new List<Card>();

    public GameObject inventoryCard;

    private void Start()
    {
        CardManager.instance.deck.ForEach((card) =>
        {
            deckDisplay.Add(card);
            Instantiate(card, this.transform);
        });
    }
}
