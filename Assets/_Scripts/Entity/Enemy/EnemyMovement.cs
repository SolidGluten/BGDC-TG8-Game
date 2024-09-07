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

        var dir = CellsHighlighter.GetDirection(Vector3.right * this.transform.position.x, Vector3.right * charaToMove.transform.position.x);
        if (dir == Direction.Left) enemy.Flip(false);
        if (dir == Direction.Right) enemy.Flip(true);

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
