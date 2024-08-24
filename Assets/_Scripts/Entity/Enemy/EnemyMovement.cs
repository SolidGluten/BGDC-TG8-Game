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
    private List<Cell> detectionArea = new List<Cell>();

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    public Character FindNearestCharacter() {

        detectionArea = CellsHighlighter.HighlightArea(enemy.occupiedCell.index, enemy.enemyScriptable.detectionRange, HighlightShape.Circle);

        var characters = detectionArea
            .Where((cell) => cell.isOccupied)
            .Select((cell) => cell.occupiedEntity.GetComponent<Character>())
            .ToList();

        characters.RemoveAll(x => x == null);

        if (!characters.Any()) return null;
        else return characters.First();
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

        //if (pathToChara.Any()) CellsHighlighter.SetTypes(pathToChara, CellType.Path, false);
        pathToChara = Pathfind.FindPath(enemy.occupiedCell.index, charaToMove.occupiedCell.index);

        if (!pathToChara.Any()) return;
        //CellsHighlighter.SetTypes(pathToChara, CellType.Path, true);

        pathToChara.Remove(pathToChara.Last()); // Remove character cell from path

        if (pathToChara.Count >= enemy.enemyScriptable.maxRangeFromTarget)
        {
            for(int i = 0; i < enemy.enemyScriptable.maxRangeFromTarget - 1; i++)
                pathToChara.Remove(pathToChara.Last());

            int moveCount = Mathf.Min(pathToChara.Count, enemy.stats.MOV);
            pathToChara[moveCount - 1]?.SetEntity(enemy);
            return;
        }
            
        enemy.isTargetInRange = true;
    }
}
