using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ToggleButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private UnityEvent _onClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        _onClick?.Invoke();
    }
}   
