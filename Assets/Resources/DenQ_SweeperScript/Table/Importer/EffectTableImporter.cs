using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Linq;
using System.IO;
using System.Text;

using DenQ;
using DenQData;
public class EffectTableImorter : TableImporterBase
{


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
    }
    public override void PreImportData()
    {
        DenQDataBase.effectTable.Clear();
        filePath = "EffectTable";
        isFinished = true;
    }
    public override void ImportData()
    {
        var data = new EffectData();
        data.code = Read_ulong("code");
        data.name = Read_string("name");
        data.lifeTime = Read_uint("life_time");

        if (DenQDataBase.effectTable.ContainsKey(data.code)) return;
        DenQDataBase.effectTable.Add(data.code, data);
    }
    public override void AfterImportData()
    {
        isFinished = true;
    }
    public static Dictionary<ulong, EffectData> GetBombData()
    {
        return DenQDataBase.effectTable;
    }
}