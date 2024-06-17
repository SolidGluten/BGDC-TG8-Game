using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    public Character SelectedCharacter;

    private void Update() 
    {
        if (!SelectedCharacter && Input.GetMouseButtonDown(0))
            Select();
    }

    private void Select()
    {
        Character chara = null;
        RaycastHit2D[] hits = Physics2D.RaycastAll(GameManager.MousePos, Vector3.forward);
        foreach (var hit in hits)
        {
            if (hit.collider.gameObject.TryGetComponent<Character>(out chara)) break;
        }

        if (chara) 
        {
            SelectedCharacter = chara;
            SelectedCharacter.isActive = true;
            SelectedCharacter.OnTurnFinish += Unselect;
        }
    }

    private void Unselect()
    {
        SelectedCharacter.OnTurnFinish -= Unselect;
        SelectedCharacter.isActive = false;
        SelectedCharacter = null;
    }
}
