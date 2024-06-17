using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public event Action OnTakeDamage;
    public void TakeDamage(int dmg);    
}
