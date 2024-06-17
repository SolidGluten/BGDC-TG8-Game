using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GridSystem grid;
    [SerializeField] private GameObject enemyObject;

    public List<Enemy> Enemies = new List<Enemy>();

    private List<StatsScriptable> statsScriptables = new List<StatsScriptable>();

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

    public IEnumerator InitiateTurn()
    {
        OnStartTurn?.Invoke();
        
        //Insert enemy manager logic here

        OnEndTurn?.Invoke();
        yield break;
    }

    private Enemy AddEnemy(StatsScriptable stats, Vector2Int cellPos)
    {
        var enemyObj = Instantiate(enemyObject);

        var cell = grid.GetCell(cellPos);
        if (!cell)
        {
            Debug.LogWarning("Cell is NOT found");
            return null;
        }
        grid.AddObj(enemyObj, cellPos);

        var enemy = enemyObj.GetComponent<Enemy>();
        enemy.CurrCell = cell;

        Enemies.Add(enemy);
        return enemy;
    }
}
