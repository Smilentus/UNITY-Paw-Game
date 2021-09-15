using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurseOnTheDog : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {

    }


    void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Hozain_2")){
            GameObject.Find("eat").GetComponent<PurseCarry>().isCarringObject = false;
            this.gameObject.SetActive(false);
        }
    }
}