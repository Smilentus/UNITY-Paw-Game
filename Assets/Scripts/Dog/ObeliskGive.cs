using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObeliskGive : MonoBehaviour
{
    private GameObject data;

    GameObject dog;

    public GameObject strelka;

    private TreasureController treasureController;

    void Start()
    {
        dog = GameObject.Find("Dog");
        data = GameObject.Find("Data");
        treasureController = FindObjectOfType<TreasureController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (treasureController.isCarryingAmulet == true)
        {
            if (other.gameObject.tag == "Obelisk")
            {
                dog.gameObject.GetComponent<Joystic_touch>().enabled = false;
                treasureController.isCarryingAmulet = false;

                treasureController.AddScore(40);

                strelka.SetActive(false);

                treasureController.treasureMultiplier = 1.5f;

                Invoke("TimeT", 0.5f);
            }
        }
    }
    void TimeT()
    {
        dog.gameObject.GetComponent<Joystic_touch>().enabled = true;
        Invoke("EndOfEffect", 10);
    }

    void EndOfEffect()
    {
        treasureController.treasureMultiplier = 1;
    }
}
