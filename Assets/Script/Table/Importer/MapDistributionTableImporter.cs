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
        DenQDataBase.mapDistributionTable.Clear();
        filePath = "MapDistributionTable";
        isFinished = true;
    }
    public override void ImportData()
    {
        var data = new MapDistributionData();
        data.mapCode = Read_ulong("map_code");
        data.itemCode = Read_ulong("item_code");
        data.itemRate = Read_uint("item_rate");

        DenQDataBase.mapDistributionTable.Add(data);
    }
    public override void AfterImportData()
    {
        isFinished = true;
    }
    public static List<MapDistributionData> GetBombData()
    {
        return DenQDataBase.mapDistributionTable;
    }
}
public static class MapDistributionTableHelper
{
    public static List<MapDistributionData> GetDistributionData(ulong mapCode)
    {
        var db = DenQDataBase.mapDistributionTable;
        var outList = db.Where(x => x.mapCode == mapCode).ToList();
        return outList;
    }
}
