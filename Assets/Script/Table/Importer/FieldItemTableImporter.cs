﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Linq;
using System.IO;
using System.Text;

using DenQ;
using DenQData;
public class FieldItemTableImporter : TableImporterBase
{
    void Awake()
    {
    }
    public override void PreImportData()
    {
        DenQDataBase.fieldItemTable.Clear();
        filePath = "FieldItemTable";
        isFinished = true;
    }
    public override void ImportData()
    {
        var data = new FieldItemData();
        data.masterCode = Read_ulong("master_code");
        data.name = Read_string("name");
        if (DenQDataBase.fieldItemTable.ContainsKey(data.masterCode)) return;
        DenQDataBase.fieldItemTable.Add(data.masterCode, data);
    }
    public override void AfterImportData()
    {
        isFinished = true;
    }
    public static Dictionary<ulong, FieldItemData> GetFieldItemDatas()
    {
        return DenQDataBase.fieldItemTable;
    }
}
public static class FieldItemTableHelper
{
    public static FieldItemData GetFieldItemData(ulong code)
    {
        var db = DenQDataBase.fieldItemTable;
        var outData = new FieldItemData();
        if (!db.TryGetValue(code, out outData))
        {
            Logger.SWarn("could not find fielditem Id : " + code);
        }
        return outData;
    }
    public static FieldItemData GetFieldItemData(string name)
    {
        var db = DenQDataBase.fieldItemTable;
        FieldItemData outData = db.Values.Single(n => n.name == name);
        if (outData == null)
        {
            Logger.SWarn("could not find fieldItem name : " + name);
        }
        return outData;
    }
    public static FieldItemData GetNormalBlockitemData()
    {
        return GetFieldItemData(10001000);
    }
}