using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
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
    [SerializeField] private bool isPath;

    public Entity occupiedEntity;

    public Color defaultColor;
    public Color highlightedColor;
    public Color pathColor;

    public bool isOccupied => occupiedEntity != null;

    public int F { get; private set; }
    public int G { get; private set; }
    public int H { get; private set; }

    public TextMeshPro F_text;
    public TextMeshPro G_text;
    public TextMeshPro H_text;

    [HideInInspector] public Cell parent;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        SetG(0);
        SetH(0);
        SetF();
    }

    public List<Cell> GetAdjacentCells()
    {
        List<Cell> cells = new List<Cell>();
        for (float i = 0; i < 2; i += 0.5f)
        {
            var cellIdx = new Vector2Int((int)Mathf.Sin(i * Mathf.PI), (int)Mathf.Cos(i * Mathf.PI));
            var cell = GridSystem.Instance.GetCell(index + cellIdx);
            if (cell) cells.Add(cell);
        }
        return cells;
    }

    public void SetG(int g)
    {
        G = g;
        G_text.text = G.ToString();
        SetF();
    }

    public void SetH(int h)
    {
        H = h;
        H_text.text = H.ToString();
        SetF();
    }

    public void SetF() {
        F = G + H;
        F_text.text = F.ToString();
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

    public void SetPath(bool path)
    {
        isPath = path;
        _renderer.color = path ? pathColor : defaultColor; 
    }
}
