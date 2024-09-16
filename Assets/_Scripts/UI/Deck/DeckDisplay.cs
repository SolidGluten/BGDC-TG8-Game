using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum PileType { Deck, All, Draw, Discard };

public class DeckDisplay : MonoBehaviour
{
    public CardManager cardManager;

    [Space(15)]

    public GameObject cardItem;
    public Transform cardParent;

    [Space(15)]

    public PileType type = PileType.Draw;
    public bool showUpgradable;
    public CharacterType characterType = CharacterType.None;

    private List<GameObject> cardItemList = new List<GameObject>();
    private List<Card> currentPile = new List<Card>();

    private void OnEnable()
    {
        ClearDisplay();
        UpdateDisplay();
    }

    public void ClearDisplay()
    {
        for(int i = cardItemList.Count - 1; i >= 0; i--)
        {
            var item = cardItemList[i];
            Destroy(item.gameObject);
            cardItemList.RemoveAt(i);
        }
        currentPile.Clear();
    }

    public void UpdateDisplay()
    {
        switch (type)
        {
            case PileType.Deck:
                {
                    currentPile = cardManager.currentDeck;
                    break;
                }
            case PileType.Draw:
                {
                    currentPile = cardManager.drawPile.Select(x => x.cardScriptable).ToList();
                    break;
                }
            case PileType.Discard:
                {
                    currentPile = cardManager.discardPile.Select(x => x.cardScriptable).ToList();
                    break;
                }
            case PileType.All:
                {
                    currentPile = Resources.Load<Deck>("AllCards/Deck").cards;
                    break;
                }
        }

        switch (characterType)
        {
            case CharacterType.Knight:
                {
                    currentPile = currentPile.Where(x => x.caster == CharacterType.Knight).ToList();
                    break;
                }
            case CharacterType.Mage:
                {
                    currentPile = currentPile.Where(x => x.caster == CharacterType.Mage).ToList();
                    break;
                }
            default: break;
        }

        if (showUpgradable) currentPile = currentPile.Where(x => x.nextUpgrade != null).ToList();

        foreach (var card in currentPile)
        {
            var obj = Instantiate(cardItem, cardParent);
            var cardDisplay = obj.GetComponentInChildren<CardDisplay>();

            cardItemList.Add(obj);

            //var caster = CharacterManager.instance.GetCharacterByType(card.caster);
            var cardInstance = new CardInstance(card);
            cardDisplay.CardInstance = cardInstance;
        }
    }
}
