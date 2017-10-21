using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Linq;
using System.IO;
using System.Text;

using DenQ;
using DenQData;
public class FieldBlockTableImporter : TableImporterBase
{
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
    }
    public override void PreImportData()
    {
        DenQDataBase.fieldItemTable.Clear();
        filePath = "FieldBlockTable";
        isFinished = true;
    }
    public override void ImportData()
    {
        var data = new FieldBlockData();
        data.itemCode = Read_ulong("master_code");
        data.name = Read_string("name");
        data.blockType = Read_uint("block_type");
        data.level = Read_uint("level");
        data.hp = Read_uint("hp");

        if (DenQDataBase.FieldBlockTable.ContainsKey(data.itemCode)) return;
        DenQDataBase.FieldBlockTable.Add(data.itemCode, data);
        Debug.Log("bomb code" + data.itemCode + " name " + data.name);
    }
    public override void AfterImportData()
    {
        isFinished = true;
    }
    public static Dictionary<ulong, FieldBlockData> GetFieldBlockData()
    {
        return DenQDataBase.FieldBlockTable;
    }
}
