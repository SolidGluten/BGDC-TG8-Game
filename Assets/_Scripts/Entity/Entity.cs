using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Cell CurrCell;

    public void DestroySelf()
    {
        CurrCell.Obj = null;
        Destroy(gameObject);
    }
}
