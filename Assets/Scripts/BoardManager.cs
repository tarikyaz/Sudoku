using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class BoardManager : MonoBehaviour
{
    [SerializeField] RectTransform canvasRec;
    [SerializeField] GridLayoutGroup layoutGroup;
    [SerializeField] Cell[] CellsArray = new Cell[9];
    [SerializeField] LevelSetting levelTest;
    Item selectedItem;
    Item[,] items = new Item[9, 9];
    float boardSize => GetComponent<RectTransform>().sizeDelta.x;
    bool hintEnabled = false;
    struct NumSeries {
        public int a, b, c;
    }
   public void Init()
    {
        InitScreen();
        InitNumbers();
    }

    private void InitScreen()
    {
        float minValue = Mathf.Min(canvasRec.sizeDelta.x, canvasRec.sizeDelta.y);
        float newBoardSize = minValue - .1f * minValue;
        GetComponent<RectTransform>().sizeDelta = new Vector2(newBoardSize, newBoardSize);
        int cellSize = Mathf.FloorToInt(newBoardSize / 3);
        float cellPadding = layoutGroup.spacing.magnitude / layoutGroup.cellSize.magnitude;
        cellPadding *= cellSize;
        cellSize = Mathf.FloorToInt(cellSize - cellPadding);
        cellPadding = Mathf.FloorToInt(cellPadding);
        layoutGroup.cellSize = new Vector2(cellSize, cellSize);
        layoutGroup.spacing = new Vector2(cellPadding, cellPadding);
        int itemSize = Mathf.FloorToInt(cellSize / 3);
        foreach (var cell in CellsArray)
        {
            cell.Init(itemSize);
        }
    }

    private void OnEnable()
    {
        Item.OnMouseEnter += OnMouseEnterItemHandler;
        Item.OnMouseLeave += OnMouseLeaveItemHandler;
        Item.OnMouseClick += OnMouseClickItemHandler;
        Key.OnKeyClicked += OnKeyClickedHandler;
    }



    private void OnDisable()
    {
        Item.OnMouseEnter -= OnMouseEnterItemHandler;
        Item.OnMouseLeave -= OnMouseLeaveItemHandler;
        Item.OnMouseClick -= OnMouseClickItemHandler;
        Key.OnKeyClicked -= OnKeyClickedHandler;

    }


    private void OnKeyClickedHandler(LevelData.ItemTypeEnum obj)
    {
        if (hintEnabled)
        {
            return;
        }
        if (selectedItem != null)
        {
            selectedItem.SetType(obj);
        }
        if (GameIsFinished())
        {
            Debug.Log("Game is finished");
        }

    }
    bool GameIsFinished()
    {
        bool isCurrect = true;
        for (int i = 0; i < items.GetLength(0); i++)
        {
            for (int j = 0; j < items.GetLength(1); j++)
            {
                if (selectedItem != items[i, j])
                {
                    if (!items[i, j].ItsCurrect)
                    {
                        isCurrect = false;
                        break;

                    }
                }
                if (!isCurrect)
                {
                    break;
                }
            }
        }
        return isCurrect;
    }
    private void OnMouseClickItemHandler(Item _item)
    {
        if (hintEnabled)
        {
            return;
        }
        if (selectedItem != null)
        {
            selectedItem.ResetColor();
            selectedItem = null;
        }
        selectedItem = _item;
        OnMouseEnterItemHandler(_item);
        selectedItem.SetClickColor();
        InGameManager.Instance.keyboard.Show();
    }

    private void OnMouseLeaveItemHandler(Item _item)
    {
        if (hintEnabled)
        {
            return;
        }
        for (int i = 0; i < items.GetLength(0); i++)
        {
            for (int j = 0; j < items.GetLength(1); j++)
            {
                if (selectedItem != items[i, j])
                {
                    items[i, j].ResetColor();
                }
            }
        }

    }
    Item[] GetRelavieItems(Item _item)
    {
        List<Item> newItems = new List<Item>();
        var colls = Physics2D.OverlapBoxAll(_item.transform.position, new Vector2(.1f, boardSize * 2), 0);
        Item item;
        foreach (var coll in colls)
        {
            item = coll.gameObject.GetComponent<Item>();
            if (!newItems.Contains(item) && item != _item)
            {
                newItems.Add(item);
            }
        }
        var colls2 = Physics2D.OverlapBoxAll(_item.transform.position, new Vector2(.1f, boardSize * 2), 90);
        foreach (var coll in colls2)
        {
            item = coll.gameObject.GetComponent<Item>();
            if (!newItems.Contains(item) && item != _item)
            {
                newItems.Add(item);
            }
        }
        for (int i = 0; i < items.GetLength(0); i++)
        {
            for (int j = 0; j < items.GetLength(1); j++)
            {
                if (i == _item.I)
                {
                    item = items[i, j];
                    if (!newItems.Contains(item) && item != _item)
                    {
                        newItems.Add(item);
                    }
                }
            }
        }
        return newItems.ToArray();

    }
    private void OnMouseEnterItemHandler(Item _item)
    {
        if (hintEnabled)
        {
            return;
        }
        if (selectedItem != _item)
        {
            _item.SetSlelectColor();
        }
        foreach (var item in GetRelavieItems(_item))
        {
            if (selectedItem != item)
            {
                item.SetSiblingSelectColor();
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            InitNumbers();
        }
    }

    private void InitNumbers()
    {
        LevelData.ItemClass[,] levelNumbers = null;
        if (levelTest != null)
        {
            levelNumbers = levelTest.GetArrayOfNumbers();
        }

        for (int i = 0; i < CellsArray.Length; i++)
        {
            Cell cell = CellsArray[i];
            for (int j = 0; j < CellsArray.Length; j++)
            {
                Item item = cell.ItemsArray[j];
                items[i, j] = item;
                if (levelNumbers == null)
                {
                    item.SetText($"{i + 1}x{j + 1}",true);
                }
                else
                {
                    item.Init(levelNumbers[i, j].Type, levelNumbers[i,j].StartEnabled, i,j);
                }
            }
        }
    }
    public void OnClickOutSide()
    {
        if (hintEnabled)
        {
            return;
        }
        if (selectedItem !=null)
        {
            selectedItem = null;
        }
        for (int i = 0; i < items.GetLength(0); i++)
        {
            for (int j = 0; j < items.GetLength(1); j++)
            {
                items[i, j].ResetColor();
            }
        }
        InGameManager.Instance.keyboard.Hide();
    }

    internal void ShowHint(Action onFinisHint)
    {
        if (hintEnabled)
        {
            return;
        }
        hintEnabled = true;
        List<Item> items = new List<Item>();
        foreach (var cell in CellsArray)
        {
            foreach (var item in cell.ItemsArray)
            {
                items.Add(item);
                item.TuggleHint(true);
            }
        }
        DOVirtual.DelayedCall(3, () => {
            foreach (var item in items)
            {
                item.TuggleHint(false);
            }
            onFinisHint?.Invoke();
            hintEnabled = false;
        });
    }
}
