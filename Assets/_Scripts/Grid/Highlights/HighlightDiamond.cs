using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HighlightDiamond : Highlighter
{
    public override List<Cell> Highlight(Vector2Int startIndex, int width, int length, Direction dir = Direction.Right)
    {
        List<Cell> cells = new List<Cell>();

        for (int i = width; i >= -width; i--)
        {
            for (int j = -(width - Mathf.Abs(i)); j <= (width - Mathf.Abs(i)); j++)
            {
                var cellPos = new Vector2Int(j, i);
                var cell = GridSystem.Instance.GetCell(startIndex + cellPos);
                if (cell) cells.Add(cell);
                Debug.Log(cell);
            }
        }

        return cells;
    }

}
