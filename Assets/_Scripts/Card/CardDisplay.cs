using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Card card;
    public Hand hand;

    public int handIndex;

    private bool isHovered;
    private bool isSelected;

    private Image image;
    private Canvas canvas;

    public TextMeshProUGUI card_name;
    public TextMeshProUGUI card_description;
    public TextMeshProUGUI card_cost;

    public Vector2 originalPos;
    public Quaternion originalRot;
    private Vector2 originalScale;

    protected static CardDisplay selectedCard;

    public Vector2 hoverPos;
    [SerializeField] private float hoverSizeMultiplier = 1;
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color selectedColor;

    private void Awake()
    {
        image = GetComponent<Image>();
        canvas = GetComponent<Canvas>();
        image.color = defaultColor;

        originalScale = transform.localScale;
    }

    private void Start()
    {
        card_name.text = card.cardName;
        card_description.text = card.description;
        card_cost.text = card.cost.ToString();
    }

    private void Update()
    {
        if (isHovered && selectedCard == null || (selectedCard != null && selectedCard == this))
        {
            image.color = selectedColor;
            transform.localPosition = hoverPos;
            //transform.localRotation = Quaternion.Euler(0, 0, 0);
            transform.localScale = originalScale * hoverSizeMultiplier;
            canvas.sortingOrder = 100;


        } else
        {
            image.color = defaultColor;
            transform.localPosition = originalPos;
            //transform.localRotation = originalRot;
            transform.localScale = originalScale;
            canvas.sortingOrder = 2;
        }
    }

    public IEnumerator ApplyCard()
    {
        SelectCard();

        CardManager.instance.OnCancelCard += UnselectCard;

        yield return CardManager.instance.PlayCard(handIndex);
        
        CardManager.instance.OnCancelCard -= UnselectCard;
    }

    public void SelectCard()
    {
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
        if (selectedCard == null) StartCoroutine(ApplyCard());
    }

    public void OnPointerEnter(PointerEventData eventData) => isHovered = true;

    public void OnPointerExit(PointerEventData eventData) => isHovered = false;

    private void OnDisable()
    {
        if (selectedCard == this) selectedCard = null;
        CardManager.instance.OnCancelCard -= UnselectCard;
    }
}
