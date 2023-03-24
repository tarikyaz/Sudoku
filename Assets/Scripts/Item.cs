using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] TMP_Text textArea;
    internal LevelData.ItemTypeEnum itemType = LevelData.ItemTypeEnum.None;
    public void SetType(LevelData.ItemTypeEnum type, bool startEnabled)
    {
        itemType = type;
        if (!startEnabled)
        {
            SetText("");
            return;
        }
        switch (type)
        {
            case LevelData.ItemTypeEnum.n2: SetText("2"); break;
            case LevelData.ItemTypeEnum.n1: SetText("1"); break;
            case LevelData.ItemTypeEnum.n3: SetText("3"); break;
            case LevelData.ItemTypeEnum.n4: SetText("4"); break;
            case LevelData.ItemTypeEnum.n5: SetText("5"); break;
            case LevelData.ItemTypeEnum.n6: SetText("6"); break;
            case LevelData.ItemTypeEnum.n7: SetText("7"); break;
            case LevelData.ItemTypeEnum.n8: SetText("8"); break;
            case LevelData.ItemTypeEnum.n9: SetText("9"); break;
            default: SetText("Not set"); break;

        }
    }
    public void SetText(string text)
    {
        textArea.text = text;
    }
}
