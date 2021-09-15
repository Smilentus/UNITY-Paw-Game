using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LvlInfoManager : MonoBehaviour
{
    public GameObject lvlInfoPanel;
    public Text medalsText;
    public Text energyText;

    public Text lvlInfoTask;

    // Обычная иконка уровня
    public Sprite openedLvlIcon;
    // Иконка выполненного уровня
    public Sprite finishedLvlIcon;
    // Иконка недоступного уровня
    public Sprite closedLvlIcon;
    // Иконка уровня, пройденного на Хангеркор
    public Sprite hungerCoreFinishedLvlIcon;
    // Текущий выбранный уровень
    private LvlInfo choosedLvl;

    private void Start()
    {
        Application.targetFrameRate = 60;

        InitWorldMapObjects();
        DrawUIElements();
    }

    /// <summary>
    /// Отрисовка элементов пользовательского интерфейса (монеты и энергия)
    /// </summary>
    public void DrawUIElements()
    {
        medalsText.text = GeneralParamsCollector.getMedalsString;
        energyText.text = GeneralParamsCollector.getEnergyString;
    }

    // Также понадобится для реализации загрузки и сохранения
    // Формально это умная система для отображения иконок
    /// <summary>
    /// Инициализация и отрисовка кнопок выбора уровня на мировой карте
    /// </summary>
    private void InitWorldMapObjects()
    {
        Debug.Log("Инициализируем объекты на игровой карте ...");

        // Ищем все кнопки для старта уровней
        GameObject[] worldLevelObjects = GameObject.FindGameObjectsWithTag("WorldLevelObject");

        foreach (var obj in worldLevelObjects)
        {
            // Получаем информацию об уровне с кнопки
            LvlInfo info = obj.GetComponent<WorldMapLevelObject>().currentLvlInfo;

            // Если не смогли получить объект, значит что-то не так, не даём доступ к нему
            if (info is null || string.IsNullOrEmpty(info.Lvl_ID))
            {
                // Тогда рисуем иконку того, что уровень недоступен
                obj.GetComponent<SpriteRenderer>().sprite = closedLvlIcon;
                continue;
            }

            //// Проверяем выполнен уровень или нет
            //if (info.isLvlFinished)
            //{
            //    // Если выполнен в режиме ХангерКор, то своя иконка
            //    if (info.isHungerCore)
            //        obj.GetComponent<SpriteRenderer>().sprite = hungerCoreFinishedLvlIcon;
            //    else // Иначе обычная иконка завершения
            //        obj.GetComponent<SpriteRenderer>().sprite = finishedLvlIcon;
            //}
            //else
            //{
            //    // Уровень девственно не тронут, поэтому рисуем обычную иконку
                obj.GetComponent<SpriteRenderer>().sprite = openedLvlIcon;
            //}

            // WLO - World Level Object
            // Именуем каждый объект специальным тэгом для будущего удобства
            obj.name = "WLO_" + info.Lvl_ID;
        }
    }

    /// <summary>
    /// Функция отображения информации о выбранном уровне
    /// </summary>
    /// <param name="info"></param>
    public void ShowLvlInformation(LvlInfo info)
    {
        // Если уровень недоступен отображаем об этом или просто игнорим
        if (info is null || string.IsNullOrEmpty(info.Lvl_ID))
        {
            Debug.Log("Уровень в разработке! [ошибка, уровня нет]");
            return;
        }

        // Отображаем панель информации об уровне
        ShowInfoPanel();
        choosedLvl = info;
        lvlInfoTask.text = info.GetLvlDescr();

        Debug.Log("Выбран уровень: " + info.Lvl_ID);
    }

    // Открытие окна информации об уровне
    public void ShowInfoPanel()
    {
        lvlInfoPanel.SetActive(true);
    }

    // Закрытие окна информации об уровне
    public void HideInfoPanel()
    {
        lvlInfoPanel.SetActive(false);
        choosedLvl = null;
    }

    public void LoadLvlScene()
    {
        // Проверяем выбран ли уровень
        if (choosedLvl == null)
        {
            Debug.Log("Уровень не выбран!");
            return;
        }

        Debug.Log("Загрузка уровня " + choosedLvl.Lvl_SceneName + " ...");
        SceneManager.LoadScene(choosedLvl.Lvl_SceneName);
    }
}
