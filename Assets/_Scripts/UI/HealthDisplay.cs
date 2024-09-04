using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    public GameObject healthBarObj;
    public List<HealthBar> healthBarList = new List<HealthBar>();
    public EnemyManager enemyManager;
    public CharacterManager characterManager;

    private void OnEnable()
    {
        characterManager.OnCharacterInitialize += InitCharacterHealth;
        enemyManager.OnEnemyInitialize += InitEnemyHealth;
    }

    private void InitCharacterHealth()
    {
        foreach (var chara in characterManager.ActiveCharacters)
        {
            var obj = Instantiate(healthBarObj, this.transform);
            var healthBar = obj.GetComponent<HealthBar>();
            healthBar.SetEntity(chara);

            healthBarList.Add(healthBar);
        }
    }

    private void InitEnemyHealth()
    {
        foreach (var enemy in enemyManager.ActiveEnemies)
        {
            var obj = Instantiate(healthBarObj, this.transform);
            var healthBar = obj.GetComponent<HealthBar>();
            healthBar.SetEntity(enemy);

            healthBarList.Add(healthBar);
        }
    }

    private void OnDisable()
    {
        characterManager.OnCharacterInitialize -= InitCharacterHealth;
        enemyManager.OnEnemyInitialize -= InitEnemyHealth;
    }
}
