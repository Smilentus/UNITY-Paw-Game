using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    Controller Controller;
    public GameObject Cam;
    public Text MenuText;

    // Start is called before the first frame update
    void Start()
    {
        Controller = Cam.GetComponent<Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBtnClick(){
        SceneManager.LoadScene("Level1");
    }
}
