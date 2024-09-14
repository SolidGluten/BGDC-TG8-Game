using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum PileType { All, Deck, Draw, Discard };

public class DeckDisplay : MonoBehaviour
{
    public GameObject cardItem;
    public Transform cardParent;

    public PileType type = PileType.Deck;
    public bool showUpgradable;

    [HideInInspector] public List<GameObject> cardItemList = new List<GameObject>();

    private void Start()
    {
        List<Card> currentDeck = new List<Card>();

        switch (type)
        {
            case PileType.Deck:
                {
                    currentDeck = CardManager.instance.currentDeck.cards;
                    break;
                }
            case PileType.Draw:
                {
                    currentDeck = CardManager.instance.drawPile.Select(x => x.cardScriptable).ToList();
                    break;
                }
            case PileType.Discard:
                {
                    currentDeck = CardManager.instance.discardPile.Select(x => x.cardScriptable).ToList();
                    break;
                }
        }

        if (showUpgradable) currentDeck.Where(x => x.nextUpgrade);
        if (currentDeck.Any()) UpdateDisplay(currentDeck);
    }

    public void ClearDisplay()
    {
        for(int i = cardItemList.Count - 1; i >= 0; i--)
        {
            var item = cardItemList[i];
            Destroy(item.gameObject);
            cardItemList.RemoveAt(i);
        }
    }

    public void UpdateDisplay(List<Card> cards)
    {
        foreach (var card in cards)
        {
            var obj = Instantiate(cardItem, cardParent);
            var cardDisplay = obj.GetComponentInChildren<CardDisplay>();

            cardItemList.Add(obj);

            var caster = CharacterManager.instance.GetCharacterByType(card.caster);
            var cardInstance = new CardInstance(card, caster);
            cardDisplay.CardInstance = cardInstance;
        }
    }
}
