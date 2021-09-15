using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatDisturb : MonoBehaviour
{
    public GameObject mark;
    CatAI CatAI;
    public bool catDisturb;

    void Start()
    {
        CatAI = GameObject.Find("Cat").GetComponent<CatAI>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Cat"))
        {
            ActivateMark();
            catDisturb = true;
            CatAI.ScareCat();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        DeactivateMark();
		catDisturb = false;
		CatAI.CalmCat();
	}

    void ActivateMark()
    {
        mark.SetActive(true);
    }

    void DeactivateMark()
    {
        mark.SetActive(false);
    }
}
