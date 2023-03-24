using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : Singleton<InGameManager>
{
    public Keyboard keyboard;
    public BoardManager BoardManager;
    private void Start()
    {
        keyboard.gameObject.SetActive(false);
        BoardManager.Init();
    }
}
