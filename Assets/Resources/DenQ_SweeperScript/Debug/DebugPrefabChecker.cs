using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using DenQ.BaseStruct;
public class DebugPrefabChecker : EditorWindow
{
    class PrefabScope
    {
        public string PrefabName;
        public bool Existing = false;

    }
    static List<PrefabScope> ScopeList = new  List<PrefabScope>();

    [MenuItem("Debug/PrefabChecker", true)]
    static bool OpenChecker()
    {
        return ResourcesHolder.ExistList();
    }
    [MenuItem("Debug/PrefabChecker")]
    static void ExecutePrefabChecker(MenuCommand command)
    {
        EditorWindow.GetWindow<DebugPrefabChecker>("PrefabChecker");
        ScopeList.Clear();
        foreach(PREFABU_NAME  name in Enum.GetValues(typeof(PREFABU_NAME)))
        {
            PrefabScope scopTemp = new PrefabScope();
            scopTemp.PrefabName = name.ToString();
            scopTemp.Existing = ResourcesHolder.GetPrefabByName(name) == null? false : true;
            ScopeList.Add(scopTemp);
        }
    }

    void OnGUI()
    {
        GUILayout.BeginHorizontal("box");
        {

            GUILayout.BeginVertical();
            {
                
                foreach(PrefabScope  element in ScopeList)
                {
                        GUILayout.BeginHorizontal("box");
                        {
                            GUILayout.Label(string.Format("{0:s}",element.Existing.ToString()));
                            GUILayout.Button(string.Format("{0}",element.PrefabName),GUILayout.Width(200));  
                        } 
                         GUILayout.EndHorizontal();
                }
            }
            GUILayout.EndVertical();
        }
        GUILayout.EndHorizontal();
    }
}
