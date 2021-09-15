using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureAllocator : MonoBehaviour
{
    [Header("Точки спавна на текущем уровне")]
    public TreasurePoint[] lvlTreasurePoints;
    
    private List<TreasurePoint> currentTreasurePoints;

    [Header("Рандомизировать хранилище?")]
    private bool randomlyGenerateStorage = false; // Задел на будущее

    private ScoreAnimation scoreAnimation;
    private TreasureController treasureController;

    public int storageCount
    {
        get { return currentTreasurePoints.Count;  }
    }

    public void Start()
    {
        currentTreasurePoints = new List<TreasurePoint>();
        foreach (var treasure in lvlTreasurePoints)
            currentTreasurePoints.Add(treasure);

        scoreAnimation = FindObjectOfType<ScoreAnimation>();
        treasureController = FindObjectOfType<TreasureController>();
        
        //for (int i = 0; i < 100; i++)
        //    TestRandomGeneration();
    }
    
    /// <summary>
    /// Триггер раскопанного предмета (именно сюда идёт скрипт после отработки столкновения)
    /// </summary>
    public void TriggerTreasure()
    {
        if (currentTreasurePoints.Count == 0)
        {
            Debug.Log("Не осталось спавнпоинтов с кладами");
            return;
        }

        // Получаем раскопанное сокровище с хранилища
        TreasurePoint exportItem = currentTreasurePoints[0];
        currentTreasurePoints.RemoveAt(0);

        // Получаем случайный предмет
        TreasurePoint.treasureType randomItem = GetRandomItem(exportItem);

        // Обрабатываем предмет
        SetTreasureEffect(randomItem);
    }

    /// <summary>
    /// Получаем случайный предмет из раскопанного сокровища
    /// </summary>
    public TreasurePoint.treasureType GetRandomItem(TreasurePoint tp)
    {
        float totalTreasuresLimit = 0;
        for (int i = 0; i < tp.TPparams.Length; i++)
            totalTreasuresLimit += tp.TPparams[i].itemChance;

        // Собсна шанс на предмет
        float random = Random.Range(0, totalTreasuresLimit);

        float curSum = 0;
        TreasurePoint.treasureType resultItem = TreasurePoint.treasureType.Null;

        // Умная математическая формула линейного рандома в которой проверяем какой предмет выпал
        for (int i = 0; i < tp.TPparams.Length; i++)
        {
            if (curSum <= random && random < curSum + tp.TPparams[i].itemChance)
            {
                resultItem = tp.TPparams[i].itemType;
                break;
            }
            curSum += tp.TPparams[i].itemChance;
        }

        return resultItem;
    }

    /// <summary>
    /// Триггерит событие, связанное с предметом
    /// </summary>
    public void SetTreasureEffect(TreasurePoint.treasureType itemType)
    {
        // Скриптуем рандомное событие
        int chanceToEvent = Random.Range(0, 100);
        
        // Делаем что-то в зависимости от выпавшей вещи
        switch (itemType)
        {
            case TreasurePoint.treasureType.Bone: // Кость
                treasureController.AddScore(25);
                scoreAnimation.BoneAnimation();
                if (chanceToEvent <= Random.Range(15, 21)) SpawnSeagull();
                break;
            case TreasurePoint.treasureType.GoldenBone: // Золотая кость
                treasureController.AddScore(60);
                scoreAnimation.GoldenBoneAnimation();
                if (chanceToEvent <= Random.Range(15, 21)) SpawnSeagull();
                break;
            case TreasurePoint.treasureType.Hat: // Шляпа
            case TreasurePoint.treasureType.Wallet: // Кошелёк
                treasureController.isCarryingPurse = true;
                GameObject.Find("eat").GetComponent<Klad_Up>().purse();
                break;
            case TreasurePoint.treasureType.Amulet: // Амулет
                treasureController.isCarryingAmulet = true;
                break;
            case TreasurePoint.treasureType.AncientSphere: // Древняя сфера 
                int bonus = Random.Range(-1, 8);
                treasureController.AddScore(10 * bonus);
                UFOEffect();
                break;
            case TreasurePoint.treasureType.Null:
            default:
                // Чтобы не крашилось в неудобные моменты
                Debug.Log("Забавно, это по факту ошибка О_о");
                break;
        }
    }   

    /// <summary>
    /// Триггер для спавна чайки (точка привязки для кода Влада)
    /// </summary>
    private void SpawnSeagull()
    {
        Debug.Log("Не повезло, якобы чайка заспавнилась и украла наши косточки :(");
    }

    /// <summary>
    /// Триггер для спавна НЛО, музыка, анимация и эффект.
    /// </summary>
    private void UFOEffect()
    {
        Debug.Log("Заспавнилось НЛО");

        // Играет музыка, анимации и т.п.
        // Замедляется собачка

        treasureController.isShowingThrough = true;
        ShowAllTreasures();
        Invoke("EndOfUFOEffect", 10f); // Через 10 секунд прекратится эффект НЛО
    }

    /// <summary>
    /// Окончание действия эффекта НЛО
    /// </summary>
    private void EndOfUFOEffect()
    {
        treasureController.isShowingThrough = false;
        HideAllTreasures();
    }

    /// <summary>
    /// Функция для отображения всех сокровищ на игровом поле
    /// </summary>
    public void ShowAllTreasures()
    {
        GameObject[] treasures = GameObject.FindGameObjectsWithTag("Klad");

        foreach(GameObject obj in treasures)
        {
            obj.GetComponent<MeshRenderer>().enabled = true;
        }
    }
    
    /// <summary>
    /// Функция для скрытия всех сокровищ на игровом поле
    /// </summary>
    public void HideAllTreasures()
    {
        GameObject[] treasures = GameObject.FindGameObjectsWithTag("Klad");

        foreach (GameObject obj in treasures)
        {
            obj.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    /// <summary>
    /// Только для тестирования и проверки шансов на выпадение, можно удалить на релизе
    /// </summary>
    public void TestRandomGeneration()
    {
        if (currentTreasurePoints.Count == 0)
        {
            Debug.Log("Не осталось спавнпоинтов с кладами");
            return;
        }

        // Получаем раскопанное сокровище с хранилища
        TreasurePoint exportItem = currentTreasurePoints[0];

        // Получаем случайный предмет
        TreasurePoint.treasureType randomItem = GetRandomItem(exportItem);

        Debug.Log("Выпал предмет: " + randomItem);
    }
}
