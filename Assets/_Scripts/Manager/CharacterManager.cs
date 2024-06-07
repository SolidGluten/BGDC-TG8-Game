using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private GameObject characterObject;
    [SerializeField] private Character activeCharacter;

    [SerializeField] private bool toggleSelect;
    [SerializeField] private CardManager cardManager;
    [SerializeField] private GridSystem grid;

    [SerializeField] private StatsScriptable firstCharStats;
    [SerializeField] private StatsScriptable secondCharStats;

    private void Start()
    {
        if (!grid) Debug.LogWarning("Grid is NOT assigned.");
        AddCharacter(firstCharStats, new Vector2Int(0, 0));
        AddCharacter(secondCharStats, new Vector2Int(0, 1));
    }

    public void SetActiveChar(Character chara)
    {
        activeCharacter = chara;
        activeCharacter.isActive = true;
        activeCharacter.OnMovedCell += ResetSelection;
    }

    private void Update()
    {
        if (!activeCharacter && toggleSelect && Input.GetMouseButtonDown(0))
            Select();
    }

    private void Select()
    {
        Character chara = null;
        RaycastHit2D[] hits = Physics2D.RaycastAll(GameManager.MousePos, Vector3.forward);
        foreach(var hit in hits)
            if(hit.collider.gameObject.TryGetComponent<Character>(out chara)) break;

        if (chara)
            SetActiveChar(chara);
    }

    private void AddCharacter(StatsScriptable stats, Vector2Int cellPos)
    {
        
        var charObj = Instantiate(characterObject);

        var cell = grid.GetCell(cellPos);
        if (!cell)
        {
            Debug.LogWarning("Cell is NOT found");
            return;
        }
        grid?.AddObj(charObj, cellPos);

        cell.Obj = charObj;
        charObj.GetComponent<Character>().currCell = cell; 
    }

    private void ResetSelection()
    {
        activeCharacter.isActive = false;
        activeCharacter.OnMovedCell -= ResetSelection;
        activeCharacter = null;
        Debug.Log("Reseted");
    }
}
