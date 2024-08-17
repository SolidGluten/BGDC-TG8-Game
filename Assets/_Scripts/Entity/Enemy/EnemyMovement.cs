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

        if (detectionArea.Any())
        {
            foreach (var _cell in detectionArea)
                _cell.Types = EnumFlags.LowerFlag(_cell.Types, CellType.Enemy_Detection);
        }

        detectionArea = CellsHighlighter.HighlightArea(enemy.occupiedCell.index, enemy.enemyScriptable.detectionRange, HighlightShape.Square);

        foreach (var _cell in detectionArea)
            _cell.Types = EnumFlags.RaiseFlag(_cell.Types, CellType.Enemy_Detection);

        var entities = detectionArea
            .Where((cell) => cell.isOccupied)
            .Select((cell) => cell.occupiedEntity);
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
            Cell cellDst = null;

            if (moveCount == 1) return;

            if (pathToChara[moveCount - 1].isOccupied && moveCount <= enemy.stats.MOV)
                cellDst = pathToChara[moveCount - 2];
            else
                cellDst = pathToChara[moveCount - 1];

            cellDst.SetEntity(enemy);
        }
    }
}
