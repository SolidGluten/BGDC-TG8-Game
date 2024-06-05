using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class GridSystem : MonoBehaviour
{
    public const int maxRange = 30;

    [Range(1, maxRange)]
    [SerializeField] private int width;
    [Range(1, maxRange)]
    [SerializeField] private int height;

    [SerializeField] private Vector2 gridPos;
    [SerializeField] private int cellSize = 1;
    [SerializeField] private float cellGap = 0f;
    [SerializeField] private GameObject cellObj;

    private Cell[,] gridArr = new Cell[maxRange, maxRange];

    private void Awake()
    {
        RegenerateGrid();
    }

    [ContextMenu("Regenerate Grid")]
    public void RegenerateGrid() 
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i).gameObject;
            EditorCustomUtils.DestroyOnEdit(child);
        }

        //cells background
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                var index = new Vector2Int(i, j);
                var pos = (cellSize + cellGap) * (Vector2)index + gridPos;
                var val = (i * height) + j;

                var obj = Instantiate(cellObj, pos, Quaternion.identity, transform);
                //copy value from the associated element from gridArr
                obj.name = "Cell" + val;

                var cell = obj.GetComponent<Cell>();
                cell.Index = index;
                cell.Value = val;

                gridArr[i, j] = cell;
            }
        }
    }

    public void AddObj(GameObject obj, Vector2Int pos)
    {
        if (pos.x < 0 || pos.x > width - 1 || 
            pos.y > height - 1 || pos.y < 0)
            Debug.LogWarning("Cell's position is outside the scope of the grid");
        
        if (gridArr[pos.x, pos.y])
        {
            gridArr[pos.x, pos.y].Obj = obj;
        }
            
    }

    //draws the grid
    private void OnDrawGizmos()
    {
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++){
                var pos = (cellSize + cellGap) * new Vector2(i, j) + gridPos;
                var val = (i * height) + j;

                if (gridArr[i, j])
                {
                    Gizmos.color = gridArr[i, j].isOccupied ? Color.red : Color.green;
                    Handles.color = gridArr[i, j].isOccupied ? Color.red : Color.green;
                }

                Handles.Label(pos, val.ToString());
                Gizmos.DrawWireCube(pos, Vector2.one * cellSize);   
            }
        }
    }
}


