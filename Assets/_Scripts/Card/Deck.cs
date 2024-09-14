using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Deck", menuName = "ScriptableObjects/Deck")]
public class Deck : ScriptableObject
{
    public List<Card> cards;
}
