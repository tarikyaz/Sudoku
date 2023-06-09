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
    Item selectedItem;
    internal Item[,] boardItems { get; private set; }
    internal float boardSize => GetComponent<RectTransform>().sizeDelta.x;
    bool hintEnabled = false;
    LevelManager currentLevel;
    LevelSetting currentLevelSetting => currentLevel !=null ?  currentLevel.currentLevelSetting : null;
    struct NumSeries {
        public int a, b, c;
    }
   public void Init(LevelManager levelManager)
    {
        currentLevel = levelManager;
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
        CheckConflict();
        if (GameIsFinished())
        {
            LevelIsDone();
        }

    }

    private static void LevelIsDone()
    {
        BaseEvents.CallOnLevelFinish(true);
        Debug.Log("Game is finished");
    }

    void CheckConflict()
    {
        for (int i = 0; i < boardItems.GetLength(0); i++)
        {
            for (int j = 0; j < boardItems.GetLength(1); j++)
            {
                Item item = boardItems[i, j];
                item.CheckConflict(false);
            }
        }
        for (int i = 0; i < boardItems.GetLength(0); i++)
        {
            for (int j = 0; j < boardItems.GetLength(1); j++)
            {
                Item item = boardItems[i, j];
                item.CheckConflict(true);
            }

        }
    }

    bool GameIsFinished()
    {
        bool isCurrect = true;
        for (int i = 0; i < boardItems.GetLength(0); i++)
        {
            for (int j = 0; j < boardItems.GetLength(1); j++)
            {
                if (selectedItem != boardItems[i, j])
                {
                    if (!boardItems[i, j].ItsCurrect)
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
        currentLevel.keyboard.Show();
    }

    private void OnMouseLeaveItemHandler(Item _item)
    {
        if (hintEnabled)
        {
            return;
        }
        for (int i = 0; i < boardItems.GetLength(0); i++)
        {
            for (int j = 0; j < boardItems.GetLength(1); j++)
            {
                if (selectedItem != boardItems[i, j])
                {
                    boardItems[i, j].ResetColor();
                }
            }
        }

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
        foreach (var item in _item.GetRelavieItems())
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
        if (currentLevelSetting != null)
        {
            levelNumbers = currentLevelSetting.GetArrayOfNumbers();
        }
        boardItems = new Item[9, 9];
        for (int i = 0; i < CellsArray.Length; i++)
        {
            Cell cell = CellsArray[i];
            for (int j = 0; j < CellsArray.Length; j++)
            {
                Item item = cell.ItemsArray[j];
                
                boardItems[i, j] = item;
                if (levelNumbers == null)
                {
                    item.SetText($"{i + 1}x{j + 1}",true);
                }
                else
                {
                    item.Init(currentLevel, levelNumbers[i, j].Type, levelNumbers[i,j].StartEnabled, i,j);
                }
            }
        }
    }
    public void OnClickOutSide()
    {
        if (selectedItem !=null)
        {
            selectedItem = null;
        }
        for (int i = 0; i < boardItems.GetLength(0); i++)
        {
            for (int j = 0; j < boardItems.GetLength(1); j++)
            {
                boardItems[i, j].ResetColor();
            }
        }
        currentLevel.keyboard.Hide();
    }

    internal void ShowHint(Action onFinisHint)
    {
        if (hintEnabled)
        {
            return;
        }
        hintEnabled = true;
        List<Item> items = new List<Item>();
        OnClickOutSide();
        for (int i = 0; i < boardItems.GetLength(0); i++)
        {
            for (int j = 0; j < boardItems.GetLength(1); j++)
            {
                Item item = boardItems[i, j];
                item.CheckConflict(false);
            }
        }
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
            for (int i = 0; i < boardItems.GetLength(0); i++)
            {
                for (int j = 0; j < boardItems.GetLength(1); j++)
                {
                    Item item = boardItems[i, j];
                    item.CheckConflict(true);
                }
            }
            onFinisHint?.Invoke();
            hintEnabled = false;
        });
    }
}
