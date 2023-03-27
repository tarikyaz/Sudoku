using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Keyboard keyboard;
    public BoardManager BoardManager;
    internal LevelSetting currentLevelSetting;
    [SerializeField] Button hint_Button, exit_Button;

    internal void Init(LevelSetting levelSetting)
    {
        currentLevelSetting = levelSetting;
        keyboard.gameObject.SetActive(false);
        BoardManager.Init(this);
        exit_Button.onClick.AddListener(() => { GameManager.Instance.GoMainMenu(); });
        hint_Button.onClick.AddListener(() => {
            Action onFinisHint = null;
            onFinisHint += () => hint_Button.gameObject.SetActive(true);
            hint_Button.gameObject.SetActive(false);
            BoardManager.ShowHint(onFinisHint);
        });
    }
}
