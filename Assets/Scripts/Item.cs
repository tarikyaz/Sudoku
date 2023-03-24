using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item : MonoBehaviour , IPointerEnterHandler , IPointerExitHandler
{
    public static Action<Item> OnMouseEnter , OnMouseLeave;
    [SerializeField] TMP_Text textArea;
    [SerializeField] Image _image;
    internal LevelData.ItemTypeEnum itemType = LevelData.ItemTypeEnum.None;
    Color colorCache;
    internal int I, J;
    public void SetType(LevelData.ItemTypeEnum type, bool startEnabled, int i, int j)
    {
        colorCache = _image.color;
        itemType = type;
        I = i;
        J = j;
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


    public void OnPointerEnter(PointerEventData eventData)
    {
        OnMouseEnter?.Invoke(this);
    }

    public void SetSiblingSelectColor()
    {
        _image.color = Color.Lerp(colorCache, Color.black, .3f);
    }
    public void SetelectColor()
    {
        _image.color = Color.Lerp(colorCache, Color.black, .5f);
    }
    public void ResetColor()
    {
        _image.color = colorCache;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        OnMouseLeave?.Invoke(this);
    }
}
