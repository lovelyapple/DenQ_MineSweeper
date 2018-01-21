using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourcesPath
{
    //system
    static string _applicationPath = string.Empty;
    public static string ApplicationPath
    {
        get
        {
            if (string.IsNullOrEmpty(_applicationPath))
            {
                _applicationPath = Application.dataPath;
            }

            return _applicationPath;
        }
    }
    public static int PrefabCharaCnt
    {
        get { return ".prefab".Length; }
    }
    //ExternalResources

    public static string TableMasterPath
    {
        get { return ApplicationPath + "/ExternalResources/TableCSV/"; }
    }
    //Resources
    public static string ResourcesDirectoryPath
    {
        get { return ApplicationPath + "/Resources/"; }
    }
    public static string GetFxFileName(uint fxId)
    {
        return "Fx_" + fxId;
    }
    public static string GetFieldBlockName(uint fxId)
    {
        return "FieldBlock_" + fxId;
    }
    public static string GetFieldObjectName(uint objToken, uint objId)
    {
        return "FieldObject_" + objToken + "_" + objId;
    }
}
