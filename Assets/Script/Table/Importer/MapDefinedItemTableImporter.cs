﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Linq;
using System.IO;
using System.Text;

using DenQ;
using DenQData;
public class MapDefinedItemTableImporter : TableImporterBase
{
    void Awake()
    {
    }
    public override void PreImportData()
    {
        DenQDataBase.mapDefinedTiemData.Clear();
        filePath = "MapDefinedItemTable";
        isFinished = true;
    }
    public override void ImportData()
    {
        var data = new MapDefinedTiemData();
        data.code = Read_ulong("code");
        data.definedItemDatas = new Dictionary<ulong, uint>();
        var itemCode = Read_ulong("master_code_01");
        if (itemCode != 0)
            data.definedItemDatas.Add(itemCode, Read_uint("item_count_01"));
        itemCode = Read_ulong("master_code_02");
        if (itemCode != 0)
            data.definedItemDatas.Add(itemCode, Read_uint("item_count_01"));
        itemCode = Read_ulong("master_code_03");
        if (itemCode != 0)
            data.definedItemDatas.Add(itemCode, Read_uint("item_count_01"));

        if (DenQDataBase.mapDefinedTiemData.ContainsKey(data.code)) return;
        DenQDataBase.mapDefinedTiemData.Add(data.code, data);
    }
    public override void AfterImportData()
    {
        isFinished = true;
    }
    public static Dictionary<ulong, MapDefinedTiemData> GetBombData()
    {
        return DenQDataBase.mapDefinedTiemData;
    }
}
