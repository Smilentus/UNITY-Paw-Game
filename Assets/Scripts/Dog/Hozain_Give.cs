using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hozain_Give : MonoBehaviour
{
    Joystic_touch joystic_Touch;

    private GameObject data;

    GameObject dog;

    public GameObject strelka1;
    public GameObject strelka2;
    public GameObject purseOnTheDog;
    public GameObject hatOnTheDog;
    public GameObject strawhatOnTheDog;

    private TreasureController treasureController;

    public static event SpeedBoostSound ActivateSpeedBoostSound;

    public delegate IEnumerator SpeedBoostSound();

    float canstSpeedDog;

    private void Start()
    {
        dog = GameObject.Find("Dog");
        data = GameObject.Find("Data");
        joystic_Touch = GameObject.Find("Dog").GetComponent<Joystic_touch>();
        canstSpeedDog = joystic_Touch.speedMove;
        treasureController = FindObjectOfType<TreasureController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (treasureController.isCarryingPurse == true)
        {
            if (other.gameObject.tag == "Hozain_1" || other.gameObject.tag == "Hozain_2")
            {
                treasureController.isCarryingPurse = false;
                dog.gameObject.GetComponent<Joystic_touch>().enabled = false;
                
                treasureController.AddScore(50);
                joystic_Touch.speedMove = joystic_Touch.speedMove + (canstSpeedDog / 2);

                StartCoroutine(ActivateSpeedBoostSound());
                Invoke("EndOfEffect", 8);

                if (purseOnTheDog.activeSelf)
                {
                    purseOnTheDog.SetActive(false);
                }
                if (hatOnTheDog.activeSelf)
                {
                    hatOnTheDog.SetActive(false);
                }
                if (strawhatOnTheDog.activeSelf)
                {
                    strawhatOnTheDog.SetActive(false);
                }

                GameObject.Find("eat").GetComponent<Klad_Up>().isCarringObject = false;

                strelka1.SetActive(false);
                strelka2.SetActive(false);

                Invoke("TimeT", 0.5f);
            }
        }
    }
    void TimeT()
    {
        dog.gameObject.GetComponent<Joystic_touch>().enabled = true;
    }

    void EndOfEffect()
    {
        joystic_Touch.speedMove = joystic_Touch.speedMove - (canstSpeedDog / 2);
    }
}
