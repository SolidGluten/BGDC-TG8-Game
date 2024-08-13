using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    public GameObject healthBarObj;

    public List<HealthBar> healthBarList = new List<HealthBar>();

    private void Start()
    {
        CharacterManager.Instance.OnCharacterInitialize += InitCharacterHealth;
        EnemyManager.Instance.OnEnemyInitialize += InitEnemyHealth;
    }

    private void InitCharacterHealth()
    {
        foreach (var chara in CharacterManager.Instance.ActiveCharacters)
        {
            var obj = Instantiate(healthBarObj, this.transform);
            var healthBar = obj.GetComponent<HealthBar>();
            healthBar.entity = chara;

            healthBarList.Add(healthBar);
        }
    }

    private void InitEnemyHealth()
    {
        foreach (var enemy in EnemyManager.Instance.ActiveEnemies)
        {
            var obj = Instantiate(healthBarObj, this.transform);
            var healthBar = obj.GetComponent<HealthBar>();
            healthBar.entity = enemy;

            healthBarList.Add(healthBar);
        }
    }

    private void OnDisable()
    {
        CharacterManager.Instance.OnCharacterInitialize -= InitCharacterHealth;
        EnemyManager.Instance.OnEnemyInitialize -= InitEnemyHealth;
    }
}
