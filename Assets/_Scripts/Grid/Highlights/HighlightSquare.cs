    using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HighlightSquare : Highlighter
{
    public override List<Cell> Highlight(Vector2Int startIndex, int width, int length, Direction dir = Direction.Right)
    {
        List<Cell> cells = new List<Cell>();

        for (int i = -width/2; i < width/2; i++)
        {
            for (int j = -width/2; j < width/2; j++)
            {
                var cell = GridSystem.Instance.GetCell(startIndex + new Vector2Int(j, i));
                if (cell) cells.Add(cell);
            }
        }

        return cells;
    }
}
