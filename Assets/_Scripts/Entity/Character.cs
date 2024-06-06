using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Character : Entity
{
    [SerializeField] private bool isCharSelected;
    [SerializeField] private float moveRange;

    private Vector2 moveDir = Vector2.zero;

    private void Update()
    {
        if (Input.GetButtonDown("Vertical"))
        {
            var y_axis = Input.GetAxisRaw("Vertical");
            moveDir = new Vector2(0, y_axis);
            Debug.Log(moveDir);

            var cell = GetAdjacentCell(moveDir);
            if (cell) MoveToCell(cell);
        }
        else if(Input.GetButtonDown("Horizontal"))
        {
            var x_axis = Input.GetAxisRaw("Horizontal");
            moveDir = new Vector2(x_axis, 0);
            Debug.Log(moveDir);

            var cell = GetAdjacentCell(Vector2.right * x_axis);
            if (cell) MoveToCell(cell);
        }
    }
     
    public Cell GetAdjacentCell(Vector2 dir)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, dir, Mathf.Infinity, 1 << 6);
        return hits.Length >= 2 ? hits[1].collider.gameObject.GetComponent<Cell>() : null;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)moveDir * 1f);
    }
#endif

    public override bool ValidateMove(Cell cell)
    {
        return cell != null;
    }
}
