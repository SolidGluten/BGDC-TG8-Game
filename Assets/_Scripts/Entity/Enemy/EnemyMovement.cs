using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class EnemyMovement : MonoBehaviour
{
    private Enemy enemy;

    public List<Cell> pathToChara = new List<Cell>();

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    public Character FindNearestCharacter() {
        if (!CharacterManager.Instance.ActiveCharacters.Any()) return null;

        var closestChara = CharacterManager.Instance.ActiveCharacters[0];
        foreach(Character chara in CharacterManager.Instance.ActiveCharacters)
        {
            if (Vector2.Distance(transform.position, chara.transform.position) < Vector2.Distance(transform.position, closestChara.transform.position))
            {
                closestChara = chara;
            }
        }
        return closestChara;
    }


    public void Move()
    {
        Character charaToMove = FindNearestCharacter();
        if (!charaToMove) {
            Debug.LogWarning("No character found to move to!");
            return;
        }

        if (pathToChara.Any()) foreach (Cell cell in pathToChara) EnumFlags.SetFlag(cell.Types, CellType.Path, true);
        pathToChara = Pathfind.FindPath(enemy.occupiedCell.index, charaToMove.occupiedCell.index);

        if (pathToChara.Any())
        {
            foreach (Cell cell in pathToChara) EnumFlags.SetFlag(cell.Types, CellType.Range, true);
            int moveCount = Mathf.Clamp(pathToChara.Count, 1, enemy.stats.MOV);
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
