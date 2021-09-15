using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strelka : MonoBehaviour
{
    private GameObject data;

    TreasureController treasureController;

    public GameObject strelka1;
    public GameObject strelka2;
    public GameObject strelka3;

    GameObject hozain1;
    GameObject hozain2;
    GameObject obelisk;
    Transform trHozain1;
    Transform trHozain2;
    Transform trObelisk;

    private void Start()
    {
        data = GameObject.Find("Data");
        treasureController = FindObjectOfType<TreasureController>();
    }
    void Update()
    {
        if (treasureController.isCarryingPurse == true)
        {
            hozain1 = GameObject.FindWithTag("Hozain_1");
            trHozain1 = hozain1.transform;
            strelka1.transform.LookAt(trHozain1);
            hozain2 = GameObject.FindWithTag("Hozain_2");
            trHozain2 = hozain2.transform;
            strelka2.transform.LookAt(trHozain2);
            strelka1.SetActive(true);
            strelka2.SetActive(true);
        }

        if (treasureController.isCarryingAmulet == true)
        {
            obelisk = GameObject.FindWithTag("Obelisk");
            trObelisk = obelisk.transform;
            strelka3.transform.LookAt(trObelisk);
            strelka3.SetActive(true);
        }
    }
}
