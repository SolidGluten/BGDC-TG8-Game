using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GridSystem grid;
    [SerializeField] private GameObject enemyObject;

    [SerializeField] private List<StatsScriptable> enemyStats = new List<StatsScriptable>();
    public List<Enemy> ActiveEnemies = new List<Enemy>();

    public static EnemyManager Instance;

    public event Action OnStartTurn;
    public event Action OnEndTurn;

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

        if (!grid) Debug.LogWarning("Grid is NOT assigned.");
    }

    private void Start()
    {
        InitializeEnemies();
    }

    public IEnumerator InitiateTurn()
    {
        OnStartTurn?.Invoke();
        
        //Insert enemy manager logic here

        OnEndTurn?.Invoke();
        yield break;
    }

    private void InitializeEnemies()
    {
        if(enemyStats.Count == 0)
        {
            Debug.LogWarning("Enemy stats not assigned.");
            return;
        }

        for(int i = 0; i < enemyStats.Count; i++)
        {
            ActiveEnemies.Add(AddEnemy(enemyStats[i], new Vector2Int(0, GridSystem.Instance.Height - i - 1)));
        }
    }

    private Enemy AddEnemy(StatsScriptable stats, Vector2Int cellPos)
    {
        var cell = grid.GetCell(cellPos);
        if (!cell)
        {
            Debug.LogWarning("Cell is NOT found");
            return null;
        }

        var enemyObj = Instantiate(enemyObject);
        var enemy = enemyObj.GetComponent<Enemy>();
        enemy.Stats = stats;

        cell.SetObject(enemyObj);

        ActiveEnemies.Add(enemy);

        return enemy;
    }
}
