using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterManager : MonoBehaviour, ITurn
{
    public const int MAX_CHARS = 2;

    [SerializeField] private GameObject characterObject;
    [SerializeField] private CardManager cardManager;

    [SerializeField] private List<StatsScriptable> charStats = new List<StatsScriptable>();
    public List<Character> ActiveCharacters = new List<Character>();

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
    }

    private void Start()
    {
        InitializeCharacters();
    }

    public IEnumerator Turn()
    {
        foreach (var chara in ActiveCharacters)
        {
            chara.isTurn = true;
        }

        //wait until every character have finished their turn
        while(ActiveCharacters.Any((chara) => chara.isTurn))
        {
            yield return null;
        }
    }

    public void StartTurn()
    {
        Debug.Log("Player Start");
    }

    public void EndTurn()
    {
        Debug.Log("Player End");
    }

    //Spawn Characters
    public void InitializeCharacters()
    {
        if(charStats.Count == 0)
        {
            Debug.Log("No character stats are assigned");
            return;
        }

        for(int i = 0; i < MAX_CHARS; i++)
        {
            AddCharacter(charStats[i], new Vector2Int(0, i));
        }
    }

    public Character AddCharacter(StatsScriptable stats, Vector2Int cellPos)
    {
        var cell = GridSystem.Instance.GetCell(cellPos);
        if (!cell)
        {
            Debug.LogWarning("Cell is NOT found");
            return null;
        }

        var charObj = Instantiate(characterObject);
        var chara = charObj.GetComponent<Character>();
        chara.stats = stats;    

        cell.SetEntity(chara);
        ActiveCharacters.Add(chara);

        return chara;
    }
}
