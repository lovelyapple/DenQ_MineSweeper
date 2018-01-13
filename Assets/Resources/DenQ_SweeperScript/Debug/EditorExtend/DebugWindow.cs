using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DebugWindow : EditorWindow
{
    [MenuItem("Debug/DebugWindow")]
    static void Open()
    {
        EditorWindow.GetWindow<DebugWindow>("DebugWindow");
    }
    void OnGUI()
    {
        GUILayout.BeginHorizontal("box", GUILayout.MaxWidth(1000));
        GUILayout.BeginVertical();
        {
            if (GUILayout.Button("CreateFeild"))
            {
                DebugCreateField();
            }
        }
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
    }
    void DebugCreateField()
    {
        FieldManager.GetInstance().CreateField(10000000);
    }
}
