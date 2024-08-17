using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour, ITurn
{
    public static EnemyManager Instance;

    [SerializeField] private GameObject enemyObject;

    [SerializeField] private List<SpawnableEnemy> enemySpawnList = new List<SpawnableEnemy>();
    //[SerializeField] private List<StatsScriptable> enemySpawnList = new List<StatsScriptable>();
    public List<Enemy> ActiveEnemies = new List<Enemy>();

    public event Action OnEnemyInitialize;

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
        if(enemySpawnList.Count == 0)
        {
            Debug.LogWarning("Enemy stats not assigned.");
            return;
        }

        for(int i = 0; i < enemySpawnList.Count; i++)
        {
            AddEnemy(enemySpawnList[i].stats, enemySpawnList[i].enemy, GridSystem.Instance.enemySpawnPositions[i]);
        }

        OnEnemyInitialize?.Invoke();
    }

    private Enemy AddEnemy(StatsScriptable stats, EnemyScriptable enemyScript, Vector2Int cellPos)
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
        enemy.enemyScriptable = enemyScript;

        cell.SetEntity(enemy);
        ActiveEnemies.Add(enemy);

        return enemy;
    }
}

[Serializable]
public class SpawnableEnemy {
    public int cost;
    public StatsScriptable stats;
    public EnemyScriptable enemy;
}