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
    private float hoverOffset = 25f;
    private Image image;
    private int siblingIndex;

    [SerializeField] private Color defaultColor;
    [SerializeField] private Color selectedColor;

    private void Awake()
    {
        image = GetComponent<Image>();
        image.color = defaultColor;
        siblingIndex = transform.GetSiblingIndex();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHover = true;
        image.color = selectedColor;
        transform.localPosition += Vector3.up * hoverOffset;
        transform.SetAsLastSibling();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHover= false;
        image.color = defaultColor;
        transform.localPosition -= Vector3.up * hoverOffset;
        transform.SetSiblingIndex(siblingIndex);
    }
}
