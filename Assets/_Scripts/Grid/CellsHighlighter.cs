using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum HighlightShape { Square, Cross, Diamond}

public class CellsHighlighter : MonoBehaviour
{
    public GridSystem Grid;

    public List<Cell> HighlightArea(Cell targetCell, int radius = 2, HighlightShape shape = HighlightShape.Diamond)
    {
        if (radius < 0)
        {
            Debug.LogError("Highlight radius can't be negative.");    
        }

        List<Cell> cells = new List<Cell>();

        switch (shape)
        {
            case HighlightShape.Square:
                cells = HighlightSquare(targetCell, radius); break;
            case HighlightShape.Cross:
                cells = HighlightCross(targetCell, radius); break;
            case HighlightShape.Diamond:
                cells = HighlightDiamond(targetCell, radius); break;
            default: break;
        } 

        foreach(var cell in cells)
        {
            cell.Highlight();
        }

        return cells;
    }

    private List<Cell> HighlightDiamond(Cell targetCell, int radius)
    {
        List<Cell> h_cells = new List<Cell>();
        List<Cell> v_cells = new List<Cell>();

        h_cells.Add(targetCell);
        Cell temp_up = targetCell.up;

        for (int i = 0; i < radius && temp_up != null; i++)
        {
            HighlightHorizontal(temp_up, radius - i, ref h_cells);
            temp_up = temp_up.up;
        }

        Cell temp_down = targetCell.down;
        for (int i = 0; i < radius && temp_down != null; i++)
        {
            HighlightHorizontal(temp_down, radius - i, ref h_cells);
            temp_down = temp_down.down;
        }

        Cell temp_right = targetCell.right;
        for (int i = 0; i < radius && temp_right != null; i++)
        {
            HighlightVertical(temp_right, radius - i, ref v_cells);
            temp_right = temp_right.right;
        }

        Cell temp_left = targetCell.left;
        for (int i = 0; i < radius && temp_left != null; i++)
        {
            HighlightVertical(temp_left, radius - i, ref v_cells);
            temp_left = temp_left.left;
        }

        h_cells.AddRange(v_cells);
        return h_cells.Distinct().ToList();
    }

    private List<Cell> HighlightSquare(Cell targetCell, int radius)
    {
        List<Cell> h_cells = new List<Cell>();
        List<Cell> v_cells = new List<Cell>();

        h_cells.Add(targetCell);

        Cell temp_up = targetCell.up;
        for (int i = 0; i < radius && temp_up != null; i++)
        {
            HighlightHorizontal(temp_up, radius + 1, ref h_cells); 
            temp_up = temp_up.up;
        }

        Cell temp_down = targetCell.down;
        for (int i = 0; i < radius && temp_down != null; i++)
        {
            HighlightHorizontal(temp_down, radius + 1, ref h_cells);
            temp_down = temp_down.down;
        }

        Cell temp_right = targetCell.right;
        for (int i = 0; i < radius && temp_right != null; i++)
        {
            HighlightVertical(temp_right, radius + 1, ref v_cells); 
            temp_right = temp_right.right;
        }

        Cell temp_left = targetCell.left;
        for (int i = 0; i < radius && temp_left != null; i++)
        {
            HighlightVertical(temp_left, radius + 1, ref v_cells);
            temp_left = temp_left.left;
        }

        h_cells.AddRange(v_cells);
        return h_cells.Distinct().ToList();
    }

    private List<Cell> HighlightCross(Cell targetCell, int radius)
    {
        List<Cell> h_cells = new List<Cell>();
        List<Cell> v_cells = new List<Cell>();

        h_cells.Add(targetCell);
        HighlightHorizontal(targetCell, radius + 1, ref h_cells);
        HighlightVertical(targetCell, radius + 1, ref v_cells);

        h_cells.AddRange(v_cells);
        return h_cells.Distinct().ToList();
    }

    private void HighlightHorizontal(Cell targetCell, int length, ref List<Cell> result)
    {
        if (length == 0) return;
        result.Add(targetCell);
        if(targetCell.right) HighlightHorizontal(targetCell.right, length - 1, ref result);
        if(targetCell.left) HighlightHorizontal(targetCell.left, length - 1, ref result);
    }

    private void HighlightVertical(Cell targetCell, int length, ref List<Cell> result)
    {
        if (length == 0) return;
        result.Add(targetCell);
        if (targetCell.up) HighlightVertical(targetCell.up, length - 1, ref result);
        if(targetCell.down) HighlightVertical(targetCell.down, length - 1, ref result);
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
