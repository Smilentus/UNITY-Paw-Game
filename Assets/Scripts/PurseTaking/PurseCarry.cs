using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurseCarry : MonoBehaviour
{
    public GameObject PurseOnTheDog;
    public GameObject HatOnTheDogLeather;
    public GameObject HatOnTheDogStraw;
    public bool isCarringObject = false;

    void Start() {

    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Purse") && !isCarringObject){
            isCarringObject = true;
            PurseOnTheDog.SetActive(true);
            Destroy(other.gameObject);
        }

        else if(other.gameObject.CompareTag("Hat") && !isCarringObject){
            isCarringObject = true;
            if(other.gameObject.name == "strawhat1") HatOnTheDogStraw.SetActive(true);
            else HatOnTheDogLeather.SetActive(true);
            Destroy(other.gameObject);
        }
    }
}
