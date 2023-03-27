using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Keyboard keyboard;
    public BoardManager BoardManager;
    internal LevelSetting currentLevelSetting;
    [SerializeField] Button hint_Button, exit_Button , nextLevel_Button;
    [SerializeField] GameObject LevelFinishedUI;
    private void OnEnable()
    {
        BaseEvents.OnLevelFinish += OnLevelFinishHandler;
    }
    private void OnDisable()
    {
        BaseEvents.OnLevelFinish -= OnLevelFinishHandler;

    }
    private void OnLevelFinishHandler(bool obj)
    {
        LevelFinishedUI.gameObject.SetActive(true);
    }
    internal void Init(LevelSetting levelSetting)
    {
        LevelFinishedUI.gameObject.SetActive(false);

        currentLevelSetting = levelSetting;
        keyboard.gameObject.SetActive(false);
        BoardManager.Init(this);
        nextLevel_Button.onClick.AddListener(() => {
            LevelFinishedUI.gameObject.SetActive(false);
            BaseEvents.CallLevelStart();
        });
        exit_Button.onClick.AddListener(() => { GameManager.Instance.GoMainMenu(); });
        hint_Button.onClick.AddListener(() => {
            Action onFinisHint = null;
            onFinisHint += () => hint_Button.gameObject.SetActive(true);
            hint_Button.gameObject.SetActive(false);
            BoardManager.ShowHint(onFinisHint);
        });
    }
}
