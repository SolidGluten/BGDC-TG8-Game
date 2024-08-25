using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CardDisplay))]
public class CardAdder : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public CardDisplay cardDisplay { get; private set; }
    [SerializeField] private float hoverSizeMultiplier = 2;
    private Vector3 originalScale;

    private void Awake()
    {
        cardDisplay = GetComponent<CardDisplay>();
    }

    private void Start()
    {
        cardDisplay.defaultSortOrder = 100;
        cardDisplay.ResetSortingOrder();
        originalScale = transform.localScale;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        CardManager.instance.AddCardToHand(cardDisplay.card);
    } 

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = originalScale * hoverSizeMultiplier;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalScale;
    }
}
