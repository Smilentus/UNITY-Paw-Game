using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabScore : MonoBehaviour
{
    public GameObject cameras;
    // Start is called before the first frame update
    void Start()
    {
        cameras = GameObject.FindWithTag("KladGold");
    }

    // Update is called once per frame
    void Update()
    {
        if (ScoreAnimation.scoreGo == true)
        {
            this.transform.position += (cameras.transform.position - this.transform.position).normalized * 15 * Time.deltaTime;
            if (Vector3.Distance(cameras.transform.position, this.transform.position) < 2)
            {
                ScoreAnimation.scoreGo = false;
                Destroy(gameObject);
            }
        }
    }
}
