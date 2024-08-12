using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellSelector : MonoBehaviour
{
    public static CellSelector Instance;

    [SerializeField] private Cell selectedCell;
    public Cell SelectedCell
    {
        get { return selectedCell;}
        private set { selectedCell = value; }
    }

    [SerializeField] private Cell hoveredCell;
    public Cell HoveredCell { 
        get { return hoveredCell;  }
        private set { hoveredCell = value; }
    }

    public bool toggleHover;
    public bool toggleHighlights;

    private const int MAX_RADIUS = 6; //WARNING: DONT GO OVER MAX RANGE OR PREPARE FOR DEATH
    private const int MAX_RANGE = 10;

    private Highlighter highlighter = new HighlightSquare();

    public Direction Direction;

    [SerializeField] private HighlightShape shape;
    public HighlightShape Shape
    {
        get { return shape; }
        set
        {
            switch (value)
            {
                case HighlightShape.Square:
                    highlighter = new HighlightSquare(); break;
                case HighlightShape.Cross:
                    highlighter = new HighlightCross(); break;
                case HighlightShape.Diamond:
                    highlighter = new HighlightDiamond(); break;
                case HighlightShape.Line:
                    highlighter = new HighlightLine(); break;
                default: break;
            }
            shape = value;
        }
    }

    //RADIUS
    [SerializeField]
    [Range(0, MAX_RADIUS)]
    private int radius;
    public int Radius
    {
        get { return radius; }
        set
        {
            radius = Mathf.Clamp(value, 0, MAX_RADIUS);
        }
    }

    //RANGE
    [SerializeField]
    [Range(0, MAX_RANGE)]
    private int range;
    public int Range
    {
        get { return range; }
        set
        {
            range = Mathf.Clamp(value, 0, MAX_RANGE);
        }
    }

    public List<Cell> HighlightedCells;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        } else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            SelectCell();

        if (toggleHover) HoverCell();
    }

    private void SelectCell()
    {
        var pos = GameManager.MousePos;
        var hit = Physics2D.Raycast(pos, Vector3.forward);
        SelectedCell = hit.collider?.gameObject.GetComponent<Cell>();
    }

    private void HoverCell()
    {
        var pos = GameManager.MousePos;
        var hit = Physics2D.Raycast(pos, Vector3.forward);
        if (!hit) return;

        var cell = hit.collider.GetComponent<Cell>();
        if (cell)
        {
            hoveredCell = cell;

            if (toggleHighlights)
            {
                HighlightedCells = CellsHighlighter.HighlightArea(hoveredCell.index, Radius, Shape, Range, Direction);
            }
        }

    }

    private void OnDisable()
    {
        HighlightedCells.Clear();
    }
}
