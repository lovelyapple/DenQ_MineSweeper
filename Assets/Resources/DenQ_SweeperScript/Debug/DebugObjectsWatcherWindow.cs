using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using DenQ;
using DenQ.Mgr;


public class DebugObjectsWatcherWindow : EditorWindow
{

    [MenuItem("Debug/DebugObjectsWatcherWindow", true)]
    static bool CheckWindow()
    {
        return true;
    }

    [MenuItem("Debug/DebugObjectsWatcherWindow")]
    static void OpenObjectsWatcher()
    {

        EditorWindow.GetWindow<DebugObjectsWatcherWindow>("DebugObjectsWatcherWindow");
    }

    List<ObjectBaseData> objList = new List<ObjectBaseData>();
    Vector2 scrollPosition = Vector2.zero;
    public ObjectType op;
    public long id;
    //bool isSearchFinished = true;
    void OnGUI()
    {
        GUILayout.BeginVertical("box");
        {
            GUILayout.BeginHorizontal();
            {
                op = (ObjectType)EditorGUILayout.EnumPopup("Select by Type:", op);
                if (GUILayout.Button("Search!", GUILayout.Width(50)))// && isSearchFinished)
                {
                    SearchByType();
                }
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label("Search by ID:");
                id = int.Parse(EditorGUILayout.TextArea(id.ToString(),GUILayout.Width(50.0f)));
                if (GUILayout.Button("Search!", GUILayout.Width(50)))// && isSearchFinished)
                {
                    SearchById();
                }
            }
            GUILayout.EndHorizontal();
            GUILayout.Label("==============================");
            UpdateShow();
            if(objList.Count<=0)
            {
                GUILayout.Label("not found");
            }
        }
        GUILayout.EndVertical();
    }
    void SearchByType()
    {
        objList.Clear();
        //isSearchFinished = false;
        objList = GameObjectsManager.GetInstance().GetObjectBaseDataByType(op);
    }
    void SearchById()
    {
        objList.Clear();
        var obj = GameObjectsManager.GetInstance().GetObjectBaseDataByID(id);
        if (obj != null)
        {
            objList.Add(obj);
        }
    }
    void UpdateShow()
    {
        if (objList == null || objList.Count <= 0) { return; }
        scrollPosition = GUILayout.BeginScrollView(scrollPosition);
        foreach (var obj in objList)
        {
            GUILayout.BeginHorizontal();
            {
                if (obj == null) continue;
                GUILayout.Label(obj.objectId.ToString(), GUILayout.Width(40.0f));
                EditorGUILayout.ObjectField(obj, typeof(ObjectBaseData), true);
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndScrollView();
    }
}
