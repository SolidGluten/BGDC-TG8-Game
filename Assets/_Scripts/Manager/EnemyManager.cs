using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour, ITurn
{
    [SerializeField] private GameObject enemyObject;

    [SerializeField] private List<StatsScriptable> enemyStats = new List<StatsScriptable>();
    public List<Enemy> ActiveEnemies = new List<Enemy>();

    public static EnemyManager Instance;

    //Singleton
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        } else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        InitializeEnemies();
    }

    public void StartTurn()
    {
        Debug.Log("Enemy Start");
        foreach(Enemy enemy in ActiveEnemies)
        {
            enemy.GetComponent<EnemyMovement>()?.Move();
        }
    }

    public void EndTurn()
    {
        Debug.Log("Enemy End");
    }

    //Spawn Enemies
    private void InitializeEnemies()
    {
        if(enemyStats.Count == 0)
        {
            Debug.LogWarning("Enemy stats not assigned.");
            return;
        }

        int i = 0;
        foreach(StatsScriptable stats in enemyStats)
        {
            AddEnemy(stats, new Vector2Int(0, GridSystem.Instance.Height - i - 1));
            i++;
        }
    }

    private Enemy AddEnemy(StatsScriptable stats, Vector2Int cellPos)
    {
        var cell = GridSystem.Instance.GetCell(cellPos);
        if (!cell)
        {
            Debug.LogWarning("Cell is NOT found");
            return null;
        }

        var enemyObj = Instantiate(enemyObject);
        var enemy = enemyObj.GetComponent<Enemy>();
        enemy.stats = stats;

        cell.SetEntity(enemy);

        ActiveEnemies.Add(enemy);

        return enemy;
    }
}
