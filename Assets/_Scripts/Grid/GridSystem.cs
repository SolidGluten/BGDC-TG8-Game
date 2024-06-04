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
    [SerializeField] private Sprite cellBG;

    private Cell[,] gridArr = new Cell[maxRange, maxRange];

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

                var obj = gridArr[i, j] ? gridArr[i, j].Obj : new GameObject("Cell " + val, typeof(Cell));
                var cell = obj.GetComponent<Cell>();

                //copy value from the associated element from gridArr
                cell.Index = index;
                cell.Value = val;

                //creates a new bg object
                obj.GetComponent<SpriteRenderer>().sprite = cellBG;

                //change the parent transform
                obj.transform.position = pos;
                obj.transform.parent = transform;
            }
        }
    }

    public void AddObj(GameObject obj, Vector2Int pos)
    {
        if (pos.x < 0 || pos.x > width - 1 || 
            pos.y > height - 1 || pos.y < 0)
            Debug.LogWarning("Cell's position is outside the scope of the grid");
        else
            gridArr[pos.x, pos.y].Obj = obj;
    }

    //draws the grid
    private void OnDrawGizmos()
    {
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++){
                var pos = (cellSize + cellGap) * new Vector2(i, j) + gridPos;
                var val = (i * height) + j;

                Gizmos.color = Color.green;
                Handles.color = Color.green;

                if (gridArr[i, j] && !gridArr[i, j].isOccupied)
                {
                    Gizmos.color = Color.red;
                    Handles.color = Color.red;
                }
                
                Handles.Label(pos, val.ToString());
                Gizmos.DrawWireCube(pos, Vector2.one * cellSize);
            }
        }
    }
}


