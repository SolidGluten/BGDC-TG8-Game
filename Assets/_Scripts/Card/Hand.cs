using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public GameObject cardObj;
    public List<CardInteract> cardsInHand = new List<CardInteract>();

    [SerializeField] private float fanSpread;
    [SerializeField] private float cardSpacing;
    [SerializeField] private float verticalSpacing;
    [SerializeField] private float hoverLift = 25;

    private void Awake()
    {
        CardManager.instance.OnDrawCard += AddCard;
        CardManager.instance.OnPlayCard += RemoveCard;
    }

    public void AddCard(Card card)
    {
        var obj = Instantiate(cardObj, transform.position, Quaternion.identity, transform);
        var cardDisplay = obj.GetComponent<CardDisplay>();
        var cardInteract = obj.GetComponent<CardInteract>();

        cardDisplay.card = card;
        cardInteract.hand = this;

        cardsInHand.Add(cardInteract);

        UpdateHandVisuals();
    }

    public void RemoveCard(int index)
    {
        CardInteract card = cardsInHand[index];
        if (!card) return;

        cardsInHand.Remove(card);
        Destroy(card.gameObject);

        UpdateHandVisuals();
    }

    [ContextMenu("Update Hand Visual")]
    public void UpdateHandVisuals()
    {
        for(int i = 0; i < cardsInHand.Count; i++)
        {
            cardsInHand[i].handIndex = i;
        }

        int cardCount = cardsInHand.Count;

        if (cardCount == 1) {
            cardsInHand[0].transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            cardsInHand[0].transform.localPosition = new Vector3(0f, 0f, 0f);
            return;
        }

        for (int i = 0; i < cardCount; i++)
        {
            //float rotationAngle = (fanSpread * (i - (cardCount - 1) / 2f));
            //cardsInHand[i].transform.localRotation = Quaternion.Euler(0f, 0f, rotationAngle);

            float horizontalOffset = (cardSpacing * (i - (cardCount - 1) / 2f));
            cardsInHand[i].hoverPos = new Vector2(horizontalOffset, hoverLift);

            float normalizedPosition = (2f * i / (cardCount - 1) - 1f);
            float verticalOffset = verticalSpacing * (1 - normalizedPosition * normalizedPosition);
            cardsInHand[i].originalPos = new Vector2(horizontalOffset, verticalOffset);
        }
    }
    
}
