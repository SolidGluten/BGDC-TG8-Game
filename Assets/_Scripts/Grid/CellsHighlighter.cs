using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellsHighlighter : MonoBehaviour
{
    public GridSystem Grid;

    public List<Cell> HighlightArea(Vector2 pos, float size = 2f)
    {
        var cols = Physics2D.OverlapBoxAll(pos, Vector2.one * size, 45); //Diamond shape
        List<Cell> cells = new List<Cell>();
        foreach (var col in cols)
        {
            var cell = col.GetComponent<Cell>();
            if (cell) cell.Highlight();
            cells.Add(cell);
        }

        return cells;
    }

    [ContextMenu("Un-Highlight All")]
    public void ClearAll()
    {
        for(int i = 0; i < Grid.Width; i++)
        {
            for(int j = 0; j < Grid.Height; j++)
            {
                Grid.Cells[i, j].UnHighlight();
            }
        }
    }
}
