using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public GameObject cardObj;
    public List<CardDisplay> cardDisplayInHand = new List<CardDisplay>();

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

        cardDisplay.card = card;
        cardDisplay.hand = this;

        cardDisplayInHand.Add(cardDisplay);

        UpdateHandVisuals();
    }

    public void RemoveCard(int index)
    {
        CardDisplay cardDisplay = cardDisplayInHand[index];
        if (!cardDisplay) return;

        cardDisplayInHand.Remove(cardDisplay);
        Destroy(cardDisplay.gameObject);

        UpdateHandVisuals();
    }

    [ContextMenu("Update Hand Visual")]
    public void UpdateHandVisuals()
    {
        for(int i = 0; i < cardDisplayInHand.Count; i++)
        {
            cardDisplayInHand[i].handIndex = i;
        }

        int cardCount = cardDisplayInHand.Count;

        if (cardCount == 1) {
            cardDisplayInHand[0].transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            cardDisplayInHand[0].transform.localPosition = new Vector3(0f, 0f, 0f);
            return;
        }

        for (int i = 0; i < cardCount; i++)
        {
            //float rotationAngle = (fanSpread * (i - (cardCount - 1) / 2f));
            //cardDisplayInHand[i].transform.localRotation = Quaternion.Euler(0f, 0f, rotationAngle);

            float horizontalOffset = (cardSpacing * (i - (cardCount - 1) / 2f));
            cardDisplayInHand[i].hoverPos = new Vector2(horizontalOffset, hoverLift);

            float normalizedPosition = (2f * i / (cardCount - 1) - 1f);
            float verticalOffset = verticalSpacing * (1 - normalizedPosition * normalizedPosition);
            cardDisplayInHand[i].originalPos = new Vector2(horizontalOffset, verticalOffset);
        }
    }
    
}
