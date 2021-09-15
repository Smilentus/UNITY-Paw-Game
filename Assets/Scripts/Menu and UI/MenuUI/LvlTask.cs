using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LvlTask
{
    [Header("Наименование задачи")]
    public string Task_Name;
    [Header("Полное описание задачи")]
    public string Task_Descr;
    [Header("Текущий прогресс задачи (не изменять)")]
    public int Task_Progress;
    [Header("Необходимый прогресс задачи")]
    public int Task_FullProgress;
    [Header("Задача выполнена?")]
    public bool isDone;

    public string GetTaskName()
    {
        return Task_Name.Replace("{value}", Task_FullProgress.ToString());
    }
}
