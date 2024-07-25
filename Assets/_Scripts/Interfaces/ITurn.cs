using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITurn
{
    public IEnumerator Turn();
    public void StartTurn();
    public void EndTurn();
}
