using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterTrigerRadar : MonoBehaviour
{
    private GameObject data;

    Joystic_touch joystic_Touch;
    CatDisturb CatDisturb;
    RadarBonus RadarBonus;

    private TreasureController treasureController;

    void Start()
    {
        joystic_Touch = GameObject.Find("Dog").GetComponent<Joystic_touch>();
        data = GameObject.Find("Data");
        RadarBonus = GameObject.Find("Dog").GetComponent<RadarBonus>();
        CatDisturb = GameObject.Find("CatDisturbCol").GetComponent<CatDisturb>();
        treasureController = FindObjectOfType<TreasureController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (treasureController.isCarryingPurse == false && treasureController.isCarryingAmulet == false && CatDisturb.catDisturb == false) 
        {
            if (other.gameObject.tag == "Klad")
            {
                if (RadarBonus.IsFounded(other))
                {
                    if (!treasureController.SpeedChanged)
                    {
                        joystic_Touch.speedMove -= treasureController.SpeedReduce;
                        treasureController.SpeedChanged = true;
                    }

                    RadarBonus.ArrowsCounter++;
                    RadarBonus.SetRadarArrows();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.tag == "Klad")
        {
            if (RadarBonus.IsFounded(other))
            {
                if (treasureController.SpeedChanged)
                {
                    joystic_Touch.speedMove += treasureController.SpeedReduce;
                    treasureController.SpeedChanged = false;
                }

                RadarBonus.ArrowsCounter--;
                RadarBonus.SetRadarArrows();
            }
        }
    }
}