using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CUMera : MonoBehaviour
{
    public GameObject dog;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        dog = GameObject.Find("Dog");
        offset = transform.position - dog.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = dog.transform.position + offset;
    }
}
