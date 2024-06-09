using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum HighlightShape { Square, Cross, Diamond, Line}
public enum Direction { Left, Right, Up, Down }

public class CellsHighlighter : MonoBehaviour
{
    private const int maxRadius = 6; //WARNING: DONT GO OVER MAX RANGE OR PREPARE FOR DEATH
    private const int maxRange = 10;

    public GridSystem Grid;
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
    [Range(0, maxRadius)]
    private int radius;
    public int Radius
    {
        get { return radius; }
        set
        {
            radius = Mathf.Clamp(value, 0, maxRadius);
        }
    }

    //RANGE
    [SerializeField]
    [Range(0, maxRange)]
    private int range;
    public int Range
    {
        get { return range; }
        set
        {
            range = Mathf.Clamp(value, 0, maxRange);
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

    public List<Cell> HighlightArea(Cell targetCell)
    {
        if (radius < 0) Debug.LogError("Highlight radius can't be negative.");
        if (range < 0) Debug.LogError("Highlight range can't be negative.");

        List<Cell> cells = highlighter.Highlight(targetCell, Radius, Range, Direction);

        foreach(var cell in cells) cell.Highlight();

        return cells;
    }

    [ContextMenu("Un-Highlight All")]
    public void ClearAll()
    {
        for(int i = 0; i < Grid.Width; i++)
        {
            for(int j = 0; j < Grid.Height; j++)
            {
                Grid.Cells[i, j].UnHighlight();
            }
        }
    }
}
