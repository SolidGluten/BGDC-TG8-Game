using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Cell occupiedCell;

    public void DestroySelf()
    {
        occupiedCell.occupiedEntity = null;
        Destroy(gameObject);
    }
}
