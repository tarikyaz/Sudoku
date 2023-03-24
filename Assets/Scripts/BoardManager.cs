using System;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    [SerializeField] RectTransform canvasRec;
    [SerializeField] GridLayoutGroup layoutGroup;
    [SerializeField] Cell[] CellsArray = new Cell[9];
    [SerializeField] LevelSetting levelTest;
    [SerializeField] RectTransform cornerPoint;
    Item[,] items = new Item[9, 9];
    float boardSize => Vector2.Distance(transform.position, cornerPoint.transform.position)*2;
    struct NumSeries {
        public int a, b, c;
    }
    void Start()
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
        int itemSize = Mathf.FloorToInt( cellSize / 3);
        foreach (var cell in CellsArray)
        {
            cell.Init(itemSize);
        }
        InitNumbers();
    }

    private void OnEnable()
    {
        Item.OnMouseEnter += OnMouseEnterItemHandler;
        Item.OnMouseLeave += OnMouseLeaveItemHandler;
    }
    
    private void OnDisable()
    {
        Item.OnMouseEnter -= OnMouseEnterItemHandler;
        Item.OnMouseLeave -= OnMouseLeaveItemHandler;
    }

    private void OnMouseLeaveItemHandler(Item _item)
    {

        for (int i = 0; i < items.GetLength(0); i++)
        {
            for (int j = 0; j < items.GetLength(1); j++)
            {
                items[i,j].ResetColor();

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
        _item.SetelectColor();
        foreach (var item in GetRelavieItems(_item))
        {
            item.SetSiblingSelectColor();
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
                    item.SetText($"{i + 1}x{j + 1}");
                }
                else
                {
                    item.SetType(levelNumbers[i, j].Type, levelNumbers[i,j].StartEnabled, i,j);
                }
            }
        }
    }
}
