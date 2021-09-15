using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TreasurePointParams
{
    [Header("Выпадаемый предмет")]
    public TreasurePoint.treasureType itemType;
    [Header("Вероятность выпадения")]
    [Range(0, 100)]
    public float itemChance;
}
