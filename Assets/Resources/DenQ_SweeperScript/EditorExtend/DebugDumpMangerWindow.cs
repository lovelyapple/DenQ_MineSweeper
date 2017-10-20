using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DenQ;
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
        {
#if UNITY_EDITOR
            switch((DUMP_TYPE)dumpType)
            {
                case DUMP_TYPE.GAM_DEBUG:
                case DUMP_TYPE.SYS_DEBUG:
                Debug.Log(dumpMsg);
                break;
                case DUMP_TYPE.GAM_WARMING:
                case DUMP_TYPE.SYS_WARNING:
                Debug.LogWarning(dumpMsg);
                break;
                case DUMP_TYPE.GAM_ERROR:
                case DUMP_TYPE.SYS_ERROR:
                Debug.LogError(dumpMsg);
                break;
            }
            
#endif
        }
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
                    if (GUILayout.Button(string.Format("{0:s}", type.ToString()), GUILayout.Width(150)))
                    {
                        dumController ^= (int)type;
                        Logger.UpdateType(dumController);
                    }
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.Label(string.Format(Convert.ToString(dumController, 2)));
        }
    }
}

