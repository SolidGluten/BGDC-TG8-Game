using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    public static CharacterSelector instance;
    public Character SelectedCharacter;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        } else {
            instance = this;
        }
    }

    private void Update() 
    {
        if (Input.GetMouseButtonDown(0))
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

        if (SelectedCharacter) Unselect();

        if (chara) 
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
