using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item : MonoBehaviour , IPointerEnterHandler , IPointerExitHandler , IPointerClickHandler
{
    public static Action<Item> OnMouseEnter , OnMouseLeave , OnMouseClick;
    [SerializeField] TMP_Text play_Text, hint_Text;
    [SerializeField] Image _image, hint_Image;
    LevelData.ItemTypeEnum itemCurrectType = LevelData.ItemTypeEnum.None;
    LevelData.ItemTypeEnum itemType = LevelData.ItemTypeEnum.None;
    public bool ItsCurrect => itemType == itemCurrectType;
    Color colorCache;
    internal int I, J;
    bool isStartedEnabled = false;
    public void Init(LevelData.ItemTypeEnum type, bool startEnabled, int i, int j)
    {
        colorCache = _image.color;
        itemCurrectType = type;
        isStartedEnabled = startEnabled;
        I = i;
        J = j;
        hint_Image.gameObject.SetActive(isStartedEnabled);
        switch (type)
        {
            case LevelData.ItemTypeEnum.n1: SetText("1",true); break;
            case LevelData.ItemTypeEnum.n2: SetText("2",true); break;
            case LevelData.ItemTypeEnum.n3: SetText("3",true); break;
            case LevelData.ItemTypeEnum.n4: SetText("4",true); break;
            case LevelData.ItemTypeEnum.n5: SetText("5",true); break;
            case LevelData.ItemTypeEnum.n6: SetText("6",true); break;
            case LevelData.ItemTypeEnum.n7: SetText("7",true); break;
            case LevelData.ItemTypeEnum.n8: SetText("8",true); break;
            case LevelData.ItemTypeEnum.n9: SetText("9",true); break;
            default: SetText("Not set",true); break;
        }
    }
    public void SetType(LevelData.ItemTypeEnum type)
    {
        itemType = type;
        switch (type)
        {
            case LevelData.ItemTypeEnum.n1: SetText("1",false); break;
            case LevelData.ItemTypeEnum.n2: SetText("2",false); break;
            case LevelData.ItemTypeEnum.n3: SetText("3",false); break;
            case LevelData.ItemTypeEnum.n4: SetText("4",false); break;
            case LevelData.ItemTypeEnum.n5: SetText("5",false); break;
            case LevelData.ItemTypeEnum.n6: SetText("6",false); break;
            case LevelData.ItemTypeEnum.n7: SetText("7",false); break;
            case LevelData.ItemTypeEnum.n8: SetText("8",false); break;
            case LevelData.ItemTypeEnum.n9: SetText("9",false); break;
            default: SetText("",false); break;
        }
    }
    public void SetText(string text, bool isHint)
    {
        if (isHint)
        {
            hint_Text.text = text;
        }
        else
        {
            play_Text.text = text;
        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isStartedEnabled)
        {
            OnMouseEnter?.Invoke(this);
        }
    }

    public void SetSiblingSelectColor()
    {
        _image.color = Color.Lerp(colorCache, Color.black, .3f);
    }
    public void SetSlelectColor()
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

    public void OnPointerClick(PointerEventData eventData)
    {
        OnMouseClick?.Invoke(this);
    }

    internal void SetClickColor()
    {
        _image.color = Color.Lerp(colorCache, Color.white, .5f);

    }

    internal void TuggleHint(bool v)
    {
        hint_Image.gameObject.SetActive(v);
    }
}
