using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class Cell : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public GridSystem grid;

    public Vector2Int Index;
    public int Value;

    public bool isOccupied;
    public bool isHighlited;

    public Cell up;
    public Cell down;
    public Cell left;
    public Cell right;

    [SerializeField] private GameObject obj;
    public GameObject Obj
    {
        get { return obj; }
        set {
            obj = value;
            isOccupied = obj != null;
        }
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }   

    public Cell SetObject(GameObject _obj)
    {
        Obj = _obj;
        if(_obj != null) _obj.transform.position = this.transform.position;
        return this;
    }

    public void Highlight()
    {
        spriteRenderer.color = Color.green;
    }
    
    public void UnHighlight()
    {
        spriteRenderer.color = Color.red;
    }
}
