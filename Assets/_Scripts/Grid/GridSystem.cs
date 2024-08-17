using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;


#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof (CellSelector))]
[RequireComponent(typeof (CellsHighlighter))]
public class GridSystem : MonoBehaviour
{
    public const int MAX_RANGE = 30;
    public static GridSystem Instance;

    [SerializeField] 
    public Vector2 gridPos;

    //Grid width
    [SerializeField]
    public int width;

    //Grid height
    [SerializeField]
    public int height;

    [Space(15)]

    [SerializeField] private int cellSize = 1;
    [SerializeField] private float cellGap = 0f;
    [SerializeField] private GameObject cellObj;

    public bool isCostShown = true;
    public Dictionary<Vector2Int, Cell> cellList = new Dictionary<Vector2Int, Cell>();

    [Space(15)]

    public Vector2Int[] characterSpawnPositions;
    public Vector2Int[] enemySpawnPositions;

    private void Awake()
    {
        //Singleton
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        } else
        {
            Instance = this;
        }

        RegenerateGrid();
        ShowCost(isCostShown);
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
            cellList.Clear();

        }

        //instantiate cell objs
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                var index = new Vector2Int(j, i);
                var pos = (cellSize + cellGap) * (Vector2)index + gridPos;
                var val = (i * width) + j;

                var obj = Instantiate(cellObj, pos, Quaternion.identity, transform);
                //copy value from the associated element from Cells
                obj.name = "Cell " + val;

                var cell = obj.GetComponent<Cell>();
                cell.index = index;

                cellList.Add(index, cell);
            }
        }
    }

    public Cell GetCell(Vector2Int index)
    {
        if (cellList.TryGetValue(index, out Cell value))
            return value;
        return null;
    }

    //path finding stuffs
    public void ResetCost()
    {
        foreach (KeyValuePair<Vector2Int, Cell> cell in cellList)
        {
            cell.Value.SetG(0);
            cell.Value.SetH(0);
            cell.Value.SetF();
        }
    }

    public void ShowCost(bool isShow)
    {
        isCostShown = isShow;
        foreach (KeyValuePair<Vector2Int, Cell> cell in cellList)
        {
            cell.Value.F_text.enabled = isCostShown;
            cell.Value.G_text.enabled = isCostShown;
            cell.Value.H_text.enabled = isCostShown;
        }
    }

    //draws the grid
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        for(int i = 0; i < height; i++)
        {
            for(int j = 0; j < width; j++){
                var pos = (cellSize + cellGap) * new Vector2(j, i) + gridPos;
                var val = (i * width) + j;

                Handles.Label(pos, val.ToString());
                Gizmos.DrawWireCube(pos, Vector2.one * cellSize);   
            }
        }
    }
#endif
}


