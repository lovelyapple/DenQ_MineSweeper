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
    Dictionary<DUMP_TYPE, int> DumpList = null;

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
        CreateList();
    }
    void CreateList()
    {
        DumpList = new Dictionary<DUMP_TYPE, int>();
        foreach (DUMP_TYPE type in Enum.GetValues(typeof(DUMP_TYPE)))
        {
            DumpList.Add(type, (int)type);
        }
    }
    void OnGUI()
    {
        if (DumpList == null)
        {
           CreateList();
        }
        GUILayout.BeginVertical("box");
        {

            foreach (DUMP_TYPE type in Enum.GetValues(typeof(DUMP_TYPE)))
            {
                GUILayout.BeginHorizontal();
                {
                    int res = (int)type & (int)DumpList[type];
                    GUILayout.Label(res == 0 ? "false" : "true",GUILayout.Width(50));
                    if (GUILayout.Button(type.ToString()))
                    {
                        DumpList[type] = ~DumpList[type];
                        int MsgCtrl = 0;
                        foreach (DUMP_TYPE t in DumpList.Keys)
                        {
                            MsgCtrl |= ((int)DumpList[t]&(int)t);
                            DenQLogger.UpdateType(MsgCtrl);
                        }
                    }
                    //2進数で表示
                    //GUILayout.Label(string.Format(Convert.ToString(DumpList[type],2)));
                }
                GUILayout.EndHorizontal();
            }
        }
        GUILayout.EndVertical();
    }
}

