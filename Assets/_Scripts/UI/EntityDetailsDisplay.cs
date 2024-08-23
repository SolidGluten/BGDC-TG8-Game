using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EntityDetailsDisplay : MonoBehaviour
{
    public TextMeshProUGUI name_text;
    public TextMeshProUGUI health_text;
    public TextMeshProUGUI shield_text;
    public TextMeshProUGUI stamina_text;
    public TextMeshProUGUI buffs_text;
    public TextMeshProUGUI debuffs_text;

    private void Update()
    {
        var entity = EntitySelector.instance.selectedEntity;
        if (entity)
        {
            name_text.text = entity.name.ToString();
            health_text.text = entity.currHealth + "/" + entity.stats.HP;
            shield_text.text = entity.currShield.ToString();
            stamina_text.text = entity.currMovePoints + "/" + entity.stats.MOV;
            //buffs

            //debuffs
        }
        else
        {
            name_text.text = "";
            health_text.text = "";
            shield_text.text = "";
            stamina_text.text = "";
        }
    }

}
