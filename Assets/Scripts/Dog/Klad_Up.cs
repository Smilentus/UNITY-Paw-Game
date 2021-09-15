using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Klad_Up : MonoBehaviour
{
    private GameObject data;
    GameObject dog;

    public static int podnatoKladov = 0;

    private TreasureController treasureController;

    public GameObject PurseOnTheDog;
    public GameObject HatOnTheDogLeather;
    public GameObject HatOnTheDogStraw;

    public static event ActionOnSpawnPoint OnSpawnPointFound;
    public delegate void ActionOnSpawnPoint(GameObject spawnPoint);

    #region GameplaySoundsEvents
    public static event DiggingSound Digging;
    public delegate IEnumerator DiggingSound();

    public static event HatFound ActivateHatSound;
    public delegate IEnumerator HatFound();

    public static event PurseFound ActivatePurseSound;
    public delegate IEnumerator PurseFound();
    #endregion

    Animator DogAnimator;
    public bool isCarringObject = false;

    private void Start()
    {
        DogAnimator = GameObject.Find("dog_model_step4_animation_static").GetComponent<Animator>();
        dog = GameObject.Find("Dog");
        data = GameObject.Find("Data");
        treasureController = FindObjectOfType<TreasureController>();
    }

    /// <summary>
    /// Функция подбора предмета.
    /// Расскапываем сокровище, триггерим дальнейшие действия
    /// </summary>
    /// <param name="other">Объект столкновения</param>
    private void OnTriggerEnter(Collider other) // зона на которой происходт потбор предмета
    {
        if (treasureController.isCarryingPurse == false && treasureController.isCarryingAmulet == false) // если не подобран кошель
        {
            if (other.gameObject.tag == "Klad") // проверяем этот ли клад по тегу
            {
                // Раскапываем рандомное сокровище (там же и эффекты с триггерами)
                FindObjectOfType<TreasureAllocator>().TriggerTreasure();

                if (OnSpawnPointFound != null)
                {
                    OnSpawnPointFound(other.gameObject);
                }

                // Уничтожаем объект столкновения
                Destroy(other.gameObject);

                // Отключаем управление собачкой и включаем анимации
                StartCoroutine(Digging());
                dog.gameObject.GetComponent<Joystic_touch>().enabled = false;
                DogAnimator.SetBool("Walking", false);
                DogAnimator.SetBool("Digging", true);

                // Триггерим конец анимации и освобождение игрока
                Invoke("Timef", 0.5f);
            }
        }
    }

    /// <summary>
    /// Освобождение игрока, возвращение доступа к джойстику и отключение анимации
    /// </summary>
    public void Timef()
    {
        dog.gameObject.GetComponent<Joystic_touch>().enabled = true;

        dog.GetComponent<RadarBonus>().NullProperties();

        if(treasureController.SpeedChanged)
        {
            dog.GetComponent<Joystic_touch>().speedMove += treasureController.SpeedReduce;
            treasureController.SpeedChanged = false;
        }
        DogAnimator.SetBool("Digging", false);
    }

    /// <summary>
    /// Фукнция подбора кошелька или шляпы
    /// Проверяем поднятый предмет и рандомно выбираем что это такое
    /// </summary>
    public void purse()
    {
        int dos = 0;
        dos = UnityEngine.Random.Range(0, 100);

        if (treasureController.isCarryingPurse == true && !isCarringObject && dos < 50)
        {
            StartCoroutine(ActivatePurseSound());
            isCarringObject = true;
            PurseOnTheDog.SetActive(true);
        }

        if (treasureController.isCarryingPurse == true && !isCarringObject && dos >= 50)
        {
            StartCoroutine(ActivateHatSound());
            isCarringObject = true;

            int dosc = 0;
            dosc = UnityEngine.Random.Range(0, 100);

            if (dosc >= 50)
            {
                HatOnTheDogStraw.SetActive(true);
            }
            else
            {
                HatOnTheDogLeather.SetActive(true);
            }
        }
    }
}
