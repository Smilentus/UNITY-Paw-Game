using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelInfo", menuName = "WorldMapLevel/CreateNewLevel")]
public class LvlInfo : ScriptableObject
{
    // === Переменные уровня ===
    [Header("Идентификатор уровня (ОБЯЗАТЕЛЬНО)")]
    public string Lvl_ID;  // Если не заполнять - игра не пустит на уровень
    [Header("Наименование сцены уровня")]
    public string Lvl_SceneName;
    [Header("Наименование уровня (для игрока)")]
    public string Lvl_Name;
    [Header("Задания на уровень")]
    public LvlTask[] Lvl_Tasks;

    public string GetLvlDescr()
    {
        return Lvl_Name.ToString() + "\n" + GetLvlTasks();
    }

    public string GetLvlTasks()
    {
        string output = "";
        foreach (var task in Lvl_Tasks)
            output += "\n" + task.GetTaskName();
        return output;
    }
}
