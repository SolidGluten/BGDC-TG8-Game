using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HighlightCross : Highlighter
{
    public override List<Cell> Highlight(Vector2Int startIndex, int width, int length, Direction dir = Direction.Right)
    {
        List<Cell> h_cells = new List<Cell>();
        List<Cell> v_cells = new List<Cell>();

        HighlightHorizontal(startIndex, width + 1, ref h_cells);
        HighlightVertical(startIndex, width + 1, ref v_cells);

        h_cells.AddRange(v_cells);
        return h_cells.Distinct().ToList();
    }
}
