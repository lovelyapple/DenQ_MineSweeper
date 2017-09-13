using System.Collections;
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
        DenQOffLineDataBase.fieldItemTable.Clear();
        filePath = "FieldItemTable";
        isFinished = true;
    }
    public override void ImportData()
    {
        var data = new FieldItemData();
        data.masterCode = Read_ulong("master_code");
        data.type = Read_uint("type");
        data.name = Read_string("name");

        if (DenQOffLineDataBase.fieldItemTable.ContainsKey(data.masterCode)) return;
        DenQOffLineDataBase.fieldItemTable.Add(data.masterCode, data);
    }
    public override void AfterImportData()
    {
        isFinished = true;
    }
    public static Dictionary<ulong, FieldItemData> GetBombData()
    {
        return DenQOffLineDataBase.fieldItemTable;
    }
}
