using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellSelector : MonoBehaviour
{
    [SerializeField] private Cell selectedCell;
    [SerializeField] private CellsHighlighter cellHighlight;

    public Cell SelectedCell
    {
        get { return selectedCell;}
        private set { selectedCell = value; }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectCell();
        }
    }

    private void SelectCell()
    {
        if(cellHighlight) cellHighlight.ClearAll();

        var pos = GameManager.MousePos;
        var hit = Physics2D.Raycast(pos, Vector3.forward);
        if (!hit) return;

        var cell = hit.collider.gameObject.GetComponent<Cell>();
        if (!cell) return;
        SelectedCell = cell;

        if (cellHighlight)
        {
            var cellPos = cell.gameObject.transform.position;
            Debug.Log(cell.gameObject + ": " + cellPos);
            cellHighlight.HighlightArea(cellPos, 3f);
        }
    }
}
