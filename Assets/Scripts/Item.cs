using DG.Tweening;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Item : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public static Action<Item> OnMouseEnter, OnMouseLeave, OnMouseClick;
    [SerializeField] TMP_Text play_Text, hint_Text;
    [SerializeField] Image _image, hint_Image;
    [SerializeField] CanvasGroup hintImage_CanvasGroup;
    [SerializeField] Outline outline;
    LevelData.ItemTypeEnum itemCurrectType = LevelData.ItemTypeEnum.None;
    LevelData.ItemTypeEnum ItemType = LevelData.ItemTypeEnum.None;
    internal LevelData.ItemTypeEnum VisualItemType => isStartedEnabled ? itemCurrectType : ItemType;

    public bool ItsCurrect => ItemType == itemCurrectType || isStartedEnabled;
    Color normalColorCache, hintColorCache;
    Color colorCache => isStartedEnabled ? hintColorCache : normalColorCache;
    internal int I, J;
    bool isStartedEnabled = false;
    Tween channgeImageColorTeen;
    Tween channgeHintImageColorTeen;
    Tween scalingTweem;
    internal bool IsConflict = false;
    Item[] relatives = new Item[0];
    public void Init(LevelData.ItemTypeEnum type, bool startEnabled, int i, int j)
    {
        IsConflict = false;
        normalColorCache = _image.color;
        hintColorCache = hint_Image.color;
        itemCurrectType = type;
        SetType(LevelData.ItemTypeEnum.None);
        isStartedEnabled = startEnabled;
        I = i;
        J = j;
        hint_Image.gameObject.SetActive(true);
        hintImage_CanvasGroup.alpha = isStartedEnabled ? 1 : 0;
        outline.enabled = false;
        transform.localScale = Vector3.one;

        switch (type)
        {
            case LevelData.ItemTypeEnum.n1: SetText("1", true); break;
            case LevelData.ItemTypeEnum.n2: SetText("2", true); break;
            case LevelData.ItemTypeEnum.n3: SetText("3", true); break;
            case LevelData.ItemTypeEnum.n4: SetText("4", true); break;
            case LevelData.ItemTypeEnum.n5: SetText("5", true); break;
            case LevelData.ItemTypeEnum.n6: SetText("6", true); break;
            case LevelData.ItemTypeEnum.n7: SetText("7", true); break;
            case LevelData.ItemTypeEnum.n8: SetText("8", true); break;
            case LevelData.ItemTypeEnum.n9: SetText("9", true); break;
            default: SetText("Not set", true); break;
        }
    }
    public void SetType(LevelData.ItemTypeEnum type)
    {
        ItemType = type;
        switch (type)
        {
            case LevelData.ItemTypeEnum.n1: SetText("1", false); break;
            case LevelData.ItemTypeEnum.n2: SetText("2", false); break;
            case LevelData.ItemTypeEnum.n3: SetText("3", false); break;
            case LevelData.ItemTypeEnum.n4: SetText("4", false); break;
            case LevelData.ItemTypeEnum.n5: SetText("5", false); break;
            case LevelData.ItemTypeEnum.n6: SetText("6", false); break;
            case LevelData.ItemTypeEnum.n7: SetText("7", false); break;
            case LevelData.ItemTypeEnum.n8: SetText("8", false); break;
            case LevelData.ItemTypeEnum.n9: SetText("9", false); break;
            default: SetText("", false); break;
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
        ChangeImageColor(Color.Lerp(colorCache, Color.black, .3f));
    }
    public void SetSlelectColor()
    {
        ChangeImageColor(Color.Lerp(colorCache, Color.black, .5f));
    }
    public void ResetColor()
    {
        outline.enabled = false;
        scalingTweem.Pause();
        scalingTweem.Kill();
        scalingTweem = transform.DOScale(1, .25f);
        ChangeImageColor(colorCache);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        OnMouseLeave?.Invoke(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isStartedEnabled)
        {
            OnMouseClick?.Invoke(this);
        }
        else
        {
            InGameManager.Instance.BoardManager.OnClickOutSide();
        }
    }

    internal void SetClickColor()
    {
        scalingTweem.Pause();
        scalingTweem.Kill();
        scalingTweem = transform.DOScale(.75f, .25f);
        outline.enabled = true;
        ChangeImageColor(Color.Lerp(colorCache, Color.white, .5f));
    }

    internal void TuggleHint(bool enable)
    {
        if (isStartedEnabled)
        {

        }
        else
        {
            channgeHintImageColorTeen.Pause();
            channgeHintImageColorTeen.Kill();
            channgeImageColorTeen.Pause();
            channgeImageColorTeen.Kill();
            Image image = hint_Image;
            image.color = enable ? normalColorCache : colorCache;
            channgeHintImageColorTeen = hintImage_CanvasGroup.DOFade(enable ? 1 : 0, .5f);
        }
    }
    void ChangeImageColor(Color newColor)
    {
        if (IsConflict)
        {
            return;
        }
        channgeImageColorTeen.Pause();
        channgeImageColorTeen.Kill();
        Image image = isStartedEnabled ? hint_Image : _image;
        channgeImageColorTeen = image.DOColor(newColor, .25f);
    }
    public Item[] GetRelavieItems()
    {
        if (relatives.Length > 0)
        {
            return relatives;
        }
        List<Item> newItems = new List<Item>();
        float boardSize = InGameManager.Instance.BoardManager.boardSize;
        var boardItems = InGameManager.Instance.BoardManager.boardItems;
        var colls = Physics2D.OverlapBoxAll(transform.position, new Vector2(.1f, boardSize * 2), 0);
        Item item;
        foreach (var coll in colls)
        {
            item = coll.gameObject.GetComponent<Item>();
            if (!newItems.Contains(item) && item != this)
            {
                newItems.Add(item);
            }
        }
        var colls2 = Physics2D.OverlapBoxAll(transform.position, new Vector2(.1f, boardSize * 2), 90);
        foreach (var coll in colls2)
        {
            item = coll.gameObject.GetComponent<Item>();
            if (!newItems.Contains(item) && item != this)
            {
                newItems.Add(item);
            }
        }
        for (int i = 0; i < boardItems.GetLength(0); i++)
        {
            for (int j = 0; j < boardItems.GetLength(1); j++)
            {
                if (i == I)
                {
                    item = boardItems[i, j];
                    if (!newItems.Contains(item) && item != this)
                    {
                        newItems.Add(item);
                    }
                }
            }
        }
        relatives = newItems.ToArray();
        return relatives;
    }

    internal void CheckConflict(bool isCheck)
    {


        if (!isCheck)
        {
            foreach (var item in GetRelavieItems())
            {
                item.SetConflict(false);
            }
            SetConflict(false);
            return;
        }
        if (VisualItemType == LevelData.ItemTypeEnum.None || isStartedEnabled)
        {
            return;
        }
        bool conflictFound = false;
        foreach (var item in GetRelavieItems())
        {
            if (item.VisualItemType == VisualItemType)
            {
                if (!conflictFound)
                {
                    conflictFound = true;
                }
                item.SetConflict(true);
            }
        }
        if (conflictFound)
        {
            SetConflict(true);
        }
    }
    internal void SetConflict(bool enable)
    {
        IsConflict = enable;
        channgeImageColorTeen.Pause();
        channgeImageColorTeen.Kill();
        Image image = isStartedEnabled ? hint_Image : _image;
        channgeImageColorTeen = image.DOColor(enable ? Color.Lerp(colorCache, Color.red, .25f) : colorCache, .25f);
    }
}
