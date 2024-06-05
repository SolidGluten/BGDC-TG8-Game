using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public List<Card> deck = new List<Card>();
    private List<Card> drawPile = new List<Card>();
    private List<Card> discardPile = new List<Card>();
    private List<Card> exhaustPile = new List<Card>();
    public List<Card> hand = new List<Card>();
    public int handSize = 10;
    public int initialDraw = 5;

    public void ResetDeck()
    {
        hand.Clear();
        drawPile.Clear();
        discardPile.Clear();
        exhaustPile.Clear();
        drawPile.AddRange(deck);
        Shuffle(drawPile);
    }

    public void DrawInitialHand()
    {
        for (int i = 0; i < initialDraw; i++)
        {
            DrawCard();
        }
    }
    public void DrawCard()
    {
        if (drawPile.Count == 0)
        {
            ReshuffleDiscardIntoDrawPile();
        }

        if (drawPile.Count == 0)
        {
            return;
        }

        Card drawnCard = drawPile[0];
        drawPile.RemoveAt(0);
        hand.Add(drawnCard);
    }

    public void DiscardCard(Card card)
    {
        discardPile.Add(card);
    }
    public void ExhaustCard(Card card)
    {
        exhaustPile.Add(card);
    }
    private void ReshuffleDiscardIntoDrawPile()
    {
        drawPile.AddRange(discardPile);
        discardPile.Clear();
        Shuffle(drawPile);
    }

    private void Shuffle(List<Card> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Card temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public void PlayCard(Card card, GameObject target)
    {
        if (hand.Contains(card))
        {
            hand.Remove(card);
            if (card.exhaust)
            {
                ExhaustCard(card);
            }
            else
            {
                DiscardCard(card);
            }
            card.cardEffect();
        }
    }

    public void DiscardHand()
    {
        foreach (Card card in hand)
        {
            DiscardCard(card);
        }
        hand.Clear();
    }
}
