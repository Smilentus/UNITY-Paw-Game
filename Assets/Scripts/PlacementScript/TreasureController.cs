using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureController : MonoBehaviour
{
    public float treasureScore = 0;
    public float treasureMultiplier = 1;

    private GameObject scoreText;

    public bool SpeedChanged;
    public float SpeedReduce;

    // Несём шляпу или кошелёк?
    public bool isCarryingPurse;
    // Несём амулет?
    public bool isCarryingAmulet;
    // Работает ли эффект НЛО?
    public bool isShowingThrough;

    private void Start()
    {
        scoreText = GameObject.Find("Score");
    }

    /// <summary>
    /// Общая функция для добавления очков (позже будет привязана к статистике)
    /// </summary>
    /// <param name="scoreToAdd"></param>
    public void AddScore(int scoreToAdd)
    {
        treasureScore += scoreToAdd * treasureMultiplier;
        scoreText.GetComponent<Text>().text = treasureScore.ToString();
    }
}
