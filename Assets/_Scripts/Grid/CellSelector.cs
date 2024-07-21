using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellSelector : MonoBehaviour
{
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

    public List<Cell> HighlightedCells;

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
        if (cellHighlight && toggleHighlights) cellHighlight.ClearAll();

        var pos = GameManager.MousePos;
        var hit = Physics2D.Raycast(pos, Vector3.forward);
        if (!hit) return;

        var cell = hit.collider.GetComponent<Cell>();
        if (cell)
        {
            hoveredCell = cell;

            if (cellHighlight && toggleHighlights)
            {
                HighlightedCells = cellHighlight.HighlightArea(hoveredCell.index);
            }
        }

    }

    private void OnDisable()
    {
        HighlightedCells.Clear();
    }
}
