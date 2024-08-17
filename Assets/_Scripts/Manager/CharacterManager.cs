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

    public Character GetCharacterByType(CharacterType _type) => ActiveCharacters.First((chara) => chara.type == _type);

    public static CharacterManager Instance { get; private set; }

    public event Action OnCharacterInitialize;

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

    public void StartTurn()
    {
        foreach (var chara in ActiveCharacters)
        {
            chara.isTurn = true;
        }
        Debug.Log("Player Start");
    }

    public void EndTurn()
    {
        foreach (var chara in ActiveCharacters)
        {
            chara.isTurn = false;
        }
        Debug.Log("Player End");
    }

    public void InitializeCharacters()
    {
        if(charStats.Count == 0)
        {
            Debug.Log("No character stats are assigned");
            return;
        }

        for(int i = 0; i < MAX_CHARS; i++)
        {
            AddCharacter(charStats[i], GridSystem.Instance.characterSpawnPositions[i]);
        }
        ActiveCharacters[0].type = CharacterType.Knight;
        ActiveCharacters[1].type = CharacterType.Mage;

        OnCharacterInitialize?.Invoke();
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

    public static Direction DirectionFromCharacter(Character chara)
    {
        Vector2 normDir = (GameManager.MousePos - (Vector2)chara.transform.position).normalized;
        int x = Math.Abs(normDir.x) > Math.Abs(normDir.y) ? (int)Math.Round(normDir.x) : 0;
        int y = Math.Abs(normDir.x) < Math.Abs(normDir.y) ? (int)Math.Round(normDir.y) : 0;
        Vector2Int dirVector = new Vector2Int(x, y);

        Direction dir = Direction.Right;

        if (dirVector == Vector2Int.left)
            dir = Direction.Left;
        else if (dirVector == Vector2Int.right)
            dir = Direction.Right;
        else if (dirVector == Vector2Int.up)
            dir = Direction.Up;
        else
            dir = Direction.Down;

        return dir;
    }
}
