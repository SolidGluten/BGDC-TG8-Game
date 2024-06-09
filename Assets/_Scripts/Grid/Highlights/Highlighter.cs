using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Highlighter
{
    public abstract List<Cell> Highlight(Cell targetCell, int width, int length, Direction dir = Direction.Right);

    public void HighlightHorizontal(Cell targetCell, int length, ref List<Cell> result)
    {
        if (length == 0) return;
        result.Add(targetCell);
        if (targetCell.right) HighlightHorizontal(targetCell.right, length - 1, ref result);
        if (targetCell.left) HighlightHorizontal(targetCell.left, length - 1, ref result);
    }

    public void HighlightVertical(Cell targetCell, int length, ref List<Cell> result)
    {
        if (length == 0) return;
        result.Add(targetCell);
        if (targetCell.up) HighlightVertical(targetCell.up, length - 1, ref result);
        if (targetCell.down) HighlightVertical(targetCell.down, length - 1, ref result);
    }

}
