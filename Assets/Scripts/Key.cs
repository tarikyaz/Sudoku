using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
    public static Action<LevelData.ItemTypeEnum> OnKeyClicked;
    [SerializeField] LevelData.ItemTypeEnum type = LevelData.ItemTypeEnum.None;
    [SerializeField] Button btn;
    [SerializeField] TMP_Text text;
    private void Start()
    {
        btn.onClick.AddListener(() => {
            OnKeyClicked?.Invoke(type);
        });
        string testStr = "";
        switch (type)
        {
            case LevelData.ItemTypeEnum.None:
                testStr = "Clear";
                break;
            case LevelData.ItemTypeEnum.n1:
                testStr = "1";
                break;
            case LevelData.ItemTypeEnum.n2:
                testStr = "2";
                break;
            case LevelData.ItemTypeEnum.n3:
                testStr = "3";
                break;
            case LevelData.ItemTypeEnum.n4:
                testStr = "4";
                break;
            case LevelData.ItemTypeEnum.n5:
                testStr = "5";
                break;
            case LevelData.ItemTypeEnum.n6:
                testStr = "6";
                break;
            case LevelData.ItemTypeEnum.n7:
                testStr = "7";
                break;
            case LevelData.ItemTypeEnum.n8:
                testStr = "8";
                break;
            case LevelData.ItemTypeEnum.n9:
                testStr = "9";
                break;
            default:
                break;
        }
        text.text = testStr;
    }

}
