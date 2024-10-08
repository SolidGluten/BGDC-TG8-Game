using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pathfind
{
    public static List<Cell> FindPath(Vector2Int startPos, Vector2Int endPos)
    {
        GridSystem.Instance.ResetCost();

        var startCell = GridSystem.Instance.GetCell(startPos);
        var endCell = GridSystem.Instance.GetCell(endPos);
        List<Cell> path = new List<Cell>();

        if (startCell == null || endCell == null) {
            Debug.LogWarning("Invalid start and end");
            return path;
        }

        List<Cell> openList = new List<Cell>();
        List<Cell> closedList = new List<Cell>();

        openList.Add(startCell);

        while (openList.Any())
        {
            var current = openList[0];
            foreach (Cell cell in openList)
            {
                if (cell.F < current.F || (cell.F == current.F && cell.H < current.H)) 
                { 
                    current = cell; 
                }
            }

            openList.Remove(current);
            closedList.Add(current);

            if (current == endCell)
            {
                var curr = current;
                while (curr != startCell) {
                    path.Add(curr);
                    curr = curr.parent;
                }

                path.Reverse();
                return path;
            }

            foreach(Cell child in current.GetAdjacentCells())
            {
                if (closedList.Contains(child) || (child != endCell && child.isOccupied)) continue;

                int newCostToChild = current.G + Heuristic(current.index, child.index);
                if (newCostToChild < child.G || !openList.Contains(child))
                {
                    child.SetG(newCostToChild);
                    child.SetH(Heuristic(child.index, endCell.index));
                    child.SetF();
                    child.parent = current;

                    if (!openList.Contains(child))
                    {
                        openList.Add(child);
                    }
                }
            }
        }

        return path;
    }

    public static int Heuristic(Vector2Int A, Vector2Int B)
    {
        int dstX = Mathf.Abs(B.x - A.x);
        int dstY = Mathf.Abs(B.y - A.y);

        return 10 * Mathf.Abs(dstX - dstY);
    }
}
