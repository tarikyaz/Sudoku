using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName ="Level Setting", menuName = "Level/New Level Setting", order =0)]
public class LevelSetting : ScriptableObject
{
    [SerializeField] private LevelData data = new LevelData();

    public LevelData Data { get => data; set => data = value; }

    public LevelData.ItemClass[,] GetArrayOfNumbers()
    {
        LevelData.ItemClass[,] arrayOfNumbers = new LevelData.ItemClass[9, 9];
        for (int i = 0; i < Data.CellsArray.Length; i++)
        {
            LevelData.CellClass cell = Data.CellsArray[i];
            for (int j = 0; j < Data.CellsArray.Length; j++)
            {
                LevelData.ItemClass item = cell.ItemsArray[j];
                arrayOfNumbers[i, j] = item;
            }
        }
        return arrayOfNumbers;
    }



}
[Serializable]
public class LevelData
{
    public CellClass[] CellsArray = new CellClass[9];
    [Serializable]
    public class ItemClass
    {
        public ItemTypeEnum Type;
        public bool StartEnabled = false;
    }
    [Serializable]
    public class CellClass
    {
        public ItemClass[] ItemsArray = new ItemClass[9];
    }
    public enum ItemTypeEnum
    {
        None,
        n1, n2, n3, n4, n5, n6, n7, n8, n9
    }
}
