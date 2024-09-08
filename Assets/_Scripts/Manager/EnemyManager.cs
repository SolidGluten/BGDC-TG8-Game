using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemyManager : MonoBehaviour, ITurn
{
    public static EnemyManager Instance;

    [SerializeField] private GameObject enemyObject;

    public bool showAttackRange = true;
    //public bool showDetectionRange = true;

    public bool spawnRandom = true;

    [SerializeField] private List<SpawnableEnemy> enemySpawnList = new List<SpawnableEnemy>();
    public List<Enemy> ActiveEnemies = new List<Enemy>();

    public event Action OnEnemyInitialize;

    public int maxEnemyCost = 5 + GameManager.currentRound * 3;
    public int currEnemyCost = 0;

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
        maxEnemyCost = 5 + GameManager.currentRound * 3;
        currEnemyCost = maxEnemyCost;

        InitializeEnemies();

        foreach (Enemy enemy in ActiveEnemies)
        {
            //if (showDetectionRange) enemy.HighlightDetectionArea();
            if (showAttackRange) enemy.HighlightAttackRange();
        }
    }

    public void StartTurn()
    {
        ActiveEnemies.RemoveAll(x => x == null);
        CellsHighlighter.ResetLayerType();

        foreach(Enemy enemy in ActiveEnemies)
        {
            enemy.GetComponent<EnemyMovement>().Move();
            enemy.PrepareAttack();

            //if (showDetectionRange) enemy.HighlightDetectionArea();
            if (showAttackRange) enemy.HighlightAttackRange();
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

        if (spawnRandom)
        {
            var limiter = 0;
            do
            {
                var randIdx = UnityEngine.Random.Range(0, enemySpawnList.Count);
                var enemy = enemySpawnList[randIdx];

                if (enemy.isSpawnable && currEnemyCost >= enemy.enemyScriptable.cost)
                {
                    var randPosIdx = UnityEngine.Random.Range(0, GridSystem.Instance.enemySpawnPositions.Length);
                    var randPos = GridSystem.Instance.enemySpawnPositions[randIdx];
                    var cell = GridSystem.Instance.GetCell(randPos);

                    if (cell && !cell.isOccupied)
                    {
                        AddEnemy(enemy.stats, enemy.enemyScriptable, randPos);
                    }

                    currEnemyCost -= enemy.enemyScriptable.cost;
                }

                limiter++;
                if (limiter > 20) break;

            } while (currEnemyCost > 0); 
        }
        else
        {
            for(int i = 0; i < enemySpawnList.Count; i++)
            {
                var spawnPos = GridSystem.Instance.enemySpawnPositions[i];
                var cell = GridSystem.Instance.GetCell(spawnPos);

                if (cell && !cell.isOccupied)
                {
                    var enemy = enemySpawnList[i];
                    if (!enemy.isSpawnable) continue;
                    if(enemy != null) AddEnemy(enemy.stats, enemy.enemyScriptable, spawnPos);
                }
            }
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

    private void OnDestroy()
    {
        ActiveEnemies?.Clear();
    }
}

[Serializable]
public class SpawnableEnemy {
    public bool isSpawnable;
    public StatsScriptable stats;
    public EnemyScriptable enemyScriptable;
}