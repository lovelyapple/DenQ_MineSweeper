using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Linq;
using System.IO;
using System.Text;

using DenQ;
using DenQData;
public class BombTableImporter : TableImporterBase
{


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
    }
    public override void PreImportData()
    {
        DenQDataBase.bombTable.Clear();
        filePath = "BombTable";
        isFinished = true;
    }
    public override void ImportData()
    {
        var data = new BombData();
        data.itemCode = Read_ulong("master_code");
        data.name = Read_string("name");
        data.bombType = Read_uint("bomb_type");
        data.level = Read_uint("level");
        data.hp = Read_uint("hp");
        data.time = Read_float("time");
        data.damage = Read_uint("damage");
        data.damageRange = Read_float("damage_range");

        if (DenQDataBase.bombTable.ContainsKey(data.itemCode)) return;
        DenQDataBase.bombTable.Add(data.itemCode, data);
        //Debug.Log("bomb code" + data.itemCode + " name " + data.name);
    }
    public override void AfterImportData()
    {
        isFinished = true;
    }
    public static Dictionary<ulong, BombData> GetBombData()
    {
        return DenQDataBase.bombTable;
    }
}
