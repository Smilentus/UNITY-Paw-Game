using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarTeniOff : MonoBehaviour
{
    public GameObject teni1;
    public GameObject teni2;
    public GameObject teni3;
    public GameObject teni4;

    private void OnTriggerExit(Collider other)
    {
        teni1.SetActive(false); teni2.SetActive(false); teni3.SetActive(false); teni4.SetActive(false);
    }
}
