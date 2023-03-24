using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

[CustomEditor(typeof(LevelSetting))]
public class LevelSettingEditor : Editor
{

    public override void OnInspectorGUI()
    {

        if (GUILayout.Button("Open numbers window"))
        {
            LevelSetting levelSetting = (LevelSetting)target;

            NumbersWindow.OpenWindow(levelSetting);
        }
        return;


    }
}
class NumbersWindow : EditorWindow {
    static LevelSetting _levelSetting;
    public static void OpenWindow(LevelSetting levelSetting)
    {
        _levelSetting = levelSetting;
        GetWindow<NumbersWindow>("Level " + levelSetting.name + " numbers");
    }
    private void OnGUI()
    {
        if (_levelSetting ==null)
        {
            Close();
            return;
        }
        var data = _levelSetting.Data;
        for (int i = 0; i < data.CellsArray.Length; i++)
        {
            LevelData.CellClass cell = data.CellsArray[i];
            for (int j = 0; j < cell.ItemsArray.Length; j++)
            {
                int size = 55;
                int padding = 5;
                GUILayout.BeginArea(new Rect((size + padding) * (j % 3) + 3 * (size + padding) * (i % 3), (size + padding) * (j / 3) + 3 * (size + padding) * (i / 3), size, size));
                LevelData.ItemClass item = cell.ItemsArray[j];
                EditorGUILayout.LabelField($"{i + 1}x{j + 1}", new GUIStyle { alignment = TextAnchor.MiddleCenter, normal = new GUIStyleState { textColor = Color.white } });
                item.Type = (LevelData.ItemTypeEnum)EditorGUILayout.EnumPopup(item.Type);
                item.StartEnabled = EditorGUILayout.Toggle(item.StartEnabled);
                GUILayout.EndArea();
            }
        }
        _levelSetting.Data = data;
        EditorUtility.SetDirty(_levelSetting);
    }
}
