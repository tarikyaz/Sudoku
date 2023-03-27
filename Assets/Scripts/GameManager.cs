using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] LevelManager levelPrefab;
    [SerializeField] GameObject mainMenu;
    internal LevelManager currrentLevel;
    public void GoLinkedinProfile()
    {
        Application.OpenURL("https://www.linkedin.com/in/trk90/");
    }
    private void Start()
    {
        Application.targetFrameRate = 60;
        GoMainMenu();
    }
    public void StartLevel(LevelSetting levelSetting)
    {
        mainMenu.gameObject.SetActive(false);
        currrentLevel.gameObject.SetActive(true);

        currrentLevel.Init(levelSetting);
    }
    public void GoMainMenu()
    {
        if (currrentLevel!=null)
        {
            Destroy(currrentLevel.gameObject);
        }
        currrentLevel = Instantiate(levelPrefab, null);
        currrentLevel.transform.localPosition = Vector3.zero;
        currrentLevel.transform.localScale = Vector3.one;
        currrentLevel.transform.localRotation = Quaternion.identity;
        mainMenu.gameObject.SetActive(true);
        currrentLevel.gameObject.SetActive(false);
    }
}
