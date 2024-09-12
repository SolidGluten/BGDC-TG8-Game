using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckDisplay : MonoBehaviour
{
    public GameObject cardItem;
    public Transform cardParent;
    [HideInInspector] public List<GameObject> cardItemList = new List<GameObject>();

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
            cardDisplay.cardInstance = cardInstance;
        }
    }
}
