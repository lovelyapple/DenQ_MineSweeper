using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class DebugTableTester : EditorWindow
{
    [MenuItem("Debug/TableHelper")]
    static void Opend()
    {
        EditorWindow.GetWindow<DebugTableTester>("TableHelper");
    }
    // Use this for initialization
    void OnGUI()
    {
        GUILayout.BeginVertical("box");
        {
            GUILayout.Label("DebugTalbeHelper");
            //if (EditorApplication.isPlaying)
            {
                if (GUILayout.Button("Debug Write"))
                    BombTableImporter.DebugWriteData();
            }
            {
                if (GUILayout.Button("Debug Reader"))
                    BombTableImporter.DebugReader();
            }
            //else
            {
                GUILayout.Label("only used in playing mode");
            }
        }
        GUILayout.EndVertical();
    }
}
