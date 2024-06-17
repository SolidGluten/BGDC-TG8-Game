using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
    using UnityEditor;
#endif

public class GridSystem : MonoBehaviour
{
    public const int MAX_RANGE = 30;

    //Grid width
    [Range(1, MAX_RANGE)]
    [SerializeField] private int width;
    public int Width { get { return width; } }

    //Grid height
    [Range(1, MAX_RANGE)]
    [SerializeField] private int height;
    public int Height { get { return height; } }

    [SerializeField] private Vector2 gridPos;
    [SerializeField] private int cellSize = 1;
    [SerializeField] private float cellGap = 0f;
    [SerializeField] private GameObject cellObj;

    public Cell[,] Cells { get; } = new Cell[MAX_RANGE, MAX_RANGE];

    private void Awake()
    {

        RegenerateGrid();
    }

    [ContextMenu("Regenerate Grid")]
    public void RegenerateGrid()
    {
        //destroy previous cells
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i).gameObject;
            #if UNITY_EDITOR
                EditorCustomUtils.DestroyOnEdit(child);
            #else
                Destroy(child);
            #endif

        }

        //instantiate cell objs
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                var index = new Vector2Int(i, j);
                var pos = (cellSize + cellGap) * (Vector2)index + gridPos;
                var val = (i * height) + j;

                var obj = Instantiate(cellObj, pos, Quaternion.identity, transform);
                //copy value from the associated element from Cells
                obj.name = "Cell" + val;

                var cell = obj.GetComponent<Cell>();
                cell.Index = index;
                cell.Value = val;

                Cells[i, j] = cell;
            }
        }

        //set references to adjacent cells
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (j < height - 1) Cells[i, j].up = Cells[i, j + 1]; //UP
                if (j > 0) Cells[i, j].down = Cells[i, j - 1]; //DOWN
                if (i < width - 1) Cells[i, j].right = Cells[i + 1, j]; //RIGHT
                if (i > 0) Cells[i, j].left = Cells[i - 1, j]; //LEFT
            }
        }
    } 

    public void AddObj(GameObject obj, Vector2Int index)
    {
        Cells[index.x, index.y].Obj = obj;
        obj.transform.position = Cells[index.x, index.y].transform.position;
    }

    public Cell GetCell(Vector2Int index)
    {
        return Cells[index.x, index.y];
    }

    public bool ValidatePos(Vector2Int index)
    {
        return Cells[index.x, index.y] != null;
    }

    //draws the grid
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++){
                var pos = (cellSize + cellGap) * new Vector2(i, j) + gridPos;
                var val = (i * height) + j;

                if (Cells[i, j])
                {
                    Gizmos.color = Cells[i, j].isOccupied ? Color.red : Color.green;
                    Handles.color = Cells[i, j].isOccupied ? Color.red : Color.green;
                }

                
                    Handles.Label(pos, val.ToString());

                Gizmos.DrawWireCube(pos, Vector2.one * cellSize);   
            }
        }
    }
#endif
}


