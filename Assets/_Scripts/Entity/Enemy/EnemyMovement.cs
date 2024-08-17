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

        detectionArea = CellsHighlighter.HighlightArea(enemy.occupiedCell.index, enemy.enemyScriptable.detectionRange, HighlightShape.Square);

        var entities = detectionArea
            ?.Where((cell) => cell.isOccupied && cell != enemy.occupiedCell)
            ?.Select((cell) => cell.occupiedEntity);
        if (!entities.Any()) return null;

        var character = entities?.Select((entity) => entity.GetComponent<Character>())?.First();

        return character;
    }

    public void Move()
    {
        Character charaToMove = FindNearestCharacter();

        if (!charaToMove) {
            Debug.LogWarning("No character found to move to!");
            return;
        }

        //if (pathToChara.Any()) foreach (Cell cell in pathToChara) EnumFlags.SetFlag(cell.Types, CellType.Path, true);
        pathToChara = Pathfind.FindPath(enemy.occupiedCell.index, charaToMove.occupiedCell.index);

        if (pathToChara.Any())
        {
            //foreach (Cell cell in pathToChara) EnumFlags.SetFlag(cell.Types, CellType.Range, true);
            int moveCount = Mathf.Clamp(pathToChara.Count - enemy.enemyScriptable.maxRangeFromTarget, 1, enemy.stats.MOV);

            if (moveCount == 1) {
                enemy.isTargetInRange = true;
                return;
            } else
            {
                enemy.isTargetInRange = false;
            }

            while (pathToChara[moveCount].isOccupied && moveCount > 0) moveCount--;

            var cellDst = pathToChara[moveCount];
            cellDst.SetEntity(enemy);
        }
    }
}
