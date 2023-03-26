
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelSetting))]
public class LevelSettingEditor : Editor
{

    public override void OnInspectorGUI()
    {
        LevelSetting levelSetting = (LevelSetting)target;
        var data = levelSetting.Data;
        int size = 20;
        int padding = 2;
        int xPadding = 0;
        int yPadding = 0;
        
        for (int i = 0; i < data.CellsArray.Length; i++)
        {
            LevelData.CellClass cell = data.CellsArray[i];
            for (int j = 0; j < cell.ItemsArray.Length; j++)
            {
                if (j == 6 || j == 7 || j == 8)
                {
                    yPadding = 5;
                }
                if (j==0 || j== 3 || j==6)
                {
                    xPadding = 5;
                }
                GUILayout.BeginArea( new Rect((size + padding) * (j % 3) + 3 * (size + padding+ xPadding) * (i % 3), (size + padding) * (j / 3) + 3 * (size + padding+ yPadding) * (i / 3), size, size));
                LevelData.ItemClass item = cell.ItemsArray[j];
                string textStr = item.Type.ToString();
                textStr = item.Type ==  LevelData.ItemTypeEnum.None ? "-": textStr[1].ToString();
                EditorGUILayout.LabelField(textStr, new GUIStyle { alignment = TextAnchor.MiddleCenter, normal = new GUIStyleState { textColor = item.StartEnabled ? Color.red : Color.white } });
                GUILayout.EndArea();
            }
        }
        GUILayout.Space(9 * (size + padding + yPadding) + 10);
        if (GUILayout.Button("Edit numbers"))
        {
            NumbersWindow.OpenWindow(levelSetting);
            
        }
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
        int size = 55;
        int padding = 5;
        int xPadding = 0;
        int yPadding = 0;
        for (int i = 0; i < data.CellsArray.Length; i++)
        {
            LevelData.CellClass cell = data.CellsArray[i];
            for (int j = 0; j < cell.ItemsArray.Length; j++)
            {
                if (j == 6 || j == 7 || j == 8)
                {
                    yPadding = 8;
                }
                if (j == 0 || j == 3 || j == 6)
                {
                    xPadding = 8;
                }
                GUILayout.BeginArea(new Rect((size + padding) * (j % 3) + 3 * (size + padding+ xPadding) * (i % 3), (size + padding) * (j / 3) + 3 * (size + padding+ yPadding) * (i / 3), size, size));
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
