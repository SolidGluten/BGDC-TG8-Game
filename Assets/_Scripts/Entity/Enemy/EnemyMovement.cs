using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class EnemyMovement : MonoBehaviour
{
    private Enemy enemy;
    [SerializeField] private int moveRadius;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    public void Move(Vector2Int movePos)
    {
        var cell = GridSystem.Instance.GetCell(movePos);
        if (!cell)
        {
            Debug.LogWarning("No cell found to move to!");
            return;
        }

        cell.SetEntity(enemy);
    }
}
