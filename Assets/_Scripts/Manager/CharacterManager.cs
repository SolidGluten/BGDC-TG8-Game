using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public const int MAX_CHARS = 2;

    [SerializeField] private GridSystem grid;
    [SerializeField] private GameObject characterObject;
    [SerializeField] private CardManager cardManager;

    [SerializeField] private List<StatsScriptable> charStats = new List<StatsScriptable>();
    public List<Character> ActiveCharacters = new List<Character>();

    public event Action OnStartTurn;
    public event Action OnEndTurn;

    public static CharacterManager Instance { get; private set; }

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
        InitializeCharacters();
    }

    private void InitiateTurn()
    {
        var coroutine = I_InitiateTurn();
        StartCoroutine(coroutine);
    }

    public IEnumerator I_InitiateTurn()
    {
        OnStartTurn?.Invoke();

        foreach (var chara in ActiveCharacters) chara.isActive = true;

        //Insert character manager logic here

        OnEndTurn?.Invoke();
        yield break;
    }

    [ContextMenu("Initialize ActiveCharacters")]
    public void InitializeCharacters()
    {
        if(charStats.Count == 0)
        {
            Debug.Log("No character stats are assigned");
            return;
        }

        for(int i = 0; i < MAX_CHARS; i++)
        {
            ActiveCharacters.Add(AddCharacter(charStats[i], new Vector2Int(0, i)));
        }
    }

    public Character AddCharacter(StatsScriptable stats, Vector2Int cellPos)
    {
        var cell = grid.GetCell(cellPos);
        if (!cell)
        {
            Debug.LogWarning("Cell is NOT found");
            return null;
        }

        var charObj = Instantiate(characterObject);
        var chara = charObj.GetComponent<Character>();
        chara.Stats = stats;    

        cell.SetObject(charObj);

        return chara;
    }
}
