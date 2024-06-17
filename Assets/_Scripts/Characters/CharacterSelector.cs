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

        if (chara && chara.isActive)
        {
            SelectedCharacter = chara;
            SelectedCharacter.isSelected = true;
        }
    }

    private void Unselect()
    {
        SelectedCharacter.isSelected = false;
        SelectedCharacter = null;
    }
}
