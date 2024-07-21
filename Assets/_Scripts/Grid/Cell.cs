using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class Cell : MonoBehaviour
{
    private SpriteRenderer _renderer;

    public Vector2Int index; 
    [SerializeField] private bool isHighlited;

    public Entity occupiedEntity;

    public Color defaultColor;
    public Color highlightedColor;

    public bool isOccupied => occupiedEntity != null;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }   

    public void SetEntity(Entity entity)
    {
        if (entity.occupiedCell && entity.occupiedCell.occupiedEntity != null)
            entity.occupiedCell.occupiedEntity = null;

        entity.transform.position = transform.position;
        entity.occupiedCell = this;
        occupiedEntity = entity;
    }

    public void SetHighlight(bool highlight)
    {
        isHighlited = highlight;
        _renderer.color = isHighlited ? highlightedColor : defaultColor;
    }
}
