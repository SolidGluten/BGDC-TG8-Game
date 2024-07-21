using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightLine : Highlighter
{
    public override List<Cell> Highlight(Vector2Int startIndex, int width, int length, Direction dir = Direction.Right)
    {
        List<Cell> cells = new List<Cell>();
        
        for (int i = 0; i < length; i++)
        {
            for (int j = -width; j <= width; j++)
            {
                var cellPos = Vector2Int.zero;
                switch (dir){
                    case Direction.Up: {
                            cellPos += new Vector2Int(j, i);
                            break; 
                    }
                    case Direction.Down: {
                            cellPos += new Vector2Int(j, -i);
                            break; 
                    }
                    case Direction.Right: {
                            cellPos += new Vector2Int(i, j);
                            break; 
                    }
                    case Direction.Left: {
                            cellPos += new Vector2Int(-i, j);
                            break; 
                    }
                }
                var cell = GridSystem.Instance.GetCell(startIndex + cellPos);
                cells.Add(cell);
            }
        }

        return cells;
    }
}
