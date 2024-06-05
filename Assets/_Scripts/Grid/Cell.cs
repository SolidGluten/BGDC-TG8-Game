using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Collections;
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

    public void Highlight()
    {
        spriteRenderer.color = Color.green;
    }
    
    public void UnHighlight()
    {
        spriteRenderer.color = Color.red;
    }

    public void DestroySelf()
    {
        EditorCustomUtils.DestroyOnEdit(gameObject);
    }
}
