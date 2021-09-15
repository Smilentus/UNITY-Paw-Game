using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YamkaDead : MonoBehaviour
{
    GameObject dog;
    private void Start()
    {
        dog = GameObject.Find("Dog");
        gameObject.transform.parent = dog.transform;
        gameObject.transform.position = new Vector3(dog.transform.position.x, dog.transform.position.y - 4.2f, dog.transform.position.z + 2.3f);
        gameObject.transform.Rotate(dog.transform.rotation.x, dog.transform.rotation.y, dog.transform.rotation.z);
        gameObject.transform.parent = null;
        Invoke("Smert", 10f);
    }
    public void Smert()
    {
        Destroy(gameObject);
    }
}
