using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    [SerializeField] private int width = 5;
    [SerializeField] private int height = 5;
    [SerializeField] private int cellSize = 1;
    [SerializeField] private float cellGap = 0f;
    [SerializeField] private Sprite cellSprite;
    [SerializeField] private Vector2 currPos;

    public Cell[,] gridArr;

    private void Start()
    {
        for(int i = 0; i < 5; i++)
            gridArr[0, i].Obj = new GameObject();
    }

    private void OnValidate()
    {
        gridArr = new Cell[width, height];
        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
                gridArr[i, j] = new Cell((cellSize + cellGap) * new Vector2(i, j) + currPos, ((i * width) + j));
    }

    private void OnDrawGizmos()
    {
        for(int i = 0; i < width; i++)
            for(int j = 0; j < height; j++){
                Gizmos.color = gridArr[i, j].occupied ? Color.red : Color.green;
                Handles.Label(gridArr[i, j].Pos, gridArr[i, j].Value.ToString());
                Gizmos.DrawWireCube(gridArr[i, j].Pos, Vector2.one * cellSize);
            }
    }
}

[Serializable]
public class Cell
{
    public Vector2 Pos { get; }
    public int Value { get; }

    private GameObject obj;
    public GameObject Obj
    {
        get
        {
            return obj;
        }
        set
        {
            obj = value;
            occupied = obj != null;
        }
    }

    public bool occupied;

    public Cell(Vector2 pos, int val)
    {
        this.Pos = pos;
        this.Value = val;
    }
}

