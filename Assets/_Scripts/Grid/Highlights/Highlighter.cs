using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Highlighter
{
    public abstract List<Cell> Highlight(Vector2Int startIndex, int width, int length, Direction dir = Direction.Right);

    public void HighlightHorizontal(Vector2Int startIndex, int length, ref List<Cell> result)
    {
        var cell = GridSystem.Instance.GetCell(startIndex);
        if (!cell) return;

        if (length == 0) {
            result.Add(cell);
            return;
        }

        for(int i = -length; i <= length; i++)
        {
            var cell = GridSystem.Instance.GetCell(new Vector2Int(startIndex.x + i, startIndex.y));
            if (cell) result.Add(cell);
        }
    }

    public void HighlightVertical(Vector2Int startIndex, int length, ref List<Cell> result)
    {
        var cell = GridSystem.Instance.GetCell(startIndex);
        if (!cell) return;

        if (length == 0)
        {
            result.Add(cell);
            return;
        }

        for (int i = -length; i <= length; i++)
        {
            var cell = GridSystem.Instance.GetCell(new Vector2Int(startIndex.x, startIndex.y + i));
            if (cell) result.Add(cell);
        }
    }

}
