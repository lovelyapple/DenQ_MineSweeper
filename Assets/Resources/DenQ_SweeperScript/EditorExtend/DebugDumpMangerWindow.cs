using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DenQ.Mgr;
using System;

[FlagsAttribute]
public enum DUMP_TYPE
{
    SYS_DEBUG = 1 << 0,
    SYS_WARNING = 1 << 1,
    SYS_ERROR = 1 << 2,
    GAM_DEBUG = 1 << 3,
    GAM_WARMING = 1 << 4,
    GAM_ERROR = 1 << 5,
    SHOW_ALL = SYS_DEBUG | SYS_WARNING | SYS_ERROR | GAM_DEBUG | GAM_WARMING | GAM_ERROR,
}

public class DebugInformation
{
    public DUMP_TYPE dumpType;
    public string dumpMsg;

    public DebugInformation(DUMP_TYPE type, string msg)
    {
        dumpType = type;
        dumpMsg = msg;
    }

    public void ShowMsg()
    {
        if (dumpMsg != null)
            Debug.Log(dumpMsg);
    }
}
public class DebugDumpMangerWindow : EditorWindow
{
    int dumController = (int)DUMP_TYPE.SHOW_ALL;
    [MenuItem("Debug/DebugDumpMangerWindow", true)]
    static bool CheckWindow()
    {
        return true;
    }

    [MenuItem("Debug/DebugDumpMangerWindow")]
    static void OpenDebugDumpManger()
    {

        EditorWindow.GetWindow<DebugDumpMangerWindow>("DebugDumpMangerWindow");
    }
    void Awake()
    {
        dumController = (int)DUMP_TYPE.SHOW_ALL;
    }
    void OnGUI()
    {
        GUILayout.BeginVertical("box");
        {
            foreach (DUMP_TYPE type in Enum.GetValues(typeof(DUMP_TYPE)))
            {
                GUILayout.BeginHorizontal("Box");
                {
                    int stepOne = (int)type & dumController;
                    GUILayout.Label(string.Format("{0:s}", stepOne == 0 ? "false" : "true"));
                    if (GUILayout.Button(string.Format("{0:s}",type.ToString()),GUILayout.Width(150)))
                    {
                        dumController ^= (int)type;
                        DenQLogger.UpdateType(dumController);
                    }
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.Label(string.Format(Convert.ToString(dumController, 2)));
        }
        GUILayout.EndVertical();
    }
}

