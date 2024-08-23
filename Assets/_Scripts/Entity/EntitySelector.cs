using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySelector : MonoBehaviour
{
    public static EntitySelector instance;

    public Entity selectedEntity;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        } else
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Select();
    }

    private void Select()
    {
        Entity entity = null;
        RaycastHit2D[] hits = Physics2D.RaycastAll(GameManager.MousePos, Vector3.forward);
        foreach (var hit in hits)
        {
            if (hit.collider.gameObject.TryGetComponent<Entity>(out entity)) break;
        }

        if (selectedEntity) Unselect();

        if (entity)
        {
            selectedEntity = entity;
            var chara = selectedEntity.GetComponent<Character>();
            if (chara) chara.isSelected = true;
        }
    }

    private void Unselect()
    {
        var chara = selectedEntity.GetComponent<Character>();
        if (chara) chara.isSelected = false;
        selectedEntity = null;
    }
}
