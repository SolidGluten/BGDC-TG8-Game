using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellSelector : MonoBehaviour
{
    private const int maxRange = 6; //WARNING: DONT GO OVER MAX RANGE OR PREPARE FOR DEATH

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

    [SerializeField] private CellsHighlighter cellHighlight;
    public bool toggleHover;
    public bool toggleHighlights;
    public HighlightShape highlightShape;

    [SerializeField]
    [Range(0, maxRange)]
    private int highlightRange;
    public int HighlightRange {
        get { return highlightRange; }
        set {
            highlightRange = Math.Clamp(value, 0, maxRange);
        }
    }

    public List<Cell> highlightedCells;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectCell();
        }

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
        if(cellHighlight && toggleHighlights) cellHighlight.ClearAll();

        var pos = GameManager.MousePos;
        var hit = Physics2D.Raycast(pos, Vector3.forward);
        if (!hit) return;

        var cell = hit.collider.gameObject.GetComponent<Cell>();
        if (!cell) return;

        hoveredCell = cell;

        if (cellHighlight && toggleHighlights)
        {
            var cellPos = cell.gameObject.transform.position;
            Debug.Log($"{cell.gameObject} : {cellPos}");

            highlightedCells = cellHighlight.HighlightArea(HoveredCell, highlightRange, highlightShape);
        }
    }

    private void OnDisable()
    {
        highlightedCells.Clear();
    }
}
