using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellsHighlighter : MonoBehaviour
{
    public GridSystem Grid;

    public List<Cell> HighlightArea(Vector2 pos, float size = 2f)
    {
        //Diamond shape
        //var cols = Physics2D.OverlapBoxAll(pos, Vector2.one * size, 45); 

        //Cross shape
        //var cols_v = Physics2D.OverlapAreaAll(pos + Vector2.up * (size/2), pos + Vector2.down * (size / 2));
        //var cols_h = Physics2D.OverlapAreaAll(pos + Vector2.left * (size / 2), pos + Vector2.right * (size / 2));
        //var cols = new List<Collider2D>();
        //cols.AddRange(cols_v);
        //cols.AddRange(cols_h);

        //Cirle shape
        var cols = Physics2D.OverlapCircleAll(pos, size);

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
