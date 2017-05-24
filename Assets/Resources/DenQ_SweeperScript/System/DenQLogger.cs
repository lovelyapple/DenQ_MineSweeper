using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DenQLogger:MonoBehaviour
{
    private static List<DebugInformation> infoList = new List<DebugInformation>();
	private static int MsgTypeController = (int)DUMP_TYPE.SHOW_ALL;
    public static void SDebug(string msg)
    {
        DebugInformation info = new DebugInformation(DUMP_TYPE.SYS_DEBUG, msg);
        infoList.Add(info);
    }
    public static void SWarn(string msg)
    {
        DebugInformation info = new DebugInformation(DUMP_TYPE.SYS_WARNING, msg);
        infoList.Add(info);
    }
    public static void SError(string msg)
    {
        DebugInformation info = new DebugInformation(DUMP_TYPE.SYS_ERROR, msg);
        infoList.Add(info);
    }
    public static void GDebug(string msg)
    {
        DebugInformation info = new DebugInformation(DUMP_TYPE.GAM_DEBUG, msg);
        infoList.Add(info);
    }
    public static void GWarn(string msg)
    {
        DebugInformation info = new DebugInformation(DUMP_TYPE.GAM_WARMING, msg);
        infoList.Add(info);
    }
    public static void GError(string msg)
    {
        DebugInformation info = new DebugInformation(DUMP_TYPE.GAM_ERROR, msg);
        infoList.Add(info);
    }
	public static void UpdateType(int type)
	{
		MsgTypeController = type;
	}
	public static void ClearList()
	{
		infoList.Clear();
	}
	public static void Runing()
	{
		SDebug("System debug");
		SWarn("System warning");
		SError("System error");

		GDebug("Game debug");
		GWarn("Game warning");
		GError("Game error");
        
		if(infoList.Count <= 0)
		{
			return;
		}
		foreach(DebugInformation info in infoList)
		{
			if(((int)info.dumpType & MsgTypeController) != 0)
			{
				info.ShowMsg();
			}
		}
		ClearList();
	}
}