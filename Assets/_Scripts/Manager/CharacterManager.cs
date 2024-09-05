using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterManager : MonoBehaviour, ITurn
{
    public const int MAX_CHARS = 2;

    public GridSystem gridSystem;

    [SerializeField] private GameObject characterObject;
    [SerializeField] private CardManager cardManager;

    [SerializeField] private Sprite knightSprite;
    [SerializeField] private Sprite mageSprite;

    [SerializeField] private List<PlayableCharacters> playableCharacters = new List<PlayableCharacters>();
    public List<Character> ActiveCharacters = new List<Character>();

    public Character GetCharacterByType(CharacterType _type) => ActiveCharacters.First((chara) => chara.type == _type);

    public static CharacterManager instance { get; private set; }

    public event Action OnCharacterInitialize;
    public event Action OnTurn;

    //Singleton
    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        } else
        {
            instance = this;
        }
    }

    private void Start()
    {
        InitializeCharacters();
    }

    public void StartTurn()
    {
        OnTurn?.Invoke();
        Debug.Log("Player Start");
    }

    public void EndTurn()
    {
        Debug.Log("Player End");
    }

    public void InitializeCharacters()
    {
        for(int i = 0; i < playableCharacters.Count; i++)
        {
            var chara = NewCharacter(playableCharacters[i], gridSystem.characterSpawnPositions[i]);
            ActiveCharacters.Add(chara);
        }

        OnCharacterInitialize?.Invoke();
    }

    public Character NewCharacter(PlayableCharacters characterDetails, Vector2Int cellPos)
    {
        var cell = gridSystem.GetCell(cellPos);

        if (!cell)
        {
            Debug.Log("Cell is NOT found");
            return null;
        }

        var charObj = Instantiate(characterObject);
        var chara = charObj.GetComponent<Character>();

        chara.stats = characterDetails.stats;
        chara.entityName = characterDetails.name;
        chara.GetComponent<SpriteRenderer>().sprite = characterDetails.sprite;
        chara.type = characterDetails.characterType;

        cell.SetEntity(chara);

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

    private void OnDestroy()
    {
        ActiveCharacters.Clear();
    }
}

[Serializable]
public class PlayableCharacters {
    public string name;
    public Sprite sprite;
    public StatsScriptable stats;
    public CharacterType characterType;
}
