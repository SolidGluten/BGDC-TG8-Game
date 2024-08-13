using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum HighlightShape { Square, Cross, Diamond, Line}
public enum Direction { Left, Right, Up, Down }

public class CellsHighlighter : MonoBehaviour
{
    public static List<Cell> HighlightArea(Vector2Int startIndex, int radius, HighlightShape shape = HighlightShape.Diamond, int range = 0, Direction dir = Direction.Right)
    {
        if (radius < 0)
        {
            Debug.LogError("Highlight radius can't be negative.");
            return null;
        }
        if (range < 0)
        {
            Debug.LogError("Highlight range can't be negative.");
            return null;
        }

        Highlighter highlighter = GetHighlighter(shape);
        List<Cell> cells = highlighter.Highlight(startIndex, radius, range, dir);

        return cells;
    }

    private static Highlighter GetHighlighter(HighlightShape shape)
    {
        Highlighter highlighter = new HighlightDiamond();
        switch (shape)
        {
            case HighlightShape.Square:
                highlighter = new HighlightSquare(); break;
            case HighlightShape.Cross:
                highlighter = new HighlightCross(); break;
            case HighlightShape.Diamond:
                highlighter = new HighlightDiamond(); break;
            case HighlightShape.Line:
                highlighter = new HighlightLine(); break;
            default: break;
        }
        return highlighter;
    }

    public static void ClearAll()
    {
        for (int i = 0; i < GridSystem.Instance.Height; i++)
        {
            for (int j = 0; j < GridSystem.Instance.Width; j++)
            {
                var cell = GridSystem.Instance.GetCell(new Vector2Int(j, i));
                if (cell) cell.Types = EnumFlags.ClearFlags(cell.Types);
            }
        }
    }

}
