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
            if (startCell) startCell.RaiseType(CellType.Effect);
            startCell = CellSelector.Instance.HoveredCell;
            if (startCell) startCell.LowerType(CellType.Effect);
        } else if (Input.GetMouseButtonDown(1))
        {
            if (endCell) endCell.RaiseType(CellType.Effect);
            endCell = CellSelector.Instance.HoveredCell;
            if (endCell) endCell.LowerType(CellType.Effect);
        } 

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (path.Any())
                CellsHighlighter.RaiseLayerType(path, CellType.Effect);

            path = Pathfind.FindPath(startCell.index, endCell.index);
            CellsHighlighter.LowerLayerType(path, CellType.Effect);
        }
    }
}
