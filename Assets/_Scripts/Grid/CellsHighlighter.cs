using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum HighlightShape { Square, Cross, Diamond, Line, Circle}
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

        cells.RemoveAll(cell => cell == null);

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
            case HighlightShape.Circle:
                highlighter = new HighlightCircle(); break;
            default: break;
        }
        return highlighter;
    }

    public static void ClearAll()
    {
        foreach(var cell in GridSystem.Instance.cellList.Values)
        {
            if (cell) cell.Types = EnumFlags.ClearFlags(cell.Types);
        }
    }

    public static void ClearAllType(CellType type)
    {
        foreach (var cell in GridSystem.Instance.cellList.Values)
        {
            if (cell) cell.Types = EnumFlags.LowerFlag(cell.Types, type);
        }
    }

    // OLD
    public static void SetTypes(List<Cell> cells, CellType type, bool set)
    {
        foreach (var cell in cells)
            if (cell) cell.Types = EnumFlags.SetFlag(cell.Types, type, set);
    }

    // NEW
    public static void RaiseLayerType(List<Cell> cells, CellType type)
    {
        foreach (var cell in cells) cell?.RaiseType(type);
    }

    public static void LowerLayerType(List<Cell> cells, CellType type)
    {
        foreach (var cell in cells) cell?.LowerType(type);
    }

    public static void ResetLayerType()
    {
        foreach (var cell in GridSystem.Instance.cellList.Values) cell?.ResetType();
    }

    public static Direction GetDirection(Vector2 from, Vector2 to)
    {
        Vector2 normDir = (to - from).normalized;
        int x = Math.Abs(normDir.x) > Math.Abs(normDir.y) ? (int)Math.Round(normDir.x) : 0;
        int y = Math.Abs(normDir.x) < Math.Abs(normDir.y) ? (int)Math.Round(normDir.y) : 0;
        Vector2Int dirVector = new Vector2Int(x, y);

        Direction dir = Direction.Right;

        if (dirVector == Vector2Int.left)
            dir = Direction.Left;
        else if (dirVector == Vector2Int.right)
            dir = Direction.Right;
        else if (dirVector == Vector2Int.up)
            dir = Direction.Up;
        else
            dir = Direction.Down;

        return dir;
    }
}
