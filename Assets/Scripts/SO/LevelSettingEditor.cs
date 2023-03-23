using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelSetting))]
public class LevelSettingEditor : Editor
{

    public override void OnInspectorGUI()
    {
        LevelSetting levelSetting = (LevelSetting)target;

        for (int i = 0; i < levelSetting.CellsArray.Length; i++)
        {
            LevelSetting.CellClass cell = levelSetting.CellsArray[i];
            for (int j = 0; j < cell.ItemsArray.Length; j++)
            {
                int size = 55;
            //   GUILayout.BeginArea(new Rect(size * (j % 3), size * (j / 3), size, size));
                LevelSetting.ItemClass item = cell.ItemsArray[j];
                EditorGUILayout.LabelField($"{i + 1}x{j + 1}", new GUIStyle { alignment = TextAnchor.MiddleCenter, normal = new GUIStyleState { textColor = Color.white } });
                item.Type = (LevelSetting.ItemTypeEnum)EditorGUILayout.EnumPopup(item.Type);

                //  GUILayout.EndArea();
            }
            GUILayout.Space(20);
        }

    }
}
