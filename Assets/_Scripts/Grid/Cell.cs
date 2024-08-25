using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TMPro;
using UnityEngine;

[Flags] public enum CellType { 
    None                = 0,
    Enemy_Detection     = 1 << 0,
    Enemy_MaxRange      = 1 << 1,
    Enemy_TargetRange   = 1 << 2,
    Enemy_Attack        = 1 << 3,
    Range               = 1 << 4, 
    Effect              = 1 << 5, 
    Path                = 1 << 6,
};

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class Cell : MonoBehaviour
{
    private SpriteRenderer _renderer;

    public SpriteRenderer highlightRenderer;
    public float highlightTransparency;

    [SerializeField] private CellType types;
    public CellType Types {
        get { return types; }
        set {
            types = value;
            var type = CellType.None;
            switch (type)
            {
                case CellType.Range:
                    _renderer.color = rangeColor; break;
                case CellType.Effect:
                    _renderer.color = effectColor; break;
                case CellType.Path:
                    _renderer.color = pathColor; break;
                case CellType.Enemy_Detection:
                    _renderer.color = enemyDetectionColor; break;
                case CellType.Enemy_MaxRange:
                    _renderer.color = enemyMaxRangeColor; break;
                case CellType.Enemy_TargetRange:
                    _renderer.color = enemyTargetRangeColor; break;
                case CellType.Enemy_Attack:
                    _renderer.color = enemyAttackColor; break;
                case 0:
                default:
                    _renderer.color = defaultColor; break;
            }
        }
    }

    public Dictionary<CellType, int> layerTypes = new Dictionary<CellType, int>
    {
        { CellType.Enemy_Detection, 0 },
        { CellType.Enemy_MaxRange, 0 },
        { CellType.Enemy_TargetRange, 0 },
        { CellType.Enemy_Attack, 0 },
        { CellType.Range, 0 },
        { CellType.Effect, 0 },
        { CellType.Path, 0 },
    };

    public float overlapColorDifference = 0f;

    public Vector2Int index; 
    public Entity occupiedEntity;

    public Color defaultColor = Color.gray;
    public Color rangeColor = Color.green; 
    public Color effectColor = Color.red;
    public Color pathColor = Color.blue;
    public Color enemyDetectionColor = Color.cyan;
    public Color enemyMaxRangeColor = Color.magenta;
    public Color enemyTargetRangeColor = Color.white;
    public Color enemyAttackColor = Color.yellow;

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
        //Types = EnumFlags.SetFlag(Types, CellType.None, true);
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

    public void RaiseType(CellType type)
    {
        layerTypes[type]++;
        UpdateStyle();
    }

    public void LowerType(CellType type)
    {
        if (layerTypes[type] != 0)
        {
            layerTypes[type]--;
        }
        UpdateStyle();
    }
    
    public void ResetType()
    {
        var keys = new List<CellType>(layerTypes.Keys);
        foreach (var key in keys)
            layerTypes[key] = 0;
        UpdateStyle();
    }

    public void UpdateStyle()
    {
        var enabledTypes = layerTypes.Where(x => x.Value > 0);

        if (!enabledTypes.Any()) {
            highlightRenderer?.gameObject.SetActive(false);
            return;
        }

        highlightRenderer.gameObject.SetActive(true);

        var highestOrder = enabledTypes.Last();

        var type = highestOrder.Key;
        var color = defaultColor;

        switch (type)
        {
            case CellType.Range:
                color = rangeColor; break;
            case CellType.Effect:
                color = effectColor; break;
            case CellType.Path:
                color = pathColor; break;
            case CellType.Enemy_Detection:
                color = enemyDetectionColor; break;
            case CellType.Enemy_MaxRange:
                color = enemyMaxRangeColor; break;
            case CellType.Enemy_TargetRange:
                color = enemyTargetRangeColor; break;
            case CellType.Enemy_Attack:
                color = enemyAttackColor; break;
            default:
                color = defaultColor; break;
        }

        float H, S, V;
        Color.RGBToHSV(color, out H, out S, out V);
        V -= (highestOrder.Value - 1) * overlapColorDifference;
        var newColor = Color.HSVToRGB(H, S, V);
        newColor.a = highlightTransparency;
        highlightRenderer.color = newColor;
        
    }
}
