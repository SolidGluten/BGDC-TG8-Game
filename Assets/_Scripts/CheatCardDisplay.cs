using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheatCardDisplay : MonoBehaviour
{
    public GameObject cheatCardObj;
    public Transform cheatCardParent;

    public List<Card> cardList = new List<Card>();

    private void Start()
    {
        cardList = Resources.LoadAll<Card>("Cards/").ToList();

        foreach(var card in cardList)
        {
            var obj = Instantiate(cheatCardObj, cheatCardParent);
            var cardDisplay = obj.GetComponentInChildren<CardDisplay>();
            cardDisplay.card = card;
        }
    }
}