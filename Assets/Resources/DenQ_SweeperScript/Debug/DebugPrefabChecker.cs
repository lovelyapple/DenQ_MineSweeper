using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using DenQ.BaseStruct;
public class DebugPrefabChecker : EditorWindow
{
    static Dictionary<PREFAB_NAME ,bool> ExistList = new Dictionary<PREFAB_NAME ,bool>();

    [MenuItem("Debug/PrefabChecker", true)]
    static bool OpenChecker()
    {
        ExistList = ResourcesManager.GetInstance().GetExistList();
        return ExistList.Count > 0;
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
                
                foreach(PREFAB_NAME  element in ExistList.Keys)
                {
                        GUILayout.BeginHorizontal("box");
                        {
                            GUILayout.Label(string.Format("{0:s}",element.ToString()));
                            GUILayout.Button(string.Format("{0:s}",ExistList[element].ToString()),GUILayout.Width(200));  
                        } 
                         GUILayout.EndHorizontal();
                }
            }
            GUILayout.EndVertical();
        }
        GUILayout.EndHorizontal();
    }
}
