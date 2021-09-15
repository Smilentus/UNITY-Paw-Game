using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnLevelUI : MonoBehaviour
{
    public static bool Restart = false;
    public GameObject PausePnl;
    public GameObject OnLevelUIPnl;
    public static int initialSpawnPointsAmmountOnTheField;
    public static event ActionOnRestart OnRestartButtonClick;
    public delegate void ActionOnRestart();
    public GameObject DogPref;
    public GameObject Dog;

    private void Start()
    {
        Dog = GameObject.Find("Dog");
        initialSpawnPointsAmmountOnTheField = AutoAllocator.currentPointsAmmountOnTheField;
        Debug.Log($"{initialSpawnPointsAmmountOnTheField} - thats initial ammount of spawn points");
    }

    public void OnPauseBtnClick()
    {
        PausePnl.SetActive(true);
        OnLevelUIPnl.SetActive(false);
    }

    public void OnContinueBtnClick()
    {
        OnLevelUIPnl.SetActive(true);
        PausePnl.SetActive(false);
    }

    public void OnMainMenuBtnClick() => SceneManager.LoadScene("Menu");

    public void OnRestartBtnClick()
    {
        foreach (var SpawnPoint in GameObject.FindGameObjectsWithTag("Klad")) Destroy(SpawnPoint);

        OnRestartButtonClick();

        OnLevelUIPnl.SetActive(true);
        PausePnl.SetActive(false);
    }
}
