using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Linq;
using System.IO;
using System.Text;

using DenQ;
using DenQData;
public class MapDistributionTableImporter : TableImporterBase
{
    void Awake()
    {
    }
    public override void PreImportData()
    {
        DenQOffLineDataBase.mapDistributionTable.Clear();
        filePath = "MultiRewardTable";
        isFinished = true;
    }
    public override void ImportData()
    {
        var data = new MapDistributionData();
        data.code = Read_ulong("code");
        data.distributionDatas = new Dictionary<ulong, uint>();
        var itemCode = Read_ulong("master_code_01");
        if (itemCode != 0)
            data.distributionDatas.Add(itemCode, Read_uint("item_rate_01"));
        itemCode = Read_ulong("master_code_02");
        if (itemCode != 0)
            data.distributionDatas.Add(itemCode, Read_uint("item_rate_01"));
        itemCode = Read_ulong("master_code_03");
        if (itemCode != 0)
            data.distributionDatas.Add(itemCode, Read_uint("item_rate_01"));

        if (DenQOffLineDataBase.mapDistributionTable.ContainsKey(data.code)) return;
        DenQOffLineDataBase.mapDistributionTable.Add(data.code, data);
    }
    public override void AfterImportData()
    {
        isFinished = true;
    }
    public static Dictionary<ulong, MapDistributionData> GetBombData()
    {
        return DenQOffLineDataBase.mapDistributionTable;
    }
}
public static class MapDistributionTableHelper
{
    public static MapDistributionData GetDistributionData(ulong code)
    {
        var dbs = DenQOffLineDataBase.mapDistributionTable;
        var outData = new MapDistributionData();
        if(!dbs.TryGetValue(code,out outData))
        {
            DenQLogger.SError("coudl not find fieldData Code :" + code);
        }
        return outData;
    }
}
