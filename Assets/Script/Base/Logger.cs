using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Logger
{
    private static int MsgTypeController = (int)DUMP_TYPE.SHOW_ALL;
    public static void SDebug(string msg)
    {
        DebugInformation info = new DebugInformation(DUMP_TYPE.SYS_DEBUG, msg);
        Runing(info);
    }
    public static void SWarn(string msg)
    {
        DebugInformation info = new DebugInformation(DUMP_TYPE.SYS_WARNING, msg);
        Runing(info);
    }
    public static void SError(string msg)
    {
        DebugInformation info = new DebugInformation(DUMP_TYPE.SYS_ERROR, msg);
        Runing(info);
    }
    public static void GDebug(string msg)
    {
        DebugInformation info = new DebugInformation(DUMP_TYPE.GAM_DEBUG, msg);
        Runing(info);
    }
    public static void GWarn(string msg)
    {
        DebugInformation info = new DebugInformation(DUMP_TYPE.GAM_WARMING, msg);
        Runing(info);
    }
    public static void GError(string msg)
    {
        DebugInformation info = new DebugInformation(DUMP_TYPE.GAM_ERROR, msg);
        Runing(info);
    }
    public static void SErrorId(long objId, string msg)
    {
        SError(string.Format("The ObjId with :{0},has a problem {1}", objId, msg));
    }
    public static void SWarnId(long objId, string msg)
    {
        SWarn(string.Format("The ObjId with :{0},has a problem {1}", objId, msg));
    }
    public static void UpdateType(int type)
    {
        MsgTypeController = type;
    }
    public static void Runing(DebugInformation info )
    {
        if (((int)info.dumpType & MsgTypeController) != 0)
        {
            info.ShowMsg();
        }

    }
}