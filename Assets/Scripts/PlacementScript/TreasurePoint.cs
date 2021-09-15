using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TreasurePoint
{
    // Все выпадаемые вещи в игре
    /*
     * Если нужно добавить новый предмет, просто напиши сюда его идентификатор, остальное игра сама сделает
     */
    public enum treasureType { Null, Bone, GoldenBone, Hat, Wallet, Amulet, AncientSphere };

    [Header("Наименование точки (для удобства разрабов)")]
    public string TPName = "";
    [Header("Предметы и вероятности")]
    public TreasurePointParams[] TPparams;
}
