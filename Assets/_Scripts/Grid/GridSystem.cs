using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    [SerializeField] private Vector2Int gridSize;
    [SerializeField] private Vector2 currPos;
    [SerializeField] private int cellSize = 1;
    [SerializeField] private float cellGap = 0f;
    [SerializeField] private Sprite cellSprite;

    public Cell[,] gridArr;

    private void OnValidate()
    {
        gridArr = new Cell[gridSize.x, gridSize.y];
    }

    private void Awake()
    {
        for(int i = 0; i < gridSize.x; i++)
        {
            for(int j = 0; j < gridSize.y; j++)
            {
                var index = new Vector2Int(i, j);
                var pos = (cellSize + cellGap) * (Vector2)index + currPos;
                var val = (i * gridSize.y) + j;

                //creates a new cell object
                var obj = new GameObject("Cell " + val, typeof(Cell));
                var cell = obj.GetComponent<Cell>();
                obj.GetComponent<SpriteRenderer>().sprite = cellSprite;

                //change the parent transform
                obj.transform.parent = transform;

                //copy value from the associated element from gridArr
                cell.Pos = pos;
                cell.Index = index;
                cell.Value = val;

                gridArr[i, j] = cell;
            }
        }
    }

    public void AddObject(GameObject obj, Vector2Int pos)
    {
        if (pos.x < 0 || pos.x > gridSize.x - 1 || pos.y > gridSize.y - 1 || pos.y < 0)
            Debug.LogWarning("Object's position is outside the scope of the grid");
        else
            gridArr[pos.x, pos.y].Obj = obj;
    }

    //draws the grid
    private void OnDrawGizmos()
    {
        for(int i = 0; i < gridSize.x; i++)
        {
            for(int j = 0; j < gridSize.y; j++){
                var pos = (cellSize + cellGap) * new Vector2(i, j) + currPos;
                var val = (i * gridSize.y) + j;

                if (gridArr[i, j])
                {
                    Gizmos.color = gridArr[i, j].isOccupied ? Color.red : Color.green;
                }
                Handles.Label(pos, val.ToString());
                Gizmos.DrawWireCube(pos, Vector2.one * cellSize);
            }
        }
    }
}


