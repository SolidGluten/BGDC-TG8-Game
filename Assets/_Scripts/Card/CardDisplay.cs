using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Card card;

    private bool isHover;
    private Image image;
    private int siblingIndex;

    private Vector2 originalPos;
    private Quaternion originalRot;

    public Vector2 hoverPos;
    [SerializeField] private float hoverSizeMultiplier = 1;
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color selectedColor;

    private void Awake()
    {
        image = GetComponent<Image>();
        image.color = defaultColor;
        siblingIndex = transform.GetSiblingIndex();
    }

    private void Start()
    {
        originalPos = transform.localPosition;  
        //originalRot = transform.localRotation;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHover = true;
        image.color = selectedColor;
        transform.localPosition = hoverPos;
        transform.SetAsLastSibling();
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        transform.localScale *= hoverSizeMultiplier;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHover = false;
        image.color = defaultColor;
        transform.localPosition = originalPos;
        transform.SetSiblingIndex(siblingIndex);
        transform.localRotation = originalRot;
        transform.localScale /= hoverSizeMultiplier;
    }
}
