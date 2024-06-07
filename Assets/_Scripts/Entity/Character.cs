using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Character : Entity
{
    public bool isActive;
    private Vector2 moveDir = Vector2.zero;
    private Cell cellDest;

    private void Update()
    {
        if (!isActive) return;
        if (currMovePoints > 0)
        {
            if (Input.GetButtonDown("Vertical"))
            {
                var y_axis = Input.GetAxisRaw("Vertical");
                moveDir = new Vector2(0, y_axis);
                Debug.Log(moveDir);

                var cell = GetAdjacentCell(moveDir);
                if (cell) cellDest = TakeStep(cell);
            }
            else if(Input.GetButtonDown("Horizontal"))
            {
                var x_axis = Input.GetAxisRaw("Horizontal");
                moveDir = new Vector2(x_axis, 0);
                Debug.Log(moveDir);
                var cell = GetAdjacentCell(Vector2.right * x_axis);
                if (cell) cellDest = TakeStep(cell);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!cellDest) return;
            MoveToCell(cellDest);
            cellDest = null;
            currMovePoints = MaxMovePoints;

        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            ResetMove();
        }
    }

    public Cell GetAdjacentCell(Vector2 dir)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, dir, Mathf.Infinity, 1 << 6);
        return hits.Length >= 2 ? hits[1].collider.gameObject.GetComponent<Cell>() : null;
    }

    public Cell TakeStep(Cell cell)
    {
        if (cell.isOccupied) return null;
        else
        {
            transform.position = cell.transform.position;
            currMovePoints--;
            return cell;
        }
    }

    public void ResetMove()
    {
        transform.position = currCell.transform.position;
        currMovePoints = MaxMovePoints;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)moveDir * 1f);
    }
#endif
}
