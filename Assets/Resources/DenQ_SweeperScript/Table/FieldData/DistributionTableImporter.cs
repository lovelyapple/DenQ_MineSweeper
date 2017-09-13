using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.IO;
using System.Text;

using DenQData;

public struct FieldItem
{
    public ulong itemBaseCode;
    public float rate;
    public uint amountLeft;
}
public class DistributionMap
{
    public List<FieldItem> fieldItemList;
    public DistributionMap()
    {
        fieldItemList = new List<FieldItem>();
        fieldItemList.Clear();
    }
}
///ファイルの構成を考え、独自のインポーターを作成
/// やっぱ、統一しようか
public class DistributionTableImporter : TableImporterBase
{

    private static string _filePath = "";
    private static string _genericFileName = "FieldData/DistributionData/DistributionMap_";
    public static List<string> fileList = new List<string>
    {
        "0000"
    };
    ///特殊なファイルパス設定
    public string SetFileName(string id)
    {
        return string.Format("{0}{1}", _genericFileName, id);
    }
    public override void PreImportData()
    {
        DenQOffLineDataBase.distributionTable = new Dictionary<ulong, DistributionMap>();
        isFinished = true;
    }
    public override void ImportData()
    {
        isFinished = false;
        foreach (var fileId in fileList)
        {
            filePathSpecial = SetFileName(fileId);
            var _distributionData = new DistributionMap();
            var sr = new StreamReader(filePath, Encoding.GetEncoding("SHIFT_JIS"));

            var data = new FieldItem();
            data.itemBaseCode = Read_ulong("item_base_code");
            data.rate = Read_float("rate");
            data.amountLeft = Read_uint("amount_left");
            _distributionData.fieldItemList.Add(data);
            DenQOffLineDataBase.distributionTable.Add(ulong.Parse(fileId), _distributionData);

        }
        isFinished = true;
    }
    public override void AfterImportData()
    {
        isFinished = true;
    }
}
public static class DistributionTableHelper
{
    public static DistributionMap GetDistributionMap(ulong id)
    {
        return DenQOffLineDataBase.distributionTable.ContainsKey(id) ?
            DenQOffLineDataBase.distributionTable[id] : null;
    }
}
