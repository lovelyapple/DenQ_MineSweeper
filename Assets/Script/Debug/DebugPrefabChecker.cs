using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using DenQ;
public class DebugPrefabChecker : EditorWindow
{
//とりあえず、何もしない
    [MenuItem("Debug/PrefabChecker", true)]
    static bool OpenChecker()
    {
        if(!Application.isPlaying)
        {
            return false;
        }
        return true;
    }
    [MenuItem("Debug/PrefabChecker")]
    static void ExecutePrefabChecker(MenuCommand command)
    {
        EditorWindow.GetWindow<DebugPrefabChecker>("PrefabChecker");
    }

    void OnGUI()
    {
        GUILayout.BeginHorizontal("box");
        {

            GUILayout.BeginVertical();
            {
            }
            GUILayout.EndVertical();
        }
        GUILayout.EndHorizontal();
    }
}
