using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    [SerializeField] RectTransform canvasRec;
    [SerializeField] GridLayoutGroup layoutGroup;
    [SerializeField] Cell[] CellsArray = new Cell[9];
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
    }

    
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            for (int i = 0; i < CellsArray.Length; i++)
            {
                Cell cell = CellsArray[i];
                for (int j = 0; j < CellsArray.Length; j++)
                {
                    Item item = cell.ItemsArray[j];
                    item.SetText($"{i + 1}x{j + 1}");
                }
            }
        }
    }
}
