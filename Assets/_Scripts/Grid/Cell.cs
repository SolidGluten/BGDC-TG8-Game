using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

[Flags] public enum CellType { 
    None = 0,
    Range = 1, 
    Effect = 2, 
    Path = 4
};

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class Cell : MonoBehaviour
{
    private SpriteRenderer _renderer;

    [SerializeField] private CellType types;
    public CellType Types {
        get { return types; }
        set {
            types = value;
            var type = EnumFlags.GetHighestSetFlag(value);
            switch (type)
            {
                case CellType.Range:
                    _renderer.color = rangeColor; break;
                case CellType.Effect:
                    _renderer.color = effectColor; break;
                case CellType.Path:
                    _renderer.color = pathColor; break;
                case 0:
                default:
                    _renderer.color = defaultColor; break;
            }
        }
    }

    public Vector2Int index; 
    public Entity occupiedEntity;

    public Color defaultColor = Color.gray;
    public Color rangeColor = Color.green;
    public Color effectColor = Color.red;
    public Color pathColor = Color.blue;

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
        Types = EnumFlags.SetFlag(Types, CellType.None, true);
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
}
