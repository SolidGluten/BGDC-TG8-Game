using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            name_text.text = entity.entityName.ToString();
            health_text.text = entity.currHealth + "/" + entity.stats.HP;
            shield_text.text = entity.currShield.ToString();
            stamina_text.text = entity.currMovePoints + "/" + entity.stats.MOV;

            //buffs
            var buffs_array = entity.GetBuffs().Select(x => x.effect.type + " " + x.stacks + "X");
            var buffs_temp_text = string.Join("\n", buffs_array);
            
            buffs_text.text = buffs_temp_text;

            //debuffs
            var debuffs_array = entity.GetDebuffs().Select(x => x.effect.type + " " + x.stacks + "X");
            var debuffs_temp_text = string.Join("\n", debuffs_array);

            debuffs_text.text = debuffs_temp_text;
        }
        else
        {
            name_text.text = "";
            health_text.text = "";
            shield_text.text = "";
            stamina_text.text = "";
            buffs_text.text = "";
            debuffs_text.text = "";
        }
    }

}
