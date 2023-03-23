using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName ="Level Setting", menuName = "Level/New Level Setting", order =0)]
public class LevelSetting : ScriptableObject
{
    public CellClass[] CellsArray = new CellClass[9];
    public enum ItemTypeEnum
    {
        n1, n2, n3, n4, n5, n6, n7,n8,n9,Black
    }
    [Serializable]
   public class CellClass
    {
        public ItemClass[] ItemsArray = new ItemClass[9];
    }
    [Serializable]  public class ItemClass {
        public ItemTypeEnum Type;
    }


}
