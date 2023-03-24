using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameManager : Singleton<InGameManager>
{
    public Keyboard keyboard;
    public BoardManager BoardManager;
    [SerializeField] Button hint_Button;

    private void Start()
    {
        keyboard.gameObject.SetActive(false);
        BoardManager.Init();
        hint_Button.onClick.AddListener(() => {
            Action onFinisHint =null;
            onFinisHint += () => hint_Button.gameObject.SetActive(true);
            hint_Button.gameObject.SetActive(false);
            BoardManager.ShowHint(onFinisHint);
        });
    }
}
