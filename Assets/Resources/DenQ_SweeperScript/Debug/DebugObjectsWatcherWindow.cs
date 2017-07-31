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
            op = (ObjectType)EditorGUILayout.EnumPopup("Select a Type:", op);
            if (GUILayout.Button("Search!"))// && isSearchFinished)
            {
                Search();
            }
			UpdateShow();
        }
        GUILayout.EndVertical();
    }
    void Search()
    {
        objList.Clear();
        //isSearchFinished = false;
        objList = GameObjectsManager.GetInstance().GetObjectBaseDataByType(op);
    }
	void UpdateShow()
	{
        if (objList == null ||objList.Count <= 0) { return; }
		scrollPosition = GUILayout.BeginScrollView(scrollPosition);
		foreach(var obj in objList)
		{
			//GUILayout.BeginHorizontal();
			{
				if(obj == null)continue;
				GUILayout.Label(obj.objectId.ToString(),GUILayout.Width(100.0f));
				EditorGUILayout.ObjectField (obj,typeof(ObjectBaseData),true);
			}
			//GUILayout.EndHorizontal();
		}
		GUILayout.EndScrollView();
	}
}
