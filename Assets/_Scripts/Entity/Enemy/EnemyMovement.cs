using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Enemy enemy;
    private List<Cell> pathToChara = new List<Cell>();
    //private List<Cell> detectionArea = new List<Cell>();

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    public Character FindNearestCharacter() {

        var characters = CharacterManager.instance.ActiveCharacters;
        if (!characters.Any()) return null;

        Character nearestChara = characters[0];
        var closestDist = Vector2.Distance(this.transform.position, nearestChara.transform.position);

        foreach (Character chara in characters)
        {
            if (!chara) continue;

            var dist = Vector2.Distance(this.transform.position, chara.transform.position);
            if (dist < closestDist)
            {
                nearestChara = chara;
                closestDist = dist;
            }
        }

        return nearestChara;
    }

    public void Move()
    {
        if (!enemy.canMove) return;

        Character charaToMove = FindNearestCharacter();

        if (!charaToMove) {
            Debug.LogWarning("No character found to move to!");
            return;
        }

        enemy.isTargetInRange = false;

        var cellToMove = charaToMove.occupiedCell;

        var flipDir = CellsHighlighter.GetDirection(Vector3.right * this.transform.position.x, Vector3.right * charaToMove.transform.position.x);
        if (flipDir == Direction.Left) enemy.Flip(false);
        if (flipDir == Direction.Right) enemy.Flip(true);

        var moveDir = CellsHighlighter.GetDirection(this.transform.position, charaToMove.transform.position);

        if (enemy.enemyScriptable.isPerpendicularToTarget)
        {
            var characterPerpendicularArea = CellsHighlighter.HighlightArea(cellToMove.index, enemy.enemyScriptable.maxRangeFromTarget, HighlightShape.Cross);

            //CellsHighlighter.RaiseLayerType(characterPerpendicularArea, CellType.Effect);

            var closestCell = characterPerpendicularArea[0];
            var closestDist = Vector2.Distance(this.transform.position, closestCell.transform.position);

            foreach(Cell cell in characterPerpendicularArea)
            {
                if (!cell) continue;
                var dist = Vector2.Distance(cell.transform.position, this.transform.position);
                if(dist < closestDist)
                {
                    closestCell = cell;
                    closestDist = dist;
                }
            }

            if (closestCell)  cellToMove = closestCell;

            //closestCell.RaiseType(CellType.Path);
        }

        //if (pathToChara.Any()) CellsHighlighter.SetTypes(pathToChara, CellType.Path, false);

        // Find closest path to character
        pathToChara = Pathfind.FindPath(enemy.occupiedCell.index, cellToMove.index);

        if (!pathToChara.Any())
        {
            enemy.isTargetInRange = true;
            return;
        }

        // Remove character cell from path
        pathToChara.Remove(charaToMove.occupiedCell);

        if (!enemy.enemyScriptable.isPerpendicularToTarget)
        {
            // Stop enemy from moving when in max range
            if (pathToChara.Count >= enemy.enemyScriptable.maxRangeFromTarget)
            {
                for (int i = 0; i < enemy.enemyScriptable.maxRangeFromTarget - 1; i++)
                    pathToChara.Remove(pathToChara.Last());

            }
        }

        int moveCount = Mathf.Min(pathToChara.Count, enemy.stats.MOV);
        if (moveCount > 0)
        {
            pathToChara[moveCount - 1]?.SetEntity(enemy);
            return;
        } 
    }
}
