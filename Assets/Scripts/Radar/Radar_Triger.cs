using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar_Triger : MonoBehaviour
{
    Joystic_touch joystic_Touch;
    private GameObject data;
    public GameObject radar;//полоска радара на которую реагирует скрипт
    TreasureController treasureController;
    Controller Controller;
    CatAI  CatAi;
    private void Start()
    {
        joystic_Touch = GameObject.Find("Dog").GetComponent<Joystic_touch>();
        data = GameObject.Find("Data");
        treasureController = FindObjectOfType<TreasureController>();
        Controller = GameObject.Find("Data").GetComponent<Controller>();
        CatAi = GameObject.Find("Cat").GetComponent<CatAI>();
    }
    private void OnTriggerStay(Collider other)//если клад входит в зону действия радара
    {
        if (other.gameObject.tag == "Cat")
        {
            CatAi.ScareCat();
        }
        if (treasureController.isCarryingPurse == false && treasureController.isCarryingAmulet == false)//если не подобран кошель
        {
            if (other.gameObject.tag == "Klad")//проверяем что это клад по тегу
            {
                if(!Controller.SpeedChanged){
                joystic_Touch.speedMove -=  Controller.SpeedReduce;
                Controller.SpeedChanged = true;
                }
                if (!radar.activeSelf)
                {
                    radar.SetActive(true);//активируем полоску радара
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)//если клад выходит из зоны действия радара
    {
        if (other.gameObject.tag == "Klad")//проверяем что это клад по тегу
        {
            if(Controller.SpeedChanged){
                joystic_Touch.speedMove += Controller.SpeedReduce;
                Controller.SpeedChanged = false;
                }
            if (radar.activeSelf)
            {
                radar.SetActive(false);//отключаем полоску радара
            }
        }
    }
}
