using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Entity entity;

    public Slider slider;
    public TextMeshProUGUI textMesh;

    [SerializeField] private int currHealthValue;
    [SerializeField] private int maxHealthValue;

    public float Y_OffsetMultiplier = 5f;

    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        this.name = entity.name + "-HealthBar";
        entity.OnDeath += DestroySelf;

        maxHealthValue = entity.stats.HP;
        slider.maxValue = maxHealthValue;
    }

    private void Update()
    {
        var halfHeight = entity.GetComponent<Collider2D>().bounds.extents.y / 2;
        var newPosition = RectTransformUtility.WorldToScreenPoint(GameManager.mainCam, entity.transform.position + Vector3.up * halfHeight * Y_OffsetMultiplier);

        transform.position = newPosition;

        currHealthValue = entity.currHealth;

        slider.value = currHealthValue;
        textMesh.text = currHealthValue.ToString();
    }

    private void DestroySelf() => Destroy(this.gameObject);

    private void OnDisable()
    {
        entity.OnDeath -= DestroySelf;
    }
}
