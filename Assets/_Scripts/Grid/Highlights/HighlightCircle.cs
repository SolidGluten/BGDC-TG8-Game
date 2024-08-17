using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HighlightCircle : Highlighter
{
    public override List<Cell> Highlight(Vector2Int startIndex, int width, int length, Direction dir = Direction.Right)
    {
        // Midpoint circle algorithm

        List<Vector2Int> cellPos = new List<Vector2Int>();

        int x = width, y = 0;

        for (int i = -x; i <= x; i++)
            cellPos.Add(new Vector2Int(i + startIndex.x, y + startIndex.y));

        cellPos.Add(new Vector2Int(y + startIndex.x, x + startIndex.y));
        cellPos.Add(new Vector2Int(y + startIndex.x, -x + startIndex.y));

        int P = 1 - width;
        while (x > y)
        {
            y++;

            if (P <= 0)
                P = P + 2 * y + 1;
            else
            {
                x--;
                P = P + 2 * y - 2 * x + 1;
            }

            if (x < y)
                break;

            for(int i = -x; i <= x; i++)
                cellPos.Add(new Vector2Int(i + startIndex.x, y + startIndex.y));
            for(int i = -x; i <= x; i++)
                cellPos.Add(new Vector2Int(i + startIndex.x, -y + startIndex.y));

            if (x != y)
            {
                for (int i = -y; i <= y; i++)
                    cellPos.Add(new Vector2Int(i + startIndex.x, x + startIndex.y));
                for (int i = -y; i <= y; i++)
                    cellPos.Add(new Vector2Int(i + startIndex.x, -x + startIndex.y));
            }
        }

        cellPos = cellPos.Distinct().ToList();

        List<Cell> cells = new List<Cell>();
        foreach (var pos in cellPos) {
            var cell = GridSystem.Instance.GetCell(pos);
            if (cell) cells.Add(cell);
        }

        return cells;
    }
}
