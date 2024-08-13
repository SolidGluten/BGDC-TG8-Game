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
            if (startCell) EnumFlags.SetFlag(startCell.Types, CellType.Effect, false);
            startCell = CellSelector.Instance.HoveredCell;
            if (startCell) EnumFlags.SetFlag(startCell.Types, CellType.Effect, false);
        } else if (Input.GetMouseButtonDown(1))
        {
            if (endCell) EnumFlags.SetFlag(endCell.Types, CellType.Effect, false);
            endCell = CellSelector.Instance.HoveredCell;
            if (endCell) EnumFlags.SetFlag(endCell.Types, CellType.Effect, false);
        } 

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (path.Any())
            {
                foreach(Cell cell in path) EnumFlags.SetFlag(cell.Types, CellType.Effect, false);
            }

            path = Pathfind.FindPath(startCell.index, endCell.index);
            foreach (Cell cell in path) EnumFlags.SetFlag(cell.Types, CellType.Effect, false);
        }
    }
}
