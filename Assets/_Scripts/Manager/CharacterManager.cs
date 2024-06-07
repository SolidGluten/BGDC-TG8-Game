using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private GameObject characterObject;
    [SerializeField] private Character activeCharacter;

    [SerializeField] private bool toggleSelect;
    public CardManager CardManager;

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

    private void AddCharacter()
    {
    }

    private void ResetSelection()
    {
        activeCharacter.isActive = false;
        activeCharacter.OnMovedCell -= ResetSelection;
        activeCharacter = null;
        Debug.Log("Reseted");
    }
}
