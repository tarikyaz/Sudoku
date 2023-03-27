using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    string currentLevelIndexKeyStr => "CURRENTLEVELINDEX";
    int currentLevelIndex
    {
        get
        {
            int v = PlayerPrefs.GetInt(currentLevelIndexKeyStr, 0);
            if (v < 0 || v >= LevelsArray.Length)
            {
                v = 0;
            }
            currentLevelIndex = v;
            return v;
        }

        set => PlayerPrefs.SetInt(currentLevelIndexKeyStr, value);
    }
    [SerializeField]
    Level[] LevelsArray = new Level[0];
    [SerializeField] GameObject mainMenu;
    [SerializeField] Button start_Button;
    Level currentLevelPrefab => LevelsArray[currentLevelIndex];
    internal Level currentLevel;
    private void OnEnable()
    {
        BaseEvents.OnLevelStart += OnLevelStartHandller;
        BaseEvents.OnLevelFinish += OnLevelFinishHandller;
    }
    private void OnDisable()
    {
        BaseEvents.OnLevelStart -= OnLevelStartHandller;
        BaseEvents.OnLevelFinish -= OnLevelFinishHandller;
    }

    private void OnLevelFinishHandller(bool isWin)
    {
        if (isWin)
        {
            currentLevelIndex++;
        }

    }

    private void OnLevelStartHandller()
    {

        StartCoroutine(StartingLevel());
        IEnumerator StartingLevel()
        {
            mainMenu.gameObject.SetActive(false);
            if (currentLevel !=null)
            {
                Destroy(currentLevel.gameObject);
                yield return new WaitUntil(() => currentLevel == null);
            }
            currentLevel = Instantiate(currentLevelPrefab);
            currentLevel.transform.localPosition = Vector3.zero;
            currentLevel.transform.localRotation = Quaternion.identity;
            currentLevel.transform.localScale = Vector3.one;
            currentLevel.Init();
            yield return new WaitUntil(() => currentLevel != null);
            BaseEvents.CallOnLevelInit(currentLevel);

        }
    }

    public void GoLinkedinProfile()
    {
        Application.OpenURL("https://www.linkedin.com/in/trk90/");
    }
    private void Start()
    {
        start_Button.onClick.AddListener(() => {
            BaseEvents.CallLevelStart();
        });
        Application.targetFrameRate = 60;
        GoMainMenu();
    }
    public void GoMainMenu()
    {
        mainMenu.gameObject.SetActive(true);
        if (currentLevel != null)
        {
            currentLevel.gameObject.SetActive(false);
        }

    }
}
