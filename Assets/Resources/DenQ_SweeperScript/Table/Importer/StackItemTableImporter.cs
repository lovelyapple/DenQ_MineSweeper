using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Linq;
using System.IO;
using System.Text;

using DenQ;
using DenQData;
public class StackItemTableImporter : TableImporterBase
{
    void Awake()
    {
    }
    public override void PreImportData()
    {
        DenQDataBase.stackItemTable.Clear();
        filePath = "StackItemTable";
        isFinished = true;
    }
    public override void ImportData()
    {
        var data = new StackItemData();
        data.masterCode = Read_ulong("master_code");
        data.type = Read_uint("type");
        data.name = Read_string("name");

        if (DenQDataBase.stackItemTable.ContainsKey(data.masterCode)) return;
        DenQDataBase.stackItemTable.Add(data.masterCode, data);
    }
    public override void AfterImportData()
    {
        isFinished = true;
    }
    public static Dictionary<ulong, StackItemData> GetBombData()
    {
        return DenQDataBase.stackItemTable;
    }
}
