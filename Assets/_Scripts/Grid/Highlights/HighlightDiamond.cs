using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HighlightDiamond : Highlighter
{
    public override List<Cell> Highlight(Vector2Int startIndex, int width, int length, Direction dir = Direction.Right)
    {
        List<Cell> h_cells = new List<Cell>();
        List<Cell> v_cells = new List<Cell>();

        for (int i = -width/2; i < width/2; i++)
        {
            for (int j = 0; j < i % width; j++)
            {
                var cellPos = new Vector2Int(j, i);
                var cell = GridSystem.Instance.GetCell(startIndex + cellPos);
            }
        }

        h_cells.AddRange(v_cells);
        return h_cells.Distinct().ToList();
    }

}
