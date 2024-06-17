using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using System;

public class CharacterMovement : MonoBehaviour
{
    private Character character;
    private Cell currCell;
    private Vector2 moveDir = Vector2.zero;

    private void Awake()
    {
        character = GetComponent<Character>();
        character.OnTurnFinish += ResetMovePoints;
    }

    private void Update()
    {
        if (!character.isActive) return;

        // Track move points and input
        if (character.currMovePoints > 0)
        {
            if (Input.GetButtonDown("Vertical"))
            {
                var y_axis = Input.GetAxisRaw("Vertical");
                moveDir = new Vector2(0, y_axis);

                var cell = GetAdjacentCell(moveDir);
                if (cell) Move(cell);
            }
            else if (Input.GetButtonDown("Horizontal"))
            {
                var x_axis = Input.GetAxisRaw("Horizontal");
                moveDir = new Vector2(x_axis, 0);

                var cell = GetAdjacentCell(Vector2.right * x_axis);
                if (cell) Move(cell);
            }
        }
    }

    private Cell GetAdjacentCell(Vector2 dir)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, dir, Mathf.Infinity, 1 << 6);
        return hits.Length >= 2 ? hits[1].collider.gameObject.GetComponent<Cell>() : null;
    }

    public void Move(Cell cell)
    {
        if (cell.isOccupied) return;
        if (currCell != null) currCell.SetObject(null);// Remove ref to this obj in the prevCell
        currCell = cell.SetObject(this.gameObject); // Move to position
        character.currMovePoints--; // Reduce character move points
    }

    private void ResetMovePoints()
    {
        character.currMovePoints = character.Stats.MOV;
    }

    private void OnDisable()
    {
        character.OnTurnFinish -= ResetMovePoints;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)moveDir * 1f);
    }
#endif
}
