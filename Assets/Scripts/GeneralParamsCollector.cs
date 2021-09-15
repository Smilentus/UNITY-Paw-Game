using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GeneralParamsCollector
{
    // А вот это уже класс для ОБЩИХ переменных, по типу медалей, энергии и т.п.
    // Хотя можно абсолютно всё спихивать, объект статичный, существует везде
    // Тут будем хранить переменные ...

    private static int player_MaxEnergy;
    private static int player_CurrentEnergy;

    private static int player_Medals;

    public static string getEnergyString { get { return player_CurrentEnergy.ToString() + "/" + player_MaxEnergy.ToString(); } }

    public static string getMedalsString { get { return player_Medals.ToString(); } }

    public static void AddMedals(int value)
    {
        player_Medals += value;
    }

    public static void ExtractMedals(int value)
    {
        player_Medals -= value;
    }

    public static void AddEnergy(int value)
    {
        player_CurrentEnergy += value;

        if (player_CurrentEnergy > player_MaxEnergy)
            player_CurrentEnergy = player_MaxEnergy;
    }

    public static void ExtractEnergy(int value)
    {
        player_CurrentEnergy -= value;

        if (player_CurrentEnergy < 0)
            player_CurrentEnergy = 0;
    }
}
