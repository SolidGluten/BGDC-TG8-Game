using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using System;

public class CharacterMovement : MonoBehaviour
{
    private Character character;

    private void Awake()
    {
        character = GetComponent<Character>();
        TurnController.instance.OnEndTurn += ResetMovePoints;
    }

    private void Start()
    {
        character.Flip(true);        
    }

    private void Update()
    {
        if (!character.isSelected) return;

        // Track move points and input
        if (character.currMovePoints > 0)
        {
            if (Input.GetButtonDown("Vertical"))
            {
                var y_axis = Input.GetAxisRaw("Vertical");
                var moveDir = new Vector2Int(0, (int)y_axis);

                var cellIdx = character.occupiedCell.index;
                Move(cellIdx + moveDir);
            }
            else if (Input.GetButtonDown("Horizontal"))
            {
                var x_axis = Input.GetAxisRaw("Horizontal");
                var moveDir = new Vector2Int((int)x_axis, 0);

                if(x_axis > 0)
                    character.Flip(true);
                if (x_axis < 0)
                    character.Flip(false);

                var cellIdx = character.occupiedCell.index;
                Move(cellIdx + moveDir);
            }
        }
    }

    public void Move(Vector2Int movePos)
    {
        if (character.currMovePoints <= 0) return;

        var cell = GridSystem.Instance.GetCell(movePos);
        if (!cell) {
            Debug.LogWarning("No cell found to move to!");
            return;
        }

        if (cell.isOccupied)
        {
            Debug.LogWarning("Cell is already occupied!");
            return;
        }

        character.currMovePoints--;
        cell.SetEntity(character);
    }

    private void ResetMovePoints()
    {
        if(character.currMovePoints < character.stats.MOV)
        {

        }
        character.currMovePoints = character.stats.MOV;
    }

    private void OnDisable()
    {
        TurnController.instance.OnEndTurn -= ResetMovePoints;
    }
}
