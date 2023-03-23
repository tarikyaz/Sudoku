using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] TMP_Text textArea;
    public void SetType(LevelSetting.ItemTypeEnum type)
    {
        switch (type)
        {
            case LevelSetting.ItemTypeEnum.n1: SetText("1"); break;
            case LevelSetting.ItemTypeEnum.n2: SetText("2"); break;
            case LevelSetting.ItemTypeEnum.n3: SetText("3"); break;
            case LevelSetting.ItemTypeEnum.n4: SetText("4"); break;
            case LevelSetting.ItemTypeEnum.n5: SetText("5"); break;
            case LevelSetting.ItemTypeEnum.n6: SetText("6"); break;
            case LevelSetting.ItemTypeEnum.n7: SetText("7"); break;
            case LevelSetting.ItemTypeEnum.n8: SetText("8"); break;
            case LevelSetting.ItemTypeEnum.n9: SetText("9"); break;
            case LevelSetting.ItemTypeEnum.Black: SetText("*"); break;
        }
    }
    public void SetText(string text)
    {
        textArea.text = text;

    }
}
