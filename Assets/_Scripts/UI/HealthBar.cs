using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Entity entity;

    public Slider healthSlider;
    public Slider shieldSlider;
    public TextMeshProUGUI textMesh;

    [SerializeField] private int currHealthValue;
    [SerializeField] private int currShieldValue;
    [SerializeField] private int maxHealthValue;

    public float Y_OffsetMultiplier = 3f;

    private float halfHeight = 0;

    private void Awake()
    {
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (!entity) return;

        var newPosition = RectTransformUtility.WorldToScreenPoint(GameManager.mainCam, entity.transform.position + Vector3.up * (halfHeight + Y_OffsetMultiplier));

        transform.position = newPosition;

        currHealthValue = entity.currHealth;
        currShieldValue = entity.currShield;

        healthSlider.value = currHealthValue;
        shieldSlider.value = currShieldValue;

        textMesh.text = currHealthValue.ToString();
    }

    public void SetEntity(Entity _entity)
    {
        entity = _entity;

        this.name = entity.name + "-HealthBar";
        entity.OnDeath += DestroySelf;

        halfHeight = entity.GetComponent<SpriteRenderer>().bounds.size.y;

        maxHealthValue = entity.stats.HP;
        healthSlider.maxValue = maxHealthValue;
        shieldSlider.maxValue = maxHealthValue * 2;
    }

    private void DestroySelf() => Destroy(this.gameObject);

    private void OnDisable()
    {
        if (entity) entity.OnDeath -= DestroySelf;
    }
}
