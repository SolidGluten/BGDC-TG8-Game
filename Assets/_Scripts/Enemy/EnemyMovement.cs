using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Enemy enemy;
    private Cell currCell;
    [SerializeField] private int moveRadius;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        moveRadius = enemy.Stats.MOV;
    }

    [ContextMenu("Move Towards Nearest Character")]
    private void MoveTowardsCharacter()
    {
        var chara = GetNearestCharacter();
        if (chara == null) return;

        var dir = (chara.transform.position - transform.position).normalized;
        var hit = Physics2D.RaycastAll(transform.position, dir, moveRadius, 1 << 6);

        if (hit == null) return;
        var furthestCell = hit.Last().collider.GetComponent<Cell>();

        Move(furthestCell);
    }

    private Character GetNearestCharacter()
    {
        var charaList = CharacterManager.Instance?.ActiveCharacters;
        if(charaList == null || !charaList.Any())
        {
            Debug.LogWarning("No characters found.");
            return null;
        }

        Debug.Log(charaList);
        Character nearestChara = charaList.First();
        float distance = Vector2.Distance(transform.position, nearestChara.transform.position);

        foreach (var chara in charaList)
        {
            var temp_dist = Vector2.Distance(transform.position, chara.transform.position);
            if (temp_dist < distance) { 
                nearestChara = chara; 
            }
        }

        return nearestChara;
    }

    private void Move(Cell cell)
    {
        if (cell.isOccupied) return;
        if(currCell) currCell.SetObject(null); // Remove ref to this obj in the prevCell
        currCell = cell.SetObject(this.gameObject); // Move to position
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, moveRadius);
    }
#endif
}
