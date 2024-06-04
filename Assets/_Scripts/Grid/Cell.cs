using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]

public class Cell : MonoBehaviour
{
    public Vector2 Pos;
    public Vector2Int Index;
    public int Value;
    public bool isOccupied;
    public GridSystem grid;

    [SerializeField] private GameObject obj;
    public GameObject Obj
    {
        get { return obj; }
        set {
            obj = value;
            this.isOccupied = obj != null;
        }
    }   

    public void Start()
    {
        transform.position = Pos;
    }
}
