using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public GameObject cardObj;
    public List<CardInteract> cardsInHand = new List<CardInteract>();

    public CardManager cardManager;

    [SerializeField] private float fanSpread;
    [SerializeField] private float cardSpacing;
    [SerializeField] private float verticalSpacing;
    [SerializeField] private float hoverLift = 25;

    private void Awake()
    {
        cardManager.OnDrawCard += AddCard;
        cardManager.OnPlayCard += RemoveCard;
        //cardManager.OnDiscardCard += RemoveCard;
        cardManager.OnDiscardHand += RemoveAll;
    }

    public void AddCard(CardInstance cardInstance)
    {
        var obj = Instantiate(cardObj, transform.position, Quaternion.identity, transform);
        var cardDisplay = obj.GetComponent<CardDisplay>();
        var cardInteract = obj.GetComponent<CardInteract>();

        cardDisplay.CardInstance = cardInstance;
        cardInteract.hand = this;

        cardsInHand.Add(cardInteract);

        UpdateHandVisuals();
    }

    public void RemoveCard(CardInstance cardInstance)
    {
        CardInteract card = cardsInHand.Find((x) => x.cardDisplay.CardInstance == cardInstance);
        if (!card) return;

        cardsInHand.Remove(card);
        Destroy(card.gameObject);

        UpdateHandVisuals();
    }

    public void RemoveAll()
    {
        var cardObjs = cardsInHand.Select(x => x.gameObject).ToArray();
        foreach(var card in cardObjs) {
            if (card) {
                cardsInHand.Remove(card.GetComponent<CardInteract>());
                Destroy(card); 
            }
        }
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

            var newPosition = new Vector2(horizontalOffset, verticalOffset);
            cardsInHand[i].transform.localPosition = newPosition;
            cardsInHand[i].originalPos = newPosition;

            cardsInHand[i].cardDisplay.defaultSortOrder = i + 2;
        }
    }

    private void OnDisable()
    {
        cardManager.OnDrawCard -= AddCard;
        cardManager.OnPlayCard -= RemoveCard;
        //cardManager.OnDiscardCard -= RemoveCard;
        cardManager.OnDiscardHand -= RemoveAll;
    }

    private void OnDestroy()
    {
        RemoveAll();
    }
}
