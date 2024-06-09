    using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HighlightSquare : Highlighter
{
    public override List<Cell> Highlight(Cell targetCell, int width, int length, Direction dir = Direction.Right)
    {
        List<Cell> h_cells = new List<Cell>();
        List<Cell> v_cells = new List<Cell>();

        h_cells.Add(targetCell);

        Cell temp_up = targetCell.up;
        for (int i = 0; i < width && temp_up != null; i++)
        {
            HighlightHorizontal(temp_up, width + 1, ref h_cells);
            temp_up = temp_up.up;
        }

        Cell temp_down = targetCell.down;
        for (int i = 0; i < width && temp_down != null; i++)
        {
            HighlightHorizontal(temp_down, width + 1, ref h_cells);
            temp_down = temp_down.down;
        }

        Cell temp_right = targetCell.right;
        for (int i = 0; i < width && temp_right != null; i++)
        {
            HighlightVertical(temp_right, width + 1, ref v_cells);
            temp_right = temp_right.right;
        }

        Cell temp_left = targetCell.left;
        for (int i = 0; i < width && temp_left != null; i++)
        {
            HighlightVertical(temp_left, width + 1, ref v_cells);
            temp_left = temp_left.left;
        }

        h_cells.AddRange(v_cells);
        return h_cells.Distinct().ToList();
    }
}
