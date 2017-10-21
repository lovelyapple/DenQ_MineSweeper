using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DebugWindow : EditorWindow
{
    void OnGUI()
    {
        GUILayout.BeginHorizontal("box", GUILayout.MaxWidth(1000));
        GUILayout.BeginVertical();
        {
			
        }
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
    }
	void DebugCreateField()
	{

	}
}
