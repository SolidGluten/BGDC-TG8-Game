using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum HighlightShape { Square, Cross, Diamond, Line}
public enum Direction { Left, Right, Up, Down }

public class CellsHighlighter : MonoBehaviour
{
    private const int MAX_RADIUS = 6; //WARNING: DONT GO OVER MAX RANGE OR PREPARE FOR DEATH
    private const int MAX_RANGE = 10;

    private Highlighter highlighter = new HighlightSquare();

    public Direction Direction;

    [SerializeField] private HighlightShape shape;
    public HighlightShape Shape {
        get { return shape; } 
        set { 
            switch (value) {
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
            shape = value;
        }
    }

    //RADIUS
    [SerializeField]
    [Range(0, MAX_RADIUS)]
    private int radius;
    public int Radius
    {
        get { return radius; }
        set
        {
            radius = Mathf.Clamp(value, 0, MAX_RADIUS);
        }
    }

    //RANGE
    [SerializeField]
    [Range(0, MAX_RANGE)]
    private int range;
    public int Range
    {
        get { return range; }
        set
        {
            range = Mathf.Clamp(value, 0, MAX_RANGE);
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        switch (Shape)
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
    }
#endif

    public List<Cell> HighlightArea(Vector2Int startIndex)
    {
        if (radius < 0) Debug.LogError("Highlight radius can't be negative.");
        if (range < 0) Debug.LogError("Highlight range can't be negative.");

        List<Cell> cells = highlighter.Highlight(startIndex, Radius, Range, Direction);

        foreach(var cell in cells) cell.SetHighlight(true);

        return cells;
    }

    [ContextMenu("Un-Highlight All")]
    public void ClearAll()
    {
        for(int i = 0; i < GridSystem.Instance.Height; i++)
        {
            for(int j = 0; j < GridSystem.Instance.Width; j++)
            {
                var cell = GridSystem.Instance.cellList[new Vector2Int(j, i)];
                if (cell) cell.SetHighlight(false);
            }
        }
    }
}
