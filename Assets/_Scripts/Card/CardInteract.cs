using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CardDisplay))]
public class CardInteract : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Hand hand;

    public CardDisplay cardDisplay { get; private set; }

    public Vector2 originalPos;
    public Quaternion originalRot;
    private Vector2 originalScale;

    protected static CardInteract selectedCard;

    public Image image;

    public int handIndex;

    private bool isHovered;
    private bool isSelected;

    public Vector2 hoverPos;
    [SerializeField] private float hoverSizeMultiplier = 1;

    //[SerializeField] private Color defaultColor;
    //[SerializeField] private Color hoverColor;
    //[SerializeField] private Color selectedColor;

    private void Awake()
    {
        image = GetComponent<Image>();
        cardDisplay = GetComponent<CardDisplay>();

        //image.color = defaultColor;
        originalScale = transform.localScale;
    }

    private void Update()
    {
        //if (isSelected)
        //    image.color = selectedColor;
        //else if (isHovered)
        //    image.color = hoverColor;
        //else
        //    image.color = defaultColor;
    }

    public IEnumerator ApplyCard()
    {
        CardManager.instance.OnCancelCard += UnselectCard;

        yield return CardManager.instance.PlayCard(cardDisplay.CardInstance);

        CardManager.instance.OnCancelCard -= UnselectCard;
    }

    public void SelectCard()
    {
        Debug.Log(cardDisplay.CardInstance.cardScriptable.cardName + "is Selected");
        isSelected = true;
        selectedCard = this;
    }

    public void UnselectCard()
    {
        isSelected = false;
        selectedCard = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SelectCard();
        if (selectedCard == this) 
            StartCoroutine(ApplyCard());
    }

    public void OnPointerEnter(PointerEventData eventData) {
        isHovered = true;
        transform.localPosition = hoverPos;
        //transform.localRotation = Quaternion.Euler(0, 0, 0);
        transform.localScale = originalScale * hoverSizeMultiplier;
        cardDisplay.SetSortingOrder(100);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
        transform.localPosition = originalPos;
        //transform.localRotation = originalRot;
        transform.localScale = originalScale;
        cardDisplay.ResetSortingOrder();
    }

    private void OnDisable()
    {
        if (selectedCard == this) selectedCard = null;
        CardManager.instance.OnCancelCard -= UnselectCard;
    }
}
