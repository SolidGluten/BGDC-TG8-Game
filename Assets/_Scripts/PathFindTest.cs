using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PathFindTest : MonoBehaviour
{
    public Cell startCell, endCell;
    public List<Cell> path;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (startCell) startCell.SetPath(false);
            startCell = CellSelector.Instance.HoveredCell;
            if (startCell) startCell.SetPath(true);
        } else if (Input.GetMouseButtonDown(1))
        {
            if (endCell) endCell.SetPath(false);
            endCell = CellSelector.Instance.HoveredCell;
            if (endCell) endCell.SetPath(true);
        } 

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (path.Any())
            {
                foreach(Cell cell in path) cell.SetPath(false);
            }

            path = Pathfind.FindPath(startCell.index, endCell.index);
            foreach (Cell cell in path) cell.SetPath(true);
        }
    }
}
