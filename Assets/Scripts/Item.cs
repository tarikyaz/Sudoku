using DG.Tweening;
using System;
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
    bool _isConflict = false;
    public void Init(LevelData.ItemTypeEnum type, bool startEnabled, int i, int j)
    {
        normalColorCache = _image.color;
        hintColorCache = hint_Image.color;
        itemCurrectType = type;
        isStartedEnabled = startEnabled;
        I = i;
        J = j;
        hint_Image.gameObject.SetActive(true);
        hintImage_CanvasGroup.alpha = isStartedEnabled ? 1:0;

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

        Debug.Log(ItsCurrect ? "currect" : "wrong");
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
            channgeHintImageColorTeen = hintImage_CanvasGroup.DOFade(enable ? 1 : 0, .5f);
            ChangeImageColor(colorCache);
        }
    }
    void ChangeImageColor(Color newColor)
    {
        if (_isConflict)
        {
            return;
        }
        channgeImageColorTeen.Pause();
        channgeImageColorTeen.Kill();
        Image image = isStartedEnabled ? hint_Image : _image;
        channgeImageColorTeen = image.DOColor(newColor, .25f);
        Debug.Log(image.name +" change color to " + newColor);
    }

    internal void SetConflict(bool isConflict)
    {
        Debug.Log("conflict " + true);
        ChangeImageColor(isConflict ? Color.red : colorCache);
        _isConflict = isConflict;
    }
}
