using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public GameObject cardObj;
    public List<GameObject> cardObjInHand = new List<GameObject>();

    [SerializeField] private float fanSpread;
    [SerializeField] private float cardSpacing;
    [SerializeField] private float verticalSpacing;


    private void Start()
    {
        for(int i = 0; i < 10; i++)
        {
            AddCard();
        }
    }

    public void AddCard()
    {
        var obj = Instantiate(cardObj, transform.position, Quaternion.identity, transform);
        cardObjInHand.Add(obj);

        UpdateHandVisuals();
    }

    //private void Update()
    //{
    //    UpdateHandVisuals();
    //}

    public void UpdateHandVisuals()
    {
        int cardCount = cardObjInHand.Count;

        if (cardCount == 1) {
            cardObjInHand[0].transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            cardObjInHand[0].transform.localPosition = new Vector3(0f, 0f, 0f);
            return;
        }

        for (int i = 0; i < cardCount; i++)
        {
            float rotationAngle = (fanSpread * (i - (cardCount - 1) / 2f));
            cardObjInHand[i].transform.localRotation = Quaternion.Euler(0f, 0f, rotationAngle);

            float horizontalOffset = (cardSpacing * (i - (cardCount - 1) / 2f));

            float normalizedPosition = (2f * i / (cardCount - 1) - 1f);
            float verticalOffset = verticalSpacing * (1 - normalizedPosition * normalizedPosition);
            cardObjInHand[i].transform.localPosition = new Vector2(horizontalOffset, verticalOffset);
        }
    }
    
}
